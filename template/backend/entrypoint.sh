#!/bin/bash
set -e

echo "🧪 Connection string from env: $ConnectionStrings__DefaultConnection"

echo "⏳ Verificando disponibilidade do banco..."
until nc -z ambev.developerevaluation.database 5432; do
  echo "⏳ Aguardando PostgreSQL..."
  sleep 1
done

echo "📦 Aplicando migrations..."
dotnet ef database update --project ../Ambev.DeveloperEvaluation.ORM --startup-project .

echo "✅ Banco atualizado. Iniciando API..."
exec dotnet run --project .