# Bitzen API

API RESTful desenvolvida como parte de um desafio técnico, utilizando .NET 8 e PostgreSQL, para gestão de salas de reunião e agendamento de reservas.

## 🛠 Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- AutoMapper
- FluentValidation
- Swagger (Swashbuckle)
- JWT (Json Web Token) Authentication

## 🎯 Objetivo

Construir uma API para:
- Gerenciar usuários (CRUD)
- Autenticar usuários com JWT
- Gerenciar salas (CRUD)
- Gerenciar reservas de salas com validações de conflitos de horários
- Buscar reservas por filtros (usuário, sala, data e status)

## 🗂 Estrutura do Projeto

```
Bitzen_API/
│
├── Application/              # Lógica de negócio e serviços
│   ├── Services/
│
├── Controllers/              # Controllers da API
│
├── ORM/
│   ├── Context/              # DbContext
│   ├── Entity/               # Entidades do banco de dados
│   ├── Mappings/             # Perfis do AutoMapper
│   ├── Model/                # Models de entrada e saída
│   ├── Repository/           # Repositórios genéricos
│
├── Program.cs                # Configuração da aplicação
├── appsettings.json          # Configurações (conexão, JWT)
```

## 🔐 Autenticação

A autenticação é feita via JWT. Para utilizar as rotas protegidas:

1. Realize login em `POST /api/User/login`
2. Copie o token retornado
3. Clique em “Authorize” no Swagger e insira o token como:

```
{seu_token}
```

## 🧪 Endpoints

  ### Usuários
  - `POST /api/User` – Criar usuário
  - `PUT /api/User/update-user/{id}` – Editar usuário
  - `DELETE /api/User/delete-user/{id}` – Remover usuário
  - `POST /api/User/login` – Autenticação via JWT
  
  ### Salas
  - `POST /api/Room` – Criar sala
  - `PUT /api/Room/update-room/{id}` – Editar sala
  - `DELETE /api/Room/delete-room/{id}` – Remover sala
  - `GET /api/Room` – Listar salas
  
  ### Reservas
  - `POST /api/Reservation` – Criar reserva
  - `GET /api/Reservation` – Listar reservas com filtros (data, status, sala, usuário) + paginação
  - `DELETE /api/Reservation/cancel-reservation/{id}` – Cancelar reserva
  
  ## 📌 Regras de Negócio
  
  - Não é permitido reservas sobrepostas para a mesma sala.
  - A reserva deve iniciar e terminar no mesmo dia.
  - Apenas usuários autenticados podem criar salas ou reservas.

## ⚙️ Execução

1. Configure o `appsettings.json` com sua connection string PostgreSQL e chave JWT.
2. Execute os comandos:
```bash
dotnet ef database update
dotnet run
```
3. Acesse o Swagger em: `https://localhost:{porta}/swagger`
