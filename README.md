# FootballPlayerManagerApi

FootballPlayerManagerApi is a simple WebApi project.

### Tech
* ASP.NET Core 7.0
* Couchbase Server Enterprise Edition 7.1.3

## About Database
### Setup Couchbase DB
To Setup Couchbase, Docker must be installed.

Run this command in the root folder to build docker image that prepared for this project.
```bash
docker build -t couchbase-dev .
```

Run this command to run container.
```bash
docker run -d --name cb-server -p 8091-8094:8091-8094 -p 11210:11210 -e COUCHBASE_ADMINISTRATOR_USERNAME=Administrator -e COUCHBASE_ADMINISTRATOR_PASSWORD=password -e COUCHBASE_BUCKET=football-bucket -e COUCHBASE_RBAC_USERNAME=admin -e COUCHBASE_RBAC_PASSWORD=password -e COUCHBASE_RBAC_NAME="admin" -e CLUSTER_NAME=FootballCluster couchbase-dev
```

Run this command to check if there "permission denied" error about configure-server.sh
```bash
docker logs -f cb-server 
```

Run this command to solve "permission denied" error about configure-server.sh on macOS.
```bash
chmod +x configure-server.sh
```
* On Windows, should be given write permission manually.

After the setup, open http://localhost:8091/ui/index.html to see the login page of Couchbase.  
username: Administrator  
password: password

### DB Structure

Example of a "Team" object
```json
{
  "name": "Chelsea FC",
  "playerIds": [
    "david_de_gea",
    "mason_mount"
  ]
}
```

Example of a "Player" object
```json
{
  "name": "David de Gea",
  "height": 192,
  "age": 32
}
```

### Seed Data On Start

When the project started, sample data was seeded by CBSeederHostedService. To disable this, remove this line from Program.cs

```csharp
builder.Services.AddHostedService<CBSeederHostedService>();
```

## EndPoints
*  GET /players/{id}
*  PUT /players/{id}
*  GET /teams/{id}/players
*  PUT /teams/{id}/player/{playerId}
*  DELETE /teams/{id}/player/{playerId}

### Swagger UI
After running the project move http://localhost:5185/swagger/index.html to open Swagger UI. There you can explore endpoints with HTTP codes.

### Postman Collection
Football Player Manager Api Documentation.postman_collection.json added to the root folder to import endpoints to Postman easily.

When the project started, sample data was seeded by CBSeederHostedService. To disable this, remove this line from Program.cs

```csharp
builder.Services.AddHostedService<CBSeederHostedService>();
```
