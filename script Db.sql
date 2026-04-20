create database desafio_tecnico;

use desafio_tecnico;

CREATE TABLE Paciente (
    PacienteId INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(150) NOT NULL,
    CPF NVARCHAR(11) UNIQUE NOT NULL
);


CREATE TABLE Profissional (
    ProfissionalId INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(150) NOT NULL,
    CPF NVARCHAR(11) UNIQUE NOT NULL
);


CREATE TABLE Consulta (
    ConsultaId INT IDENTITY(1,1) PRIMARY KEY,
    PacienteId INT NOT NULL,
    ProfissionalId INT NOT NULL,
    DataConsulta DATE NOT NULL,
    HoraConsulta TIME(0) NOT NULL,
    CONSTRAINT FK_Consulta_Paciente FOREIGN KEY (PacienteId) REFERENCES Paciente(PacienteId),
    CONSTRAINT FK_Consulta_Profissional FOREIGN KEY (ProfissionalId) REFERENCES Profissional(ProfissionalId)
);

CREATE TABLE Usuario (
    UsuarioId INT IDENTITY PRIMARY KEY,
    Email VARCHAR(100) NOT NULL,
    Senha VARCHAR(100) NOT NULL
);

CREATE TABLE RefreshToken (
    Id INT IDENTITY PRIMARY KEY,
    UsuarioId INT NOT NULL,
    Token VARCHAR(200) NOT NULL,
    Expiracao DATETIME2 NOT NULL,
    Revogado BIT NOT NULL DEFAULT 0,

    FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId)
);

 -- tabela auxiliar de feriados de sp 
CREATE TABLE Feriado (
    Id INT PRIMARY KEY IDENTITY,
    Data DATE NOT NULL UNIQUE,
    Descricao NVARCHAR(100)
);

-- Insere os feriados de SP
INSERT INTO Feriado (Data, Descricao) VALUES
('2026-01-01', 'Ano Novo'),
('2026-02-12', 'Carnaval'),
('2026-03-29', 'Sexta-feira Santa'),
('2026-04-21', 'Tiradentes'),
('2026-05-01', 'Dia do Trabalho'),
('2026-09-07', 'Independência'),
('2026-10-12', 'Nossa Senhora Aparecida'),
('2026-11-02', 'Finados'),
('2026-11-15', 'Proclamação da República'),
('2026-11-20', 'Consciência Negra'),
('2026-12-25', 'Natal');
-- 1003 , 1004, 1005, 1006
select * from Paciente;
select * from Profissional;
select * from Consulta;
select * from Usuario;
select * from RefreshToken;
select * from Feriado;

select * from Consulta where ProfissionalId = 2;

