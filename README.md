Com certeza! Aqui está um `README.md` conciso e bem formatado, perfeito para o seu Obsidian, explicando a função de cada arquivo na nova arquitetura CQRS.

---

# Arquitetura da API: CQRS + Mediator

Este projeto utiliza o padrão **CQRS (Command Query Responsibility Segregation)** com o auxílio do **Mediator** para criar uma API limpa, escalável e fácil de manter. A lógica é separada entre operações de escrita (Commands) e leitura (Queries).

---

## 🚀 O Fluxo de uma Requisição

1.  Um **Controller** recebe uma requisição HTTP.
2.  Ele cria um objeto **Command** (para escrita) ou **Query** (para leitura).
3.  O objeto é enviado para o **Mediator**.
4.  O Mediator encontra o **Handler** correto para processar a requisição.
5.  O Handler executa a lógica de negócio, interagindo diretamente com o **DbContext**.

---

## 🏛️ Domain

A camada mais interna, contendo a lógica de negócio principal.

* `Entities/User.cs`: Representa a entidade `User` e suas regras de negócio. É o molde para a tabela do banco de dados.

---

## ✨ Application (ou Services)

Onde a lógica da aplicação vive. Orquestra o fluxo de dados e substitui os "Services" tradicionais.

* `Commands/`: Representam uma **intenção de alterar dados**.
    * `CreateUserCommand.cs`: Carrega os dados necessários para criar um usuário. Funciona como um DTO de entrada para escrita.
    * `UpdateUserCommand.cs`: Carrega os dados para atualizar um usuário.
    * `DeleteUserCommand.cs`: Carrega o ID do usuário a ser deletado.

* `Queries/`: Representam uma **intenção de ler dados**.
    * `GetUserByIdQuery.cs`: Define a solicitação para buscar um usuário por ID.

* `Handlers/`: **Contêm a lógica de negócio real**. Cada Handler tem uma única responsabilidade.
    * `CreateUserCommandHandler.cs`: Recebe o `CreateUserCommand`, valida as informações, faz o hash da senha e salva o novo usuário no banco via `DbContext`.
    * `GetUserByIdQueryHandler.cs`: Recebe o `GetUserByIdQuery`, consulta o banco via `DbContext` e retorna um `UserResponseDto`.

---

## 📄 DTOs (Data Transfer Objects)

Usados principalmente para **formatar as respostas da API**.

* `UserResponseDto.cs`: Define a estrutura dos dados do usuário que serão enviados para o cliente. Garante que dados sensíveis (como o hash da senha) nunca sejam expostos.

---

## 🎮 Controllers

O ponto de entrada da API. Lida com requisições e respostas HTTP.

* `UserController.cs`: Fica extremamente "magro". Sua única função é receber a requisição, empacotar os dados em um `Command` ou `Query`, enviar para o `Mediator` e retornar a resposta HTTP apropriada (`Ok`, `NotFound`, `Created`). **Não contém nenhuma lógica de negócio**.

---

## 💾 Infrastructure

Contém os detalhes técnicos de implementação.

* `Data/AppDbContext.cs`: A representação da sessão com o banco de dados. É injetado diretamente nos `Handlers` para permitir o acesso aos dados.

---

## ⚙️ Program.cs

O ponto de partida da aplicação.

* **Função:** Configura e conecta todas as peças. Registra os serviços essenciais no contêiner de injeção de dependência, como o `DbContext` e, mais importante, o `MediatR`, que automaticamente descobre todos os Handlers do projeto.