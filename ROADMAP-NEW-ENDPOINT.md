
# Roadmap de Implementação de Novo Modelo

Este documento serve como checklist para padronizar a cria### 9. 🔧 Configuração e Injeção de Dependências

- [ ] Registrar serviços no container de DI
  - [ ] Handler Service como Scoped
  - [ ] Command Service como Scoped
  - [ ] Query Service como Scoped
- [ ] Configurar EF Core se necessário
  - [ ] Adicionar DbSet no contexto
  - [ ] Configurar relacionamentos no OnModelCreating se necessário

## Convenções e Boas Práticas

## Checklist Completo para Novo Modelo (Exemplo: User)

### 1. Modelo de Domínio

- [ ] Criar o modelo na pasta `Domain/Models/`
  - Exemplo: `User.cs`
  - Deve conter propriedades como:
    - `int Id`, `string Name`, `string Email`, `string Password`, `DateTime CreatedAt`, `DateTime? UpdatedAt`, `DateTime? DeletedAt`
  - Herdar de `BaseEntity` se aplicável
  - Usar anotações de DataAnnotations se necessário
  - Propriedades de auditoria presentes

### 2. DTOs de Entrada (Commands)

Criar na pasta `Application/DTOs/Commands/User/`:

- [ ] `CreateUserCommand.cs`
  - Propriedades: `Name`, `Email`, `Password` (sem Id e sem campos de auditoria)
- [ ] `UpdateUserCommand.cs`
  - Propriedades: `Name`, `Email`, `Password` (Id é passado por parâmetro no handler/service)
- [ ] `DeleteUserCommand.cs`
  - Propriedade: `Id` (int)

### 3. DTO de Resposta

- [ ] Criar `UserResponseDto.cs` em `Application/DTOs/Response/`
  - Deve conter todas as propriedades relevantes do modelo
  - Datas convertidas para string ISO 8601 se necessário
  - Propriedades obrigatórias com valor padrão

### 4. Adapter

- [ ] Criar `UserAdapter.cs` em `Infrastructure/Adapters/`
  - Método estático `ToDto(User? user)`
  - Retorna null se user for nulo
  - Mapeia propriedades do modelo para o DTO

### 5. Handler Service

- [ ] Criar `UserHandlerService.cs` em `Application/Services/Handlers/`
  - Injetar `AppDbContext` e `IPasswordService` via construtor
  - Métodos:
    - `HandleAsync(CreateUserCommand command)`
    - `HandleAsync(int id, UpdateUserCommand command)`
    - `HandleAsync(DeleteUserCommand command)`
  - Implementar regras de negócio, hashing de senha, soft delete, etc.

### 6. Command Service

- [ ] Criar `UserCommandService.cs` em `Application/Services/Commands/`
  - Injetar Handler via construtor
  - Métodos:
    - `CreateUserAsync(CreateUserCommand command)`
    - `UpdateUserAsync(int id, UpdateUserCommand command)`
    - `DeleteUserAsync(DeleteUserCommand command)`

### 7. Query Service

- [ ] Criar `UserQueryService.cs` em `Application/Services/Queries/`
  - Injetar `AppDbContext` via construtor
  - Métodos:
    - `GetAllUsersAsync()`
    - `GetUserByIdAsync(int id)`
    - `GetUserByEmailAsync(string email)`
    - Para cada relacionamento 1:N, 1:1 ou N:N presente na model:
      - Criar métodos como `GetUsersByRoleId(int roleId)`, `GetUsersByGroupId(int groupId)`, etc.
      - Incluir métodos para buscar entidades relacionadas conforme a necessidade do domínio.

### 8. Controller REST API

- [ ] Criar `UserController.cs` em `Presentation/Controllers/`
  - Usar `[ApiController]` e `[Route("api/v1/users")]`
  - Injetar Command Service e Query Service via construtor
  - Implementar endpoints REST:
    - **GET** `consultar` - Listar todos
    - **GET** `{id:int}` - Buscar por ID
    - **POST** `criar` - Criar novo usuário
    - **PUT** `{id:int}` - Atualizar usuário
    - **DELETE** `{id:int}` - Excluir usuário (soft delete)
    - Para cada relacionamento 1:N, 1:1 ou N:N presente na model:
      - Criar endpoints como `GET api/v1/users/por-role/{roleId:int}` para buscar usuários por RoleId
      - Criar endpoints para buscar entidades relacionadas conforme a necessidade do domínio
  - Adicionar logging e documentação se necessário

### 9. Configuração e Injeção de Dependências

- [ ] Registrar serviços no container de DI em `ServiceExtensions.cs`
  - Exemplo:
    - `services.AddScoped<UserQueryService>();`
    - `services.AddScoped<UserCommandService>();`
    - `services.AddScoped<UserHandlerService>();`
- [ ] Configurar EF Core
  - Adicionar `DbSet<User> Users` no contexto (`AppDbContext`)
  - Configurar relacionamentos (1:N, 1:1, N:N) no OnModelCreating se necessário

### 10. Criação da Migration

  - Exemplo de comando:
    - `dotnet ef migrations add AddUserTable --o Infrastructure/Data/Migrations --startup-project .`
  - Revisar arquivo de migration
  - Aplicar migration ao banco de dados
> **Observação:** Os arquivos de migration são gerados na pasta `Infrastructure/Data/Migrations`.

## Checklist Final

Antes de considerar o modelo completo, verifique:

- [ ] Model User criado
- [ ] DTOs (Commands/Response) criados
- [ ] Adapter criado
- [ ] Model adicionado no contexto
- [ ] Services (Handlers, Commands, Queries) criados
- [ ] Controller criado
- [ ] Nomenclatura segue as convenções
- [ ] Injeção de dependências configurada
- [ ] Endpoints REST funcionando corretamente (quando controller estiver pronto)
- [ ] Soft delete implementado
- [ ] Campos de auditoria funcionando
- [ ] Relacionamentos carregando corretamente (se houver)
  - [ ] Métodos e endpoints para consultas por chave estrangeira ou entidades relacionadas implementados

### 3. DTO de Resposta (Response)

- [ ] Criar `{ModelName}ResponseDto.cs` em `Application/DTOs/Response/`
  - [ ] Incluir todas as propriedades do modelo
  - [ ] Converter DateTime para string no formato ISO 8601 (.ToString("o"))
  - [ ] Incluir propriedades de navegação como DTOs aninhados quando necessário
  - [ ] Usar string? para DeletedAt (pode ser nulo)
  - [ ] Campos obrigatórios com `= string.Empty` como valor padrão

### 4. Adapter (Infrastructure Layer)

- [ ] Criar `{ModelName}Adapter.cs` em `Infrastructure/Adapters/`
  - [ ] Implementar método estático `ToDto(Model? model)`
  - [ ] Verificar se model é nulo e retornar null
  - [ ] Converter datas para string usando `.ToString("o")`
  - [ ] Mapear propriedades de navegação usando outros adapters
  - [ ] Tratar campos opcionais com operador `?.`

### 5. Handler Service (Application Layer)

- [ ] Criar `{ModelName}HandlerService.cs` em `Application/Services/Handlers/`
  - [ ] Injetar `Unicv360ServerDbContext` via construtor
  - [ ] Implementar método `HandleAsync(Create{ModelName}Command command)`
    - [ ] Criar nova instância do modelo
    - [ ] Mapear propriedades do command
    - [ ] Adicionar ao contexto
    - [ ] Salvar mudanças
    - [ ] Buscar entidade completa com Includes se necessário
    - [ ] Retornar DTO usando Adapter
  - [ ] Implementar método `HandleAsync(Update{ModelName}Command command)`
    - [ ] Buscar entidade existente com `DeletedAt == null`
    - [ ] Verificar se existe, retornar null se não encontrar
    - [ ] Atualizar propriedades
    - [ ] Definir `UpdatedAt = DateTime.UtcNow`
    - [ ] Salvar mudanças
    - [ ] Retornar DTO atualizado
  - [ ] Implementar método `HandleAsync(Delete{ModelName}Command command)`
    - [ ] Buscar entidade com `DeletedAt == null`
    - [ ] Definir `DeletedAt = DateTime.UtcNow` (soft delete)
    - [ ] Salvar mudanças
    - [ ] Retornar bool indicando sucesso

### 6. Command Service (Application Layer)

- [ ] Criar `{ModelName}CommandService.cs` em `Application/Services/Commands/`
  - [ ] Injetar Handler via construtor
  - [ ] Implementar `Create{ModelName}Async(Create{ModelName}Command command)`
    - [ ] Chamar handler correspondente
    - [ ] Adicionar validações se necessário
  - [ ] Implementar `Update{ModelName}Async(Update{ModelName}Command command)`
  - [ ] Implementar `Delete{ModelName}Async(Delete{ModelName}Command command)`

### 7. Query Service (Application Layer)

- [ ] Criar `{ModelName}QueryService.cs` em `Application/Services/Queries/`
  - [ ] Injetar `Unicv360ServerDbContext` via construtor
  - [ ] Implementar `Get{ModelName}s()` que retorna `IQueryable<{ModelName}>`
    - [ ] Filtrar por `DeletedAt == null`
    - [ ] Incluir relacionamentos necessários com `.Include()`
  - [ ] Implementar `Get{ModelName}ById(int id)` se necessário
    - [ ] Filtrar por Id e `DeletedAt == null`
    - [ ] Incluir relacionamentos
    - [ ] Retornar DTO ou null

### 8. Controller REST API (Presentation Layer)

- [ ] Criar `{ModelName}Controller.cs` em `Presentation/Controllers/`
  - [ ] Usar `[ApiController]` e `[Route("api/v1/{plural-kebab-case}")]`
  - [ ] Injetar Command Service e Query Service via construtor
  - [ ] Implementar endpoints REST:
    - [ ] **GET** `consultar` - Listar todos
      - [ ] Usar `[HttpGet("consultar")]`
      - [ ] Usar `[ProducesResponseType]` para documentação
      - [ ] Retornar `Ok(lista)`
      - [ ] Implementar paginação quando necessário
    - [ ] **GET** `{id:int}` - Buscar por ID
      - [ ] Usar `[HttpGet("{id:int}")]`
      - [ ] Retornar `Ok(item)` ou `NotFound(message)`
      - [ ] Usar mensagem padronizada: `"{Modelo} não encontrado."`
    - [ ] **POST** `criar` - Criar novo registro
      - [ ] Usar `[HttpPost("criar")]`
      - [ ] Usar `[FromBody]` para o Command
      - [ ] Usar `[Authorize]` se necessário
      - [ ] Retornar `Created` ou `StatusCode(201)`
      - [ ] Tratar erros com try-catch
    - [ ] **PUT** `{id}` - Atualizar registro existente
      - [ ] Usar `[HttpPut("{id}")]`
      - [ ] Validar se command.Id == id do parâmetro
      - [ ] Retornar `Ok(updated)` ou `NotFound()`
    - [ ] **DELETE** `{id}` - Excluir registro (soft delete)
      - [ ] Usar `[HttpDelete("{id}")]`
      - [ ] Retornar `NoContent()` ou `NotFound()`
  - [ ] Usar injeção de dependência via construtor primário quando possível
  - [ ] Adicionar logging com `ILogger<{ModelName}Controller>`
  - [ ] Documentar endpoints com XML comments quando necessário

### 9. Configuração e Injeção de Dependências

- [ ] Registrar serviços no container de DI
  - [ ] Handler Service como Scoped
  - [ ] Command Service como Scoped
  - [ ] Query Service como Scoped
- [ ] Configurar EF Core se necessário
  - [ ] Adicionar DbSet no contexto
  - [ ] Configurar relacionamentos no OnModelCreating se necessário

### 12. Criação da nova migration

 - [ ] Executar `dotnet ef migrations add Create{ModelName}Table -o Infrastructure/Data/Migrations`
 - [ ] Executar `dotnet ef database update`

## Convenções e Boas Práticas

### Nomenclatura:

- **Modelos**: PascalCase singular (ex: `Dimension`)
- **DTOs**: PascalCase com sufixo (ex: `DimensionResponseDto`, `CreateDimensionCommand`)
- **Services**: PascalCase com sufixo (ex: `DimensionHandlerService`)
- **Controllers**: PascalCase com sufixo (ex: `DimensionController`)
- **Rotas REST**: kebab-case plural (ex: `api/v1/dimensoes`)

### Estrutura de Pastas:

```
Domain/Models/
Application/
├── DTOs/
│   ├── Commands/{ModelName}/
│   └── Response/
├── Services/
│   ├── Handlers/
│   ├── Commands/
│   └── Queries/
Infrastructure/
└── Adapters/
Presentation/
└── Controllers/
```

### Padrões de Código:

- Usar `DateTime.UtcNow` para campos de auditoria
- Usar `AsNoTracking()` para consultas read-only
- Implementar soft delete via `DeletedAt`
- Usar transações quando necessário
- Validar entradas nos Command Services
- Usar Include() para carregar relacionamentos necessários
- Converter datas para ISO 8601 nos DTOs de resposta
- **REST APIs**: Seguir padrões RESTful com verbos HTTP apropriados
- **Rotas**: Usar plural em kebab-case (ex: `api/v1/avaliacoes`)
- **Status Codes**: 200 (OK), 201 (Created), 204 (NoContent), 404 (NotFound), 400 (BadRequest), 500 (InternalServerError)
- **Mensagens de erro**: Padronizadas com formato `{ message: "Descrição do erro" }`

### Tratamento de Erros:

- Retornar null quando entidade não for encontrada
- Usar try-catch com rollback em transações
- Lançar exceções apropriadas para casos de erro

### Performance:

- Usar `IQueryable` para permitir otimizações do EF Core
- Usar projeções quando possível
- Evitar N+1 queries com Include()
- Usar paginação em listagens

## Exemplo de Implementação Completa

Para referência, veja a implementação completa de `Evaluation` que segue todos estes padrões, ou os exemplos de `EvaluatedType`, `EvaluatorType` que estão implementados no projeto.

## Checklist Final

Antes de considerar o modelo completo, verifique:

- [ ] Model criado primeiro
- [ ] DTOs (Commands/Response) criados conforme regra (UpdateCommand só se não for N:N)
- [ ] Adapter criado
- [ ] Model adicionado no contexto
- [ ] Services (Handlers, Commands, Queries) criados
- [ ] Controller criado
- [ ] Nomenclatura segue as convenções
- [ ] Injeção de dependências configurada
- [ ] Endpoints REST funcionando corretamente
- [ ] Soft delete implementado
- [ ] Campos de auditoria funcionando
- [ ] Relacionamentos carregando corretamente
  - **Id**: Usar int como tipo padrão para chaves primárias
