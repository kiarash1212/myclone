# Grade Book

## Objective
Create a database-driven Grade Book API that will allow for CRUD operations on multiple database tables using .NET and the Entity Framework.

## Run

```powershell
dotnet restore
dotnet run
```

The API uses `GradeBook.sqlite` in the project root via EF Core SQLite.

## Endpoints

- `GET /api/students`
- `GET /api/students/{id}`
- `GET /api/students/{id}/grades`
- `POST /api/students`
- `PUT /api/students/{id}`
- `DELETE /api/students/{id}`

- `GET /api/assignments`
- `GET /api/assignments/{id}`
- `GET /api/assignments/{id}/grades`
- `POST /api/assignments`
- `PUT /api/assignments/{id}`
- `DELETE /api/assignments/{id}`

- `GET /api/grades`
- `GET /api/grades/{id}`
- `POST /api/grades`
- `PUT /api/grades/{id}`
- `DELETE /api/grades/{id}`
