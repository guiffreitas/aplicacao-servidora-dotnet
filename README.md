# Aplicação Servidora .NET

Esta é uma API construída em .NET 8 com fins didáticos.
Ela implementa autenticação e autorização usando o padrão JWT Bearer, e possui possui um único endpoint `GetWeatherForecast`.
Os serviços de validação do token foram implementados para uso com a plataforma Microsoft Entra ID.

O passo a passo de cadastros e configurações na plataforma Entra ID e na API servidora, estão descritos no artigo abaixo.

[Autenticação e Autorização entre APIs .NET usando Microsoft Entra ID](https://medium.com/@ffaria.gui/autoriza%C3%A7%C3%A3o-e-autentica%C3%A7%C3%A3o-entre-apis-net-usando-microsoft-entra-id-9e6e1d113ef0)

## Tecnologias Utilizadas

- .NET 8
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft Entra ID

## Pré-requisitosa

- .NET 8 SDK
- IDE (Visual Studio, Visual Studio Code, etc.)
- Conta na Microsoft Entra ID

## Configuração
Será necessário adicionar ao repositório um arquivo appsettings.json com os IDs referentes ao diretório da sua organização no Entra ID.
```
{
  "AllowedHosts": "*",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "EntraID": {
    "TenantId": "<tenant-id>",
    "Servidor": {
      "Id": "<application-id-servidor>"
    },
    "Consumidor": {
      "Id": "<application-id-consumidor>"
    },
    "Swagger": {
      "Id": "<application-id-swagger>",
      "Escopo": "api://<application-id-servidor>/swagger_access"
    }
  }
}
```
## Rodando o projeto
### Clone o repositório
```bash
git clone https://github.com/guiffreitas/aplicacao-servidora-dotnet.git
```
### Rode o comando no terminal
```
dotnet run --lauch-profile "aplicacao-servidora-dotnet"
```
