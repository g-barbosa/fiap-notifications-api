# FIAP Cloud Games - Notifications API

Microsserviço responsável pelo envio de notificações da plataforma **FIAP Cloud Games**. Esta API consome eventos de um broker RabbitMQ e dispara notificações por e-mail para os usuários.

## 📋 Funcionalidades

- **Notificação de Usuário Criado**: Envia e-mail de boas-vindas quando um novo usuário é cadastrado na plataforma
- **Notificação de Pagamento Processado**: Envia e-mail informando o status do pagamento de um pedido

## 🏗️ Arquitetura

O projeto segue a arquitetura em camadas (Clean Architecture):

```
src/
├── FiapCloudGames.Notifications.API/           # Camada de apresentação (Host)
├── FiapCloudGames.Notifications.Application/   # Regras de negócio e serviços
└── FiapCloudGames.Notifications.Infrastructure/ # Implementações externas (RabbitMQ, Email)
```

### Filas RabbitMQ Consumidas

| Fila/Exchange | Tipo | Descrição |
|---------------|------|-----------|
| `usuario-criado` | Queue | Recebe eventos de criação de usuário |
| `pagamento-processado` | Exchange (Fanout) | Recebe eventos de pagamento processado |

## 🚀 Tecnologias

- .NET 8
- RabbitMQ (Mensageria)
- Docker / Kubernetes

## ⚙️ Variáveis de Ambiente

### Configurações da Aplicação

| Variável | Descrição | Valor Padrão |
|----------|-----------|--------------|
| `ASPNETCORE_ENVIRONMENT` | Ambiente de execução (`Development`, `Production`) | `Production` |
| `ASPNETCORE_URLS` | URL de binding da aplicação | `http://+:8080` |
| `TZ` | Timezone da aplicação | `America/Sao_Paulo` |

### Configurações de Logging

| Variável | Descrição | Valor Padrão |
|----------|-----------|--------------|
| `Logging__LogLevel__Default` | Nível de log padrão | `Information` |
| `Logging__LogLevel__Microsoft.AspNetCore` | Nível de log do ASP.NET Core | `Warning` |

### Configurações do RabbitMQ

| Variável | Descrição | Valor Padrão | Sensível |
|----------|-----------|--------------|----------|
| `RabbitMq__Host` | Hostname do servidor RabbitMQ | `rabbitmq` | Não |
| `RabbitMq__Port` | Porta do servidor RabbitMQ | `5672` | Não |
| `RabbitMq__Username` | Usuário de conexão | `admin` | ✅ Sim |
| `RabbitMq__Password` | Senha de conexão | `rabbitmq123` | ✅ Sim |

## 🐳 Executando com Docker

### Build da imagem

```bash
docker build -t fiap-notifications-api .
```

### Executando o container

```bash
docker run -d \
  -p 8080:8080 \
  -e RabbitMq__Host=seu-rabbitmq-host \
  -e RabbitMq__Username=seu-usuario \
  -e RabbitMq__Password=sua-senha \
  fiap-notifications-api
```

## ☸️ Kubernetes

Os manifestos para deploy no Kubernetes estão na pasta `k8s/`:

| Arquivo | Descrição |
|---------|-----------|
| `configmap.yaml` | Configurações não sensíveis |
| `secret.yaml` | Credenciais e dados sensíveis |
| `deployment.yaml` | Definição do deployment |
| `service.yaml` | Exposição do serviço |

### Deploy no cluster

```bash
kubectl apply -f k8s/
```

## 🛠️ Desenvolvimento Local

### Pré-requisitos

- .NET 8 SDK
- RabbitMQ (local ou container)

### Executando o projeto

```bash
cd src/FiapCloudGames.Notifications.API
dotnet run
```

### Configuração local

Edite o arquivo `appsettings.Development.json` para ajustar as configurações de conexão com o RabbitMQ local.

## 📨 Eventos Consumidos

### UsuarioCriadoEvent

```json
{
  "usuarioId": "guid",
  "nome": "string",
  "email": "string"
}
```

### PagamentoProcessadoEvent

```json
{
  "pedidoId": "guid",
  "nomeUsuario": "string",
  "email": "string",
  "valor": 0.00,
  "status": "string"
}
```