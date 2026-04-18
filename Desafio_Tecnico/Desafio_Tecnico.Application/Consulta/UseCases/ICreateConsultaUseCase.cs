using Desafio_Tecnico.Application.Consulta.DTOs;
using Desafio_Tecnico.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Tecnico.Application.Consulta.UseCases
{
    public interface ICreateConsultaUseCase
    {
        Task<Result<string>> AddAsync(ConsultaDTO consultaDTO);
    }
}
