# Aplicação WEB de Agenda Telefônica

Esta é uma aplicação web de agenda telefônica que permite o cadastro e a pesquisa de contatos, incluindo informações como nome, idade e números de telefone. Os contatos podem ser pesquisados, alterados e excluídos, com log gerado após cada alteração.

## Funcionalidades

### Cadastro e Pesquisa de Contatos
- **Cadastro de Contato**: Permite incluir um novo contato com nome, idade e números de telefone.
- **Pesquisa de Contato**: Permite buscar contatos por nome ou número de telefone.
- **Alteração de Contato**: Permite alterar as informações de um contato selecionado.
- **Exclusão de Contato**: Permite excluir um contato e seus telefones associados.

### Telas
- **Cadastro de Contato**: Tela para adicionar um novo contato.
  - Botão para inclusão de contato.
  
- **Pesquisa de Contato**: Tela para pesquisa de contatos.
  - Botão para pesquisa dos contatos.
  - Botão para alterar o contato selecionado.
  - Botão para excluir o contato selecionado.
   
- **Telefones para Contato**: Cada contato pode ter qualquer número de telefones, cadastrados na tela do contato.

### Log
- Arquivo `.txt` com log gerado após cada exclusão, o caminho para geração do log pode ser alterado no appsettings.json, caminho "LogOutputPath":
```json
    "LogOutputPath": "C:\\Users\\User\\Documents\\logs"
``` 


---

## Documentação

### Banco de Dados

```sql
CREATE DATABASE AgendaTelefonica;
USE AgendaTelefonica;

CREATE TABLE Contato (
    ID BIGINT PRIMARY KEY AUTO_INCREMENT,
    NOME VARCHAR(100) NOT NULL,
    IDADE INT
);

CREATE TABLE Telefone (
    ID BIGINT PRIMARY KEY AUTO_INCREMENT,
    IDCONTATO BIGINT,
    NUMERO VARCHAR(16) NOT NULL,
    FOREIGN KEY (IDCONTATO) REFERENCES Contato(ID) ON DELETE CASCADE -- Cascade adicionado para, ao apagar o registro do contato, todos os telefones do mesmo sigam a deleção
);

-- Índices para otimização de performance com base nos campos utilizados para pesquisa
CREATE INDEX idx_nome_contato ON Contato (NOME);
CREATE INDEX idx_numero_telefone ON Telefone (NUMERO);

-- Inserção de dados iniciais
INSERT INTO Contato (NOME, IDADE) VALUES ("Túlio Silva", 24);
INSERT INTO Telefone (IDCONTATO, NUMERO) VALUES (1, "16993572303");
```

### Pacotes Instalados para o ASP.NET

- **Pomelo.EntityFrameworkCore.MySql**: Para suporte ao MySQL no Entity Framework Core.
- **Microsoft.EntityFrameworkCore.Design**: Ferramentas para design do Entity Framework.
- **Microsoft.EntityFrameworkCore.Tools**: Ferramentas de migração e scaffolding para o Entity Framework Core.

Para instalar os pacotes, execute os seguintes comandos:

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### Conexão com o Banco de Dados

Altere o arquivo `appsettings.json` para configurar a conexão com o seu banco de dados:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AgendaTelefonica;User=SeuUsuario;Password=SuaSenha;"
  }
}
```

---

## Instruções de Uso

1. Clone o repositório e abra o projeto no Visual Studio ou no Visual Studio Code.
2. Execute o comando para instalar as dependências:
   ```bash
   dotnet restore
   ```
3. Configure sua conexão com o banco de dados no arquivo `appsettings.json`.
4. Execute a aplicação usando o comando:
   ```bash
   dotnet run
   ```
5. Acesse a aplicação no navegador e utilize a interface para cadastrar, pesquisar, alterar e excluir contatos.

---

## Contribuição

Sinta-se à vontade para contribuir com melhorias, correções ou sugestões. Para isso, faça um fork do repositório, crie sua branch de funcionalidade, e envie um pull request com suas alterações.

---

## Licença

Este projeto é licenciado sob a [MIT License](LICENSE).