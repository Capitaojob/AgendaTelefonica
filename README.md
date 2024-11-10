# Aplica��o WEB de Agenda Telef�nica

Esta � uma aplica��o web de agenda telef�nica que permite o cadastro e a pesquisa de contatos, incluindo informa��es como nome, idade e n�meros de telefone. Os contatos podem ser pesquisados, alterados e exclu�dos, com log gerado ap�s cada altera��o.

## Funcionalidades

### Cadastro e Pesquisa de Contatos
- **Cadastro de Contato**: Permite incluir um novo contato com nome, idade e n�meros de telefone.
- **Pesquisa de Contato**: Permite buscar contatos por nome ou n�mero de telefone.
- **Altera��o de Contato**: Permite alterar as informa��es de um contato selecionado.
- **Exclus�o de Contato**: Permite excluir um contato e seus telefones associados.

### Telas
- **Cadastro de Contato**: Tela para adicionar um novo contato.
  - Bot�o para inclus�o de contato.
  
- **Pesquisa de Contato**: Tela para pesquisa de contatos.
  - Bot�o para pesquisa dos contatos.
  - Bot�o para alterar o contato selecionado.
  - Bot�o para excluir o contato selecionado.
   
- **Telefones para Contato**: Cada contato pode ter qualquer n�mero de telefones, cadastrados na tela do contato.

### Log
- Arquivo `.txt` com log gerado ap�s cada exclus�o, o caminho para gera��o do log pode ser alterado no appsettings.json, caminho "LogOutputPath":
```json
    "LogOutputPath": "C:\\Users\\User\\Documents\\logs"
``` 


---

## Documenta��o

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
    FOREIGN KEY (IDCONTATO) REFERENCES Contato(ID) ON DELETE CASCADE -- Cascade adicionado para, ao apagar o registro do contato, todos os telefones do mesmo sigam a dele��o
);

-- �ndices para otimiza��o de performance com base nos campos utilizados para pesquisa
CREATE INDEX idx_nome_contato ON Contato (NOME);
CREATE INDEX idx_numero_telefone ON Telefone (NUMERO);

-- Inser��o de dados iniciais
INSERT INTO Contato (NOME, IDADE) VALUES ("T�lio Silva", 24);
INSERT INTO Telefone (IDCONTATO, NUMERO) VALUES (1, "16993572303");
```

### Pacotes Instalados para o ASP.NET

- **Pomelo.EntityFrameworkCore.MySql**: Para suporte ao MySQL no Entity Framework Core.
- **Microsoft.EntityFrameworkCore.Design**: Ferramentas para design do Entity Framework.
- **Microsoft.EntityFrameworkCore.Tools**: Ferramentas de migra��o e scaffolding para o Entity Framework Core.

Para instalar os pacotes, execute os seguintes comandos:

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### Conex�o com o Banco de Dados

Altere o arquivo `appsettings.json` para configurar a conex�o com o seu banco de dados:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AgendaTelefonica;User=SeuUsuario;Password=SuaSenha;"
  }
}
```

---

## Instru��es de Uso

1. Clone o reposit�rio e abra o projeto no Visual Studio ou no Visual Studio Code.
2. Execute o comando para instalar as depend�ncias:
   ```bash
   dotnet restore
   ```
3. Configure sua conex�o com o banco de dados no arquivo `appsettings.json`.
4. Execute a aplica��o usando o comando:
   ```bash
   dotnet run
   ```
5. Acesse a aplica��o no navegador e utilize a interface para cadastrar, pesquisar, alterar e excluir contatos.

---

## Contribui��o

Sinta-se � vontade para contribuir com melhorias, corre��es ou sugest�es. Para isso, fa�a um fork do reposit�rio, crie sua branch de funcionalidade, e envie um pull request com suas altera��es.

---

## Licen�a

Este projeto � licenciado sob a [MIT License](LICENSE).