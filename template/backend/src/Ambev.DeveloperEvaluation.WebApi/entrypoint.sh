#!/bin/bash
set -e

echo "🚀 Esperando o banco iniciar..."
sleep 5

echo "🗃️ Rodando EF Core Migrations..."
dotnet ef database update --project ../Ambev.DeveloperEvaluation.ORM --startup-project . --no-build

echo "✅ Banco atualizado. Iniciando a aplicação..."
exec dotnet Ambev.DeveloperEvaluation.WebApi.dll