data "aws_availability_zones" "available" {
  state = "available"
}

locals {
  azs = slice(data.aws_availability_zones.available.names, 0, 3)
}

module "vpc" {
  source  = "terraform-aws-modules/vpc/aws"
  version = "5.16.0"

  name = "vpc-avera-db"
  cidr = var.vpc_cidr

  azs                    = local.azs
  private_subnets        = [for k, v in local.azs : cidrsubnet(var.vpc_cidr, 8, k)]
  public_subnets         = [for k, v in local.azs : cidrsubnet(var.vpc_cidr, 8, k + 4)]
  database_subnets         = [for k, v in local.azs : cidrsubnet(var.vpc_cidr, 8, k + 8)]
  database_subnet_group_name = "vpc-avera-db"
  create_database_subnet_group = true
  create_database_subnet_route_table = true
  create_database_internet_gateway_route = true
  enable_dns_hostnames    = true
  enable_dns_support      = true
  enable_nat_gateway      = false
  single_nat_gateway      = true
  one_nat_gateway_per_az  = false
  map_public_ip_on_launch = true
  
  tags = {
    ManagedBy   = "Terraform"
  }
}
