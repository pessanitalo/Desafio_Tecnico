# 📌 Desafio Técnico

Uma aplicação completa de gerenciamento de consultas médicas desenvolvida com .NET 8 no backend, Angular no frontend e SQL Server como banco de dados.

## 🚀 Como executar o projeto

### ✅ Pré-requisitos

- **Docker** instalado
- Ferramenta para acessar banco de dados (SQL Server Management Studio ou Azure Data Studio)
- Git instalado

---

## 📋 Passo a passo

### 1️⃣ Clone o repositório

```bash
git clone https://github.com/pessanitalo/Desafio_Tecnico.git
cd Desafio_Tecnico
```

### 2️⃣ Suba os containers com Docker

```bash
docker-compose up -d
```

Isso irá iniciar:
- ✔️ Backend (API)
- ✔️ Frontend (Angular)
- ✔️ Banco de dados (SQL Server)

### 3️⃣ Configure o banco de dados

Acesse o banco de dados com as seguintes credenciais:

| Configuração | Valor |
|---|---|
| **Servidor** | `localhost,1433` |
| **Autenticação** | SQL Server Authentication |
| **Usuário** | `sa` |
| **Senha** | `Numsey#2021` |

### 4️⃣ Execute os scripts do banco

Na raiz do projeto, localize o arquivo `script_db` que contém:
- ✔️ Criação do banco de dados
- ✔️ Criação das tabelas
- ✔️ Dados iniciais (se aplicável)

Execute os scripts na ferramenta de acesso ao banco de dados.

---

## 🌐 Acessos da aplicação

### Local

| Aplicação | URL |
|---|---|
| **Swagger (API)** | http://localhost:5500/swagger/index.html |
| **Frontend** | http://localhost:4200/login |


---

## Decisões Técnicas

### Arquitetura

A aplicação foi construída utilizando conceitos de **DDD (Domain-Driven Design)**, garantindo separação de responsabilidades e maior manutenibilidade.

#### 🔹 Camadas da aplicação

##### **1. API**
- Responsável por receber requisições HTTP
- Encaminha para a camada de Application
- Não possui dependência direta do domínio
- Implementa versionamento de rotas

##### **2. Application**
- Contém a lógica de orquestração
- Estruturada em:

  **DTOs** (Data Transfer Objects)
  - Transportam dados entre camadas
  - Evitam acoplamento com o domínio
  
  **Use Cases**
  - Orquestram as regras de negócio
  - Facilitam manutenção e evolução do sistema

##### **3. Domain**
- Contém as regras de negócio core
- Núcleo da aplicação
- Isolado de dependências externas
- Define entidades e agregados
- Padrão Factory

##### **4. Infrastructure**
- Responsável por implementações técnicas
- Implementa padrões como:
  - **Repository Pattern** para acesso a dados
  - **Dependency Injection** para gerenciamento de dependências

### Frontend

**Tecnologia:** Angular

**Motivos da escolha:**
- ✔️ Maior familiaridade do time
- ✔️ Estrutura organizada e padronizada
- ✔️ Melhor padronização para projetos maiores
- ✔️ Ecossistema robusto

###  Backend

**Tecnologia:** .NET 8

**Principais decisões técnicas:**

```csharp
// Dependency Injection no Program.cs
builder.Services
    .AddScoped<IPatientUseCase, PatientUseCase>()
    .AddScoped<IPatientRepository, PatientRepository>();

// Middleware para tratamento global de exceções
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

- ✔️ **Dependency Injection** para manter o Program.cs limpo e organizado
- ✔️ **Middleware** para tratamento global de exceções
- ✔️ Validação de regras de negócio na camada Domain

### Banco de Dados

**SQL Server**

**Features implementadas:**
- ✔️ Tabela de feriados de São Paulo
- ✔️ Relacionamentos entre entidades
---

## 📌 Regras de Negócio Implementadas

- ❌ Não é possível cadastrar pacientes com **CPF duplicado**
- ❌ Não é possível cadastrar profissionais com **CPF duplicado**
- ❌ Não é permitido agendar consultas em **finais de semana**
- ❌ Não é permitido agendar consultas em **feriados**
- ✅ Validação de disponibilidade de profissionais
- ✅ Validação de horários conflitantes

---

## Docker Compose

O arquivo `docker-compose.yml` orquestra os seguintes serviços:

```yaml
services:
  - frontend     (Angular - Port 4200)
  - api          (ASP.NET Core - Port 5500)
  - database     (SQL Server - Port 1433)
```

---

##  Infraestrutura

**Plataforma:** AWS (Amazon Web Services)

**Ferramenta:** Terraform

**Motivos da escolha:**
- ✔️ Infrastructure as Code (IaC)
- ✔️ Facilidade de manutenção
- ✔️ Infraestrutura versionada no Git
- ✔️ Reprodutibilidade do ambiente
- ✔️ Escalabilidade

**Recursos provisionados:**
- S3 (hospedagem do frontend)
- EC2 (backend e banco de dados)
- RDS (alternativa para banco de dados gerenciado)
---

##  Estrutura do Projeto

```
Desafio_Tecnico/
├── backend/
│   ├── src/
│   │   ├── API/
│   │   ├── Application/
│   │   ├── Domain/
│   │   └── Infrastructure/
│   ├── docker-compose.yml
│   └── script_db/
├── frontend/
│   ├── src/
│   │   ├── app/
│   │   ├── assets/
│   │   └── environments/
│   └── Dockerfile
├── terraform/
│   ├── main.tf
│   ├── variables.tf
│   └── outputs.tf
└── README.md
```

---

##  Troubleshooting

### Porta 1433 já em uso

```bash
# Altere a porta no docker-compose.yml
ports:
  - "1434:1433"
```

### Containers não iniciam

```bash
# Verifique os logs
docker-compose logs -f

# Recrie os containers
docker-compose down
docker-compose up -d --build
```
