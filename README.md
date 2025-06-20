# VolvoWebApp
This is a programming exercise required for admittance at Volvo, for a position as Professional Software Engineer.
Made using:
- .NET 8 for the MVC
- EntityFramework Core (for SQL Server)
- AutoMapper
- Bootstrap
- Fontawesome
- xUnit
- Moq
- Docker (with Linux images)

## Instructions
1. Download/clone this repository, then run it with Docker. You can quickly create everything with this command: 
docker-compose up --build -d
2. When you get your container running, you can access the WebApp from
http://localhost:8080/
3. Viewing records can be done without loging in, but creating/updating/deleting will require registering first. Only an email and password is needed, and these will be stored within the project's own database.

### Why Docker? Why not just use a Microsoft LocalDb?
When I tested everything in my other computer, I ran into many issues regarding having an already present LocalDb instance. That wouldn't be suitable for a job interview test.
So I resorted into placing everything in containers, which also became more work than it should but it ended up working anyway.

## About
All the required features and business rules were implemented. The only extras would be:
- Very basic Authorization/Registering (already described)
- Additional fields for Id, Created Date and Last Update
- Records sorted by Last Update (descending order)
- Record deletion
- Containerizing everything
