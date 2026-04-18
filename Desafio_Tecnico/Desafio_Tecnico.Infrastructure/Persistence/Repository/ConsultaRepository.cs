using Dapper;
using Desafio_Tecnico.Application.Consulta.DTOs;
using Desafio_Tecnico.Domain.Entities;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.ValueObjects;
using System.Data;

namespace Desafio_Tecnico.Infrastructure.Persistence.Repository
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly IDbConnection _connection;

        public ConsultaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AddAsync(Consulta consulta)
        {
            var sql = @"
            INSERT INTO Consulta (DataConsulta, HoraConsulta, PacienteId, ProfissionalId)
            VALUES (@DataConsulta, @HoraConsulta, @PacienteId, @ProfissionalId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            var parametros = new
            {
                DataConsulta = consulta.DataConsulta.Data,
                HoraConsulta = consulta.HoraConsulta.Hora,
                PacienteId = consulta.PacienteId,
                ProfissionalId = consulta.ProfissionalId,
            };

            var id = await _connection.ExecuteAsync(sql, parametros);
        }

        public async Task<Consulta> GetByConsultaAgendada(int pacienteId, int profissionalId, DateTime dataConsulta, TimeSpan horaConsulta)
        {
            const string sql = @"
                    SELECT * 
                    FROM Consulta 
                    WHERE PacienteId = @PacienteId
                    AND ProfissionalId =@ProfissionalId
                    AND DataConsulta = @DataConsulta
                    AND HoraConsulta = @HoraConsulta";

            var result = await _connection.QueryFirstOrDefaultAsync<ConsultaDTO>(
                sql,
                new
                {
                    PacienteId = pacienteId,
                    ProfissionalId = profissionalId,
                    DataConsulta = dataConsulta.Date,
                    HoraConsulta = horaConsulta
                });

            if (result == null)
                return null;

            return new Consulta(
                new DataConsulta(result.DataConsulta),
                new HoraConsulta(result.HoraConsulta),
                result.PacienteId,
                result.ProfissionalId
            );
        }

        public async Task<bool> TemConflito(
                   int pacienteId,
                   int profissionalId,
                   DateTime dataConsulta,
                   TimeSpan horaConsulta)
        {
            const string sql = @"
                SELECT COUNT(*) 
                FROM Consulta 
                WHERE DataConsulta = @DataConsulta
                AND HoraConsulta < @HoraFim
                AND DATEADD(MINUTE, 30, HoraConsulta) >= @HoraInicio
                AND (
                    ProfissionalId = @ProfissionalId
                    OR PacienteId = @PacienteId
                )";

            var count = await _connection.ExecuteScalarAsync<int>(
                sql,
                new
                {
                    PacienteId = pacienteId,
                    ProfissionalId = profissionalId,
                    DataConsulta = dataConsulta.Date,
                    HoraInicio = horaConsulta,
                    HoraFim = horaConsulta.Add(TimeSpan.FromMinutes(30))
                });

            return count > 0;
        }

        public async Task<Consulta> ObterPorIdAsync(int id)
        {
            const string sql = @"
                SELECT 
                    c.ConsultaId,
                    c.PacienteId,
                    c.ProfissionalId,
                    c.DataConsulta,
                    c.HoraConsulta,
                    p.Nome AS PacienteNome,
                    prof.Nome AS ProfissionalNome
                FROM Consulta c
                INNER JOIN Paciente p ON c.PacienteId = p.PacienteId
                INNER JOIN Profissional prof ON c.ProfissionalId = prof.ProfissionalId";

            var resultado = await _connection.QueryFirstOrDefaultAsync<dynamic>(
                sql,
                new { ConsultaId = id }
            );

            if (resultado == null)
                return null;

            return Consulta.ReconstituirDoRepository(
                resultado.ConsultaId,
                resultado.DataConsulta,
                resultado.HoraConsulta,
                resultado.PacienteId,
                resultado.ProfissionalId,
                resultado.PacienteNome,
                resultado.ProfissionalNome
            );
        }

        public async Task<IEnumerable<Consulta>> ObterTodasConsultasIdAsync()
        {
            const string sql = @"
            SELECT
                c.ConsultaId,
                c.PacienteId,
                c.ProfissionalId,
                c.DataConsulta,
                c.HoraConsulta,
                p.Nome AS PacienteNome,
                prof.Nome AS ProfissionalNome
            FROM Consulta c
            INNER JOIN Paciente p ON c.PacienteId = p.PacienteId
            INNER JOIN Profissional prof ON c.ProfissionalId = prof.ProfissionalId";

            var resultados = await _connection.QueryAsync<dynamic>(sql);

            var consultas = new List<Consulta>();

            foreach (var resultado in resultados)
            {
                var consulta = Consulta.ReconstituirDoRepository(
                    resultado.ConsultaId,
                    resultado.DataConsulta,
                    resultado.HoraConsulta,
                    resultado.PacienteId,
                    resultado.ProfissionalId,
                    resultado.PacienteNome,
                    resultado.ProfissionalNome
                );

                consultas.Add(consulta);
            }
            return consultas;
        }

        public async Task<IEnumerable<Consulta>> ObterAgendaPorProfissionalIdAsync(int profissionalId)
        {
            const string sql = @"
                SELECT 
                    c.ConsultaId,
                    c.PacienteId,
                    c.ProfissionalId,
                    c.DataConsulta,
                    c.HoraConsulta,
                    p.Nome AS PacienteNome,
                    prof.Nome AS ProfissionalNome
                FROM Consulta c
                INNER JOIN Paciente p ON c.PacienteId = p.PacienteId
                INNER JOIN Profissional prof ON c.ProfissionalId = prof.ProfissionalId
                WHERE c.ProfissionalId = @ProfissionalId
            ";

            var resultados = await _connection.QueryAsync<dynamic>(
                sql,
                new { ProfissionalId = profissionalId }
            );

            var consultas = new List<Consulta>();

            foreach (var resultado in resultados)
            {
                var consulta = Consulta.ReconstituirDoRepository(
                    resultado.ConsultaId,
                    resultado.DataConsulta,
                    resultado.HoraConsulta,
                    resultado.PacienteId,
                    resultado.ProfissionalId,
                    resultado.PacienteNome,
                    resultado.ProfissionalNome
                );

                consultas.Add(consulta);
            }

            return consultas;
        }
    }
}
