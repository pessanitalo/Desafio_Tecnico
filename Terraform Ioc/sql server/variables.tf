variable "vpc_cidr" {
  description = "The CIDR block for the VPC"
  type        = string
  default     = "10.0.0.0/16"
}

variable "aws_region" {
  description = "Região da AWS"
  type        = string
  default     = "us-east-1"
}

variable "allowed_cidr" {
  type     = string
  default  = "177.33.87.86/32"  ## atualizar o ip para o da máquina local
}