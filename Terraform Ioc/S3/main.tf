terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

provider "aws" {
  region = "us-east-1"  # Mude para sua região desejada
}

# Criar bucket S3
resource "aws_s3_bucket" "desafio-app" {
  bucket = "meu-desafio-app-${data.aws_caller_identity.current.account_id}"
  # O nome do bucket precisa ser globalmente único na AWS
  # Adicionar o ID da conta ajuda a garantir isso
}

# Habilitar versionamento (opcional, mas recomendado)
resource "aws_s3_bucket_versioning" "desafio-app" {
  bucket = aws_s3_bucket.desafio-app.id

  versioning_configuration {
    status = "Enabled"
  }
}

# Bloquear acesso público por padrão
resource "aws_s3_bucket_public_access_block" "desafio-app" {
  bucket = aws_s3_bucket.desafio-app.id

  block_public_acls       = false
  block_public_policy     = false
  ignore_public_acls      = false
  restrict_public_buckets = false
}

# Política para permitir acesso público de leitura
resource "aws_s3_bucket_policy" "desafio-app" {
  bucket = aws_s3_bucket.desafio-app.id
  depends_on = [aws_s3_bucket_public_access_block.desafio-app]

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Sid       = "PublicReadGetObject"
        Effect    = "Allow"
        Principal = "*"
        Action    = "s3:GetObject"
        Resource  = "${aws_s3_bucket.desafio-app.arn}/*"
      }
    ]
  })
}

# Website estático (opcional)
resource "aws_s3_bucket_website_configuration" "desafio-app" {
  bucket = aws_s3_bucket.desafio-app.id

  index_document {
    suffix = "index.html"
  }

  error_document {
    key = "index.html"  # Redireciona erros 404 para index.html (útil para SPAs)
  }
}

# Data source para pegar o ID da conta AWS
data "aws_caller_identity" "current" {}

# Output com informações úteis
output "bucket_name" {
  description = "Nome do bucket S3"
  value       = aws_s3_bucket.desafio-app.id
}

output "bucket_arn" {
  description = "ARN do bucket S3"
  value       = aws_s3_bucket.desafio-app.arn
}

output "website_endpoint" {
  description = "Endpoint do website estático"
  value       = aws_s3_bucket_website_configuration.desafio-app.website_endpoint
}
