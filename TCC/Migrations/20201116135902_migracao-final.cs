using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCC.Migrations
{
    public partial class migracaofinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fruta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Benefício1 = table.Column<string>(nullable: false),
                    Benefício2 = table.Column<string>(nullable: false),
                    Benefício3 = table.Column<string>(nullable: false),
                    Benefício4 = table.Column<string>(nullable: true),
                    Benefício5 = table.Column<string>(nullable: true),
                    Benefício6 = table.Column<string>(nullable: true),
                    Foto = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fruta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Título = table.Column<string>(nullable: false),
                    Ingredientes = table.Column<string>(nullable: false),
                    Preparo = table.Column<string>(nullable: false),
                    FotoRe = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fruta");

            migrationBuilder.DropTable(
                name: "Receita");
        }
    }
}
