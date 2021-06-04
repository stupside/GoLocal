# GoLocal - Backend

#### Applications:
- Identity : https://localhost:5000
- Client : https://localhost:5001
- Artisan : https://localhost:5002

#### Documentations:
- Identity : https://localhost:5000/swagger/index.html
- Client : https://localhost:5001/swagger/index.html
- Artisan : https://localhost:5002/swagger/index.html

## Installation

#### Asp.net Core
##### This project have been made using ASP.NET CORE 5.0
- Install dotned-sdk.
```
https://dotnet.microsoft.com/download
```
- Open your shell and make sure dotnet-cli is reconized as a command.
```
dotnet
```
- If it's the case, install dotnef-ef so you can run the migration scripts.
```
dotnet tool install --global dotnet-ef
```

### Database
##### You will first have to install postgresql on your computer (you can also tweak the project to use something else... )
- Install postgres.
```
https://www.postgresql.org/
```
- You might want to install PgAdmin (GUI).
```
https://www.pgadmin.org/
```

#### Initialize your database:
- Go to Golocal.Core.Artisan.Api and run the migration script.
```
> migration.sh
> 1
> y
 Type your pgsql password and username ...
> y
```

- Go to Golocal.Identity.Api and run the migration script.
```
> migration.sh
> 1
> y
 Type your pgsql password and username ...
> y
```

## Run
1. Start the identity server
- Go to GoLocal.Identity.Api
```
dotnet run
```

2. Start the the client/artisan api
- Go to GoLocal.Core.Artisan.Api
```
dotnet run
```
- Go to GoLocal.Core.Client.Api
```
dotnet run
```
