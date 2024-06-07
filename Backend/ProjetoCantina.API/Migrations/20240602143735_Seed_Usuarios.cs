using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoCantina.API.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Usuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert Into usuario (NomeUsuario, Senha) Values ('Admin', '1000:tjbnsKDpQjwvRzCAUSe8fZ0Ji5GXiftW:J9LwCORvpFcPEH+CxtHMM0tTtZ69phrn')");

            mb.Sql("Insert Into usuario (NomeUsuario, Senha) Values ('User', '1000:AibR9zuSyJozfqY4sfw58zuAV+9Hms0W:qWznof7A35v/LYqXH/TXF4RRVEzCqE1')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete From usuario Where UsuarioID > 0");
        }
    }
}
