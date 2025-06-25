# ?? Controle de Bar ??

Controle de opera��es di�rias de um bar, com gest�o de mesas, gar�ons, produtos e contas.

---

## ?? Sum�rio
- [Vis�o Geral](#vis%C3%A3o-geral)  
- [Funcionalidades](#funcionalidades)  
- [?? Requisitos](#requisitos)  
- [?? Pr�-requisitos](#pr%C3%A9-requisitos)  
- [?? Instala��o](#instala%C3%A7%C3%A3o)  
- [?? Uso](#uso)  
- [?? Contribui��o](#contribui%C3%A7%C3%A3o)  
- [?? Licen�a](#licen%C3%A7a)


---

## Vis�o Geral

Um sistema em C# (console ou web) para gerenciamento completo de um bar:
- Cadastro de mesas, gar�ons e produtos
- Abertura, modifica��o e fechamento de contas de clientes

---

## Funcionalidades

### M�dulo Mesas
- Criar, editar, excluir, listar e ver detalhes de mesas  
- Status: **Livre** / **Ocupada**  
- Regras:
  - N�mero (�nico, ??1)
  - Lugares (??1)
  - Status padr�o: Livre
  - Impede exclus�o se houver pedidos

### M�dulo Gar�ons
- Criar, editar, excluir e listar gar�ons  
- Regras:
  - Nome (3�100 caracteres)
  - CPF v�lido (formato XXX.XXX.XXX-XX)
  - CPF �nico
  - Impede exclus�o se houver pedidos

### M�dulo Produtos
- Criar, editar, excluir e listar produtos  
- Regras:
  - Nome (2�100 caracteres)
  - Pre�o (>?0, 2 decimais)
  - Nome �nico
  - Impede exclus�o se houver pedidos

### M�dulo Conta
- Abrir conta (cliente, mesa, gar�om)
- Adicionar/remover itens (produtos + quantidade)
- Visualizar faturamento di�rio, contas abertas e encerradas
- Fechar contas
- Regras:
  - Status: **Aberta** (padr�o) / **Fechada**
  - C�lculo autom�tico do total da conta e do faturamento di�rio
  - Uma conta ativa por mesa

---
## Tecnologias

[![Tecnologias](https://skillicons.dev/icons?i=cs,dotnet,visualstudio,git,github)](https://skillicons.dev)