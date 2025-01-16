# API Calculadora com JWT e Redis

Este repositório contém a implementação de uma API de Calculadora utilizando autenticação com JWT e integração com Redis para gerenciamento de dados.

[![.NET 8 Build, Docker Build](https://github.com/Leock9/CalcApiJwt/actions/workflows/main.yml/badge.svg)](https://github.com/Leock9/CalcApiJwt/actions/workflows/main.yml)

## **Sumário**
- [Visão Geral](#visão-geral)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Como Executar](#como-executar)
   - [Rodando via Docker Compose](#rodando-via-docker-compose)
- [DockerHub](https://hub.docker.com/r/lkhouri/calcapi)
---

## **Visão Geral**

A API Calculadora suporta operações matemáticas básicas e utiliza JWT para autenticação e Redis como cache para melhorar o desempenho e gerenciamento de sessões.

---

## **Tecnologias Utilizadas**

Certifique-se de que as seguintes ferramentas estejam instaladas:
- [.NET 8](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Redis](https://redis.io/)
- [JWT](https://jwt.io/)
- [Swagger](https://swagger.io/)
- [FastEndpoints](https://fast-endpoints.com/)
---

## **Como Executar**
### **Rodando via Docker Compose**

1. Clone o repositório:
   ```bash
   git clone https://github.com/Leock9/CalcApiJwt
   cd CalcApiJwt

2. Execute o comando abaixo para rodar a aplicação:
   ```bash
   docker-compose up -d
   ```
3. Importe a collection para seu postaman e faça as requisições necessárias.
   ```bash
   CalcApi.postman_collection.json

4. Acesse a documentação da API através do Swagger:
   ```bash
    http://localhost:8080/swagger

5. Para acessar o UI do Redis:
   ```bash
    http://localhost:8081

## **DockerHub**
A imagem da aplicação está disponível no DockerHub, para baixar a imagem execute o comando abaixo:
```bash
docker pull lkhouri/calcapi
```