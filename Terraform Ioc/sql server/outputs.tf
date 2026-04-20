output "db_password" {
  value     = random_password.password.result
  sensitive = true
}

output "db_endpoint" {
  value = aws_db_instance.demosqlserver.endpoint
}

output "connection_string" {
  value = "Server=${aws_db_instance.demosqlserver.endpoint};Database=master;User Id=${aws_db_instance.demosqlserver.username};Password=${random_password.password.result};"
  sensitive = true
}