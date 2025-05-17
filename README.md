# EnterpriseManagement

# EnterpriseManagement

[![.NET](https://github.com/JohanRivera/EnterpriseManagement/actions/workflows/dotnet.yml/badge.svg)](https://github.com/JohanRivera/EnterpriseManagement/actions/workflows/dotnet.yml)

Repositorio para la gestión empresarial, que incluye módulos de usuarios, roles, productos, ventas, y más.

## Descripción

EnterpriseManagement es una aplicación backend desarrollada en ASP.NET Core 7 que implementa una arquitectura limpia con patrones como Repository y Unit of Work, además de incluir autenticación y autorización basada en ASP.NET Core Identity y JWT.

La aplicación permite la gestión completa de entidades empresariales con una API RESTful segura y escalable.

## Características principales

- Gestión de usuarios y roles con ASP.NET Core Identity.
- Autenticación con tokens JWT para asegurar la API.
- Arquitectura basada en capas: Controllers, Services, Repositories, Unit of Work.
- Uso de Entity Framework Core con SQL Server.
- Soporte para operaciones CRUD en entidades empresariales.
- Separación de responsabilidades para facilitar mantenimiento y escalabilidad.

## Tecnologías

- .NET 7
- ASP.NET Core Identity
- JWT Bearer Authentication
- Entity Framework Core (SQL Server)
- Repository Pattern + Unit of Work
- Swagger para documentación de API

---

## Configuración

### Requisitos previos

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- SQL Server o SQL Server Express
- Visual Studio 2022 / VS Code

### Variables de configuración

En el archivo `appsettings.json` agrega la configuración para JWT:

```json
"Jwt": {
  "Key": "EstaEsUnaClaveSuperSecretaYLarga1234567890",
  "Issuer": "EnterpriseManagement",
  "Audience": "EnterpriseManagementUsers"
},
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=EnterpriseManagementDb;Trusted_Connection=True;"
}
```

## Cómo ejecutar el proyecto

1. Clona el repositorio:

```bash
git clone https://github.com/JohanRivera/EnterpriseManagement.git
cd EnterpriseManagement
git checkout develop
```

2. Restaura los paquetes

```bash
dotnet restore
```

3. Aplica migraciones

```bash
dotnet ef database update
```

4. Ejecuta
```bash
dotnet run
```

