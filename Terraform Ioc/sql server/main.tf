
resource "random_password" "password" {
  length           = 8
  special          = true
  override_special = "!#$%&*()-_=+[]{}<>:?"
}

resource "aws_security_group" "demosqlserver" {
  name        = "sqlserver-demo-sg"
  description = "Security group for SQL Server RDS instance"
  vpc_id      = module.vpc.vpc_id

  ingress {
    from_port   = 1433
    to_port     = 1433
    protocol    = "tcp"
    cidr_blocks = [var.allowed_cidr]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

resource "aws_db_instance" "demosqlserver" {
  allocated_storage          = 20
  auto_minor_version_upgrade = false
  backup_retention_period    = 0
  db_subnet_group_name       = module.vpc.database_subnet_group_name
  engine                     = "sqlserver-ex"
  engine_version             = "15.00.4455.2.v1"
  identifier                 = "sql-instance-demo"
  instance_class             = "db.t3.small"
  multi_az                   = false
  password                   = random_password.password.result
  storage_encrypted          = true
  skip_final_snapshot        = true
  username                   = "demosqlserver"
  vpc_security_group_ids     = [aws_security_group.demosqlserver.id]
  publicly_accessible        = true
  deletion_protection        = false
}
