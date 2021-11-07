using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class LatestTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilesVersion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilesVersion_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilesVersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_FilesVersion_FilesVersionId",
                        column: x => x.FilesVersionId,
                        principalTable: "FilesVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OCS_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dispensing_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fragment_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item_Number = table.Column<int>(type: "int", nullable: false),
                    Element_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prescriber_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group_Type_Declared = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Charge_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Charges_Payable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Snomed_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit_Of_Measure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pack_Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pack_Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Basic_Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payment_for_Consumables = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payment_for_Containers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DA_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EX_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LP_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MC_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MI_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MP_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NC_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ND = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RB_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SF_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDR_Professional_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDR_Professional_Fee_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZDR_Professional_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZDR_Professional_Fee_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SP_Unlicensed_Meds_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ED_Unlicensed_Meds_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MF_Hosiery_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MF_Truss_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MF_Belt_and_Girdle_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Methadone_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Methadone_Pckgd_Dose_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Home_Del_SR_Appl_Add_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Home_Del_HR_Appl_Add_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CD_Schedule_2_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CD_Schedule_3_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expensive_Item_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stoma_Customisation_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dispensing_UID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NHS_Patient_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SSP_Vat_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SSP_Fee_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Documents_DocsId",
                        column: x => x.DocsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceListHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceListsID = table.Column<int>(type: "int", nullable: false),
                    SupplierCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AMPPID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VMPPID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceListHistory_Documents_DocsId",
                        column: x => x.DocsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleOfPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dispensing_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NHS_Sales = table.Column<double>(type: "float", nullable: false),
                    DocsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleOfPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleOfPayments_Documents_DocsId",
                        column: x => x.DocsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_FilesVersionId",
                table: "Documents",
                column: "FilesVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesVersion_AppUserId",
                table: "FilesVersion",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DocsId",
                table: "Prescriptions",
                column: "DocsId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListHistory_DocsId",
                table: "PriceListHistory",
                column: "DocsId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleOfPayments_DocsId",
                table: "ScheduleOfPayments",
                column: "DocsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "PriceListHistory");

            migrationBuilder.DropTable(
                name: "ScheduleOfPayments");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "FilesVersion");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
