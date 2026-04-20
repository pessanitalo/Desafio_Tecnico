resource "aws_lb" "service" {
  name               = "lb-ecs-app"
  internal           = false
  load_balancer_type = "application"
  security_groups    = [aws_security_group.load_balancer.id]
  subnets            = module.vpc.public_subnets 

  tags = {
    managed_by = "terraform"
  }
}

resource "aws_security_group" "load_balancer" {
  name        = "Sg para LB para o service"
  description = "Security padrao para o ECS"
  vpc_id      = module.vpc.vpc_id

  tags = {
    managed_by = "Terraform"
  }
}

resource "aws_vpc_security_group_ingress_rule" "service_lb_porta_80" {
  security_group_id = aws_security_group.load_balancer.id
  cidr_ipv4         = "0.0.0.0/0"
  from_port         = 80
  ip_protocol       = "tcp"
  to_port           = 80
}

resource "aws_vpc_security_group_ingress_rule" "service_lb_porta_443" {
  security_group_id = aws_security_group.load_balancer.id
  cidr_ipv4         = "0.0.0.0/0"
  from_port         = 443
  ip_protocol       = "tcp"
  to_port           = 443
}

resource "aws_vpc_security_group_egress_rule" "service_lb_allow_all_traffic" {
  security_group_id = aws_security_group.load_balancer.id
  cidr_ipv4         = "0.0.0.0/0"
  ip_protocol       = "-1"
}

resource "aws_lb_target_group" "service" {
  name     = "lb-tg-api-service"
  port     = 8080
  protocol = "HTTP"
  vpc_id   = module.vpc.vpc_id
  target_type = "ip"

  health_check {
    protocol = "HTTP"
    port = "8080"
    path = "/api/health"
    interval = 30
    timeout = 5
    healthy_threshold = 3
    unhealthy_threshold = 2
    matcher = "200"
  }
}

resource "aws_lb_listener" "service" {
  load_balancer_arn = aws_lb.service.arn
  port              = "80"
  protocol          = "HTTP"

  default_action {
    type = "forward"
    target_group_arn = aws_lb_target_group.service.arn
  }
}

