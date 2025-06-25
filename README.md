# ?? Controle de Bar ??

Controle de operações diárias de um bar, com gestão de mesas, garçons, produtos e contas.

---

## ?? Sumário
- [Visão Geral](#vis%C3%A3o-geral)  
- [Funcionalidades](#funcionalidades)  
- [?? Requisitos](#requisitos)  
- [?? Pré-requisitos](#pr%C3%A9-requisitos)  
- [?? Instalação](#instala%C3%A7%C3%A3o)  
- [?? Uso](#uso)  
- [?? Contribuição](#contribui%C3%A7%C3%A3o)  
- [?? Licença](#licen%C3%A7a)


---

## Visão Geral

Um sistema em C# (console ou web) para gerenciamento completo de um bar:
- Cadastro de mesas, garçons e produtos
- Abertura, modificação e fechamento de contas de clientes

---

## Funcionalidades

### Módulo Mesas
- Criar, editar, excluir, listar e ver detalhes de mesas  
- Status: **Livre** / **Ocupada**  
- Regras:
  - Número (único, ??1)
  - Lugares (??1)
  - Status padrão: Livre
  - Impede exclusão se houver pedidos

### Módulo Garçons
- Criar, editar, excluir e listar garçons  
- Regras:
  - Nome (3–100 caracteres)
  - CPF válido (formato XXX.XXX.XXX-XX)
  - CPF único
  - Impede exclusão se houver pedidos

### Módulo Produtos
- Criar, editar, excluir e listar produtos  
- Regras:
  - Nome (2–100 caracteres)
  - Preço (>?0, 2 decimais)
  - Nome único
  - Impede exclusão se houver pedidos

### Módulo Conta
- Abrir conta (cliente, mesa, garçom)
- Adicionar/remover itens (produtos + quantidade)
- Visualizar faturamento diário, contas abertas e encerradas
- Fechar contas
- Regras:
  - Status: **Aberta** (padrão) / **Fechada**
  - Cálculo automático do total da conta e do faturamento diário
  - Uma conta ativa por mesa

---
## Tecnologias

[![Tecnologias](https://skillicons.dev/icons?i=cs,dotnet,visualstudio,git,github)](https://skillicons.dev)