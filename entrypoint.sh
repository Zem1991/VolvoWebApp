#!/bin/sh

echo "Waiting for SQL Server to be available..."
until nc -z db 1433; do
  echo "SQL Server not ready, retrying..."
  sleep 2
done

echo "Running EF Core migrations..."
dotnet ef database update --connection "Server=db;User Id=sa;Password=pa55w0rd!;TrustServerCertificate=True"

echo "Starting the web app..."
exec dotnet VolvoWebApp.dll