# Variáveis do Terraform
variable "aws_region" {
  description = "Região AWS onde o bucket será criado"
  type        = string
  default     = "us-east-1"
}

variable "environment" {
  description = "Ambiente (dev, prod, etc)"
  type        = string
  default     = "dev"
}
