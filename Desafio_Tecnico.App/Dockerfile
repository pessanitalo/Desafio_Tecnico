### Estágio 1 - Build Angular ###
FROM node:20-alpine AS front-end

WORKDIR /app

COPY package.json package-lock.json ./
RUN npm ci

COPY . .

RUN npm run build -- --configuration production


### Estágio 2 - Nginx ###
FROM nginx:alpine

COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=front-end /app/dist/desafio-tecnico.app/browser  /usr/share/nginx/html

EXPOSE 80