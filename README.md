# Space

## Инициализация проекта
* Создана структура решения `Backend.sln` и `Backend.csproj`.
* Настроен базовый файл `Program.cs`.
* Добавлен `.gitignore` для .NET проекта.
* Инициализирован Git-репозиторий.


### Создание минимальной структуры проекта
```bash
dotnet new sln -n Backend
dotnet new webapi -n Backend
dotnet sln Backend.sln add Backend/Backend.csproj
git init
dotnet new gitignore
```


## Подключение ORM Entity Framework Core для MySQL

* Установлен базовый пакет объектно-реляционного отображения `Microsoft.EntityFrameworkCore`.
* Добавлен провайдер `Pomelo.EntityFrameworkCore.MySql` для интеграции с базой данных Clever Cloud.

### Команды для установки пакетов (в папке Backend)
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Pomelo.EntityFrameworkCore.MySql
```