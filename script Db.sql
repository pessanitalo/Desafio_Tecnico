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

select * from Paciente;
select * from Profissional;
select * from Consulta;
select * from Usuario;
select * from RefreshToken;