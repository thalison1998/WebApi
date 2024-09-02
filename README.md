# WebAPI - Student

# 🛠️ Guia para Rodar o Projeto WebApi

Siga estes passos para clonar, configurar e rodar o projeto WebApi, bem como iniciar o front-end associado.

## Passos para Rodar o Projeto WebApi

1. **Clone o Projeto WebApi**
   - Use o seguinte comando para clonar o repositório do projeto:
     ```bash
     git clone https://github.com/thalison1998/WebApi.git
     ```

2. **Abra a Pasta Docker**
   - Navegue até a pasta `docker` no diretório do projeto clonado:
     ```bash
     cd WebApi/docker
     ```

3. **Execute o Docker Compose**
   - Execute o seguinte comando em um terminal dentro da pasta `docker` para iniciar os containers:
     ```bash
     docker-compose up -d
     ```
   - Isso iniciará o banco de dados na porta `5432`.

4. **Execute a Solution WebAPI.sln**
   - Abra a solução `WebAPI.sln` em sua IDE (Visual Studio ou outra IDE compatível).

5. **Configure o Projeto de Inicialização**
   - No Solution Explorer, certifique-se de que o projeto de inicialização seja `WebApi.Api`.

6. **Execute o Projeto**
   - Execute o projeto na IDE para iniciar a API.

7. **Inicie o Front-End**
   - Acesse e siga as instruções no repositório do front-end para iniciar a aplicação:
     - [WebFront](https://github.com/thalison1998/WebFront)

## 🚀 Pronto para Testar!
