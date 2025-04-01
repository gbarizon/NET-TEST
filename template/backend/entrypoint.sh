#!/bin/bash
set -e

echo "📦 Aplicando migrations..."
dotnet ef database update --project ../Ambev.DeveloperEvaluation.ORM --startup-project .

echo "✅ Banco atualizado. Iniciando API..."
exec dotnet run --project .