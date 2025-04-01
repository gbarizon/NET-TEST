#!/bin/bash
set -e

echo "🚀 Aguardando banco iniciar..."
sleep 5

echo "📦 Aplicando migrations..."
dotnet ef database update --project ../Ambev.DeveloperEvaluation.ORM --startup-project . --no-build

echo "✅ Banco atualizado. Iniciando API..."
exec dotnet Ambev.DeveloperEvaluation.WebApi.dll
