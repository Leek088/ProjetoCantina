1) Tenha o dotnet ef instalado no seu ambiente.
2) Configure a sua conexão para algum banco Mysql, em appsettings.json do projeto API.
3) Execute o migration, com o comando do ef: dotnet ef database update
4) Após o comando, será criado o banco e dados e registros de teste, nas tabelas.
5) Será criado dois usuários de teste, nome: "Admin", senha: "123456" e "User", senha: "10203040".
   No banco, a senha está criptografada.
