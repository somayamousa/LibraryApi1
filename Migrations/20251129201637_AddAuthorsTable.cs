using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApi1.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_Books_BookId",
                table: "Borrow");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_Users_UserId",
                table: "Borrow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Borrow",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Borrow",
                newName: "Borrows");

            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "BorrowRecords",
                newName: "ReturnedAt");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "BorrowRecords",
                newName: "DueAt");

            migrationBuilder.RenameColumn(
                name: "BorrowDate",
                table: "BorrowRecords",
                newName: "BorrowedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Borrow_UserId",
                table: "Borrows",
                newName: "IX_Borrows_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrow_BookId",
                table: "Borrows",
                newName: "IX_Borrows_BookId");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "BorrowRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicationYear",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Borrows",
                table: "Borrows",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Books_BookId",
                table: "Borrows",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Users_UserId",
                table: "Borrows",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Books_BookId",
                table: "Borrows");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Users_UserId",
                table: "Borrows");

            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Borrows",
                table: "Borrows");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BorrowRecords");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublicationYear",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Borrows",
                newName: "Borrow");

            migrationBuilder.RenameColumn(
                name: "ReturnedAt",
                table: "BorrowRecords",
                newName: "ReturnDate");

            migrationBuilder.RenameColumn(
                name: "DueAt",
                table: "BorrowRecords",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "BorrowedAt",
                table: "BorrowRecords",
                newName: "BorrowDate");

            migrationBuilder.RenameIndex(
                name: "IX_Borrows_UserId",
                table: "Borrow",
                newName: "IX_Borrow_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrows_BookId",
                table: "Borrow",
                newName: "IX_Borrow_BookId");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Borrow",
                table: "Borrow",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_Books_BookId",
                table: "Borrow",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_Users_UserId",
                table: "Borrow",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
