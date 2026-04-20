resource "aws_ecs_service" "service" {
  name            = "api-ecs-app"
  cluster         = aws_ecs_cluster.app.id
  task_definition = aws_ecs_task_definition.service.arn
  desired_count   = 2
  launch_type     = "FARGATE"

  load_balancer {
    target_group_arn = aws_lb_target_group.service.arn
    container_name   = "api-ecs-app"
    container_port   = 8080
  }

  network_configuration {
    subnets = module.vpc.public_subnets
    assign_public_ip = true
    security_groups = [
        aws_security_group.service.id
    ]
  }
}


resource "aws_security_group" "service" {
  name        = "Sg para API service do ECS"
  description = "Security padrao para API do ECS"
  vpc_id      =  module.vpc.vpc_id

  tags = {
    managed_by = "Terraform"
  }
}

resource "aws_vpc_security_group_ingress_rule" "service_porta_8080" {
  security_group_id = aws_security_group.service.id
  cidr_ipv4         = "0.0.0.0/0"
  from_port         = 8080
  ip_protocol       = "tcp"
  to_port           = 8080
}

resource "aws_vpc_security_group_egress_rule" "allow_all_traffic" {
  security_group_id = aws_security_group.service.id
  cidr_ipv4         = "0.0.0.0/0"
  ip_protocol       = "-1"
}

