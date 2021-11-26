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
                name: "UserReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReport_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilesVersion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserReportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilesVersion_UserReport_UserReportId",
                        column: x => x.UserReportId,
                        principalTable: "UserReport",
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
                name: "ExpenseSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpYear = table.Column<int>(type: "int", nullable: false),
                    DirectorSalary = table.Column<double>(type: "float", nullable: false),
                    EmployeeSalary = table.Column<double>(type: "float", nullable: false),
                    LocumCost = table.Column<double>(type: "float", nullable: false),
                    OtherCost = table.Column<double>(type: "float", nullable: false),
                    Rent = table.Column<double>(type: "float", nullable: false),
                    Rates = table.Column<double>(type: "float", nullable: false),
                    Utilities = table.Column<double>(type: "float", nullable: false),
                    Telephone = table.Column<double>(type: "float", nullable: false),
                    Repair = table.Column<double>(type: "float", nullable: false),
                    Communication = table.Column<double>(type: "float", nullable: false),
                    Leasing = table.Column<double>(type: "float", nullable: false),
                    Insurance = table.Column<double>(type: "float", nullable: false),
                    ProIndemnity = table.Column<double>(type: "float", nullable: false),
                    ComputerIt = table.Column<double>(type: "float", nullable: false),
                    Recruitment = table.Column<double>(type: "float", nullable: false),
                    RegistrationFee = table.Column<double>(type: "float", nullable: false),
                    Marketing = table.Column<double>(type: "float", nullable: false),
                    Travel = table.Column<double>(type: "float", nullable: false),
                    Entertainment = table.Column<double>(type: "float", nullable: false),
                    Transport = table.Column<double>(type: "float", nullable: false),
                    Accountancy = table.Column<double>(type: "float", nullable: false),
                    Banking = table.Column<double>(type: "float", nullable: false),
                    Interest = table.Column<double>(type: "float", nullable: false),
                    OtherExpense = table.Column<double>(type: "float", nullable: false),
                    Amortalisation = table.Column<double>(type: "float", nullable: false),
                    Depreciation = table.Column<double>(type: "float", nullable: false),
                    FilesVersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseSummary_FilesVersion_FilesVersionId",
                        column: x => x.FilesVersionId,
                        principalTable: "FilesVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MurYear = table.Column<int>(type: "int", nullable: false),
                    TotalMur = table.Column<int>(type: "int", nullable: false),
                    FilesVersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mur_FilesVersion_FilesVersionId",
                        column: x => x.FilesVersionId,
                        principalTable: "FilesVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescMonth = table.Column<int>(type: "int", nullable: false),
                    PrescItems = table.Column<int>(type: "int", nullable: false),
                    PrescAvgItem = table.Column<int>(type: "int", nullable: false),
                    FilesVersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionSummary_FilesVersion_FilesVersionId",
                        column: x => x.FilesVersionId,
                        principalTable: "FilesVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesYear = table.Column<int>(type: "int", nullable: false),
                    ZeroRatedOTCSale = table.Column<int>(type: "int", nullable: false),
                    VATExclusiveOTCSale = table.Column<int>(type: "int", nullable: false),
                    FilesVersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesSummary_FilesVersion_FilesVersionId",
                        column: x => x.FilesVersionId,
                        principalTable: "FilesVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VersionSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartYear = table.Column<int>(type: "int", nullable: false),
                    NoYear = table.Column<int>(type: "int", nullable: false),
                    VolumeDecrease = table.Column<double>(type: "float", nullable: false),
                    InflationRate = table.Column<double>(type: "float", nullable: false),
                    FilesVersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VersionSetting_FilesVersion_FilesVersionId",
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
                    Dispensing_Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    OCS_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Net_Payment_Made = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dispensing_Year = table.Column<int>(type: "int", nullable: false),
                    Dispensing_Month = table.Column<int>(type: "int", nullable: false),
                    Net_Payment = table.Column<double>(type: "float", nullable: false),
                    Total_Drug = table.Column<double>(type: "float", nullable: false),
                    Total_Fees = table.Column<double>(type: "float", nullable: false),
                    Total_Costs_wFees = table.Column<double>(type: "float", nullable: false),
                    Total_Charges = table.Column<double>(type: "float", nullable: false),
                    Total_Account = table.Column<double>(type: "float", nullable: false),
                    Recovery_Adv_Payment = table.Column<double>(type: "float", nullable: false),
                    Recovery_Adv_Payment_Late_Registered = table.Column<double>(type: "float", nullable: false),
                    Balance_Due_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance_Due = table.Column<double>(type: "float", nullable: false),
                    Account_Number = table.Column<int>(type: "int", nullable: false),
                    Account_Item = table.Column<int>(type: "int", nullable: false),
                    Payment_Account = table.Column<double>(type: "float", nullable: false),
                    Adv_Payment_Late = table.Column<double>(type: "float", nullable: false),
                    Total_Authorised = table.Column<double>(type: "float", nullable: false),
                    Total_Authorised_LPP = table.Column<double>(type: "float", nullable: false),
                    Total_Other = table.Column<double>(type: "float", nullable: false),
                    Total_Price_Standrt_Disc = table.Column<double>(type: "float", nullable: false),
                    Discount_Percent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Total_Price_Zero_Disc = table.Column<double>(type: "float", nullable: false),
                    Sub_Total_Price = table.Column<double>(type: "float", nullable: false),
                    Out_Pocket_Expenses = table.Column<double>(type: "float", nullable: false),
                    Pay_Consumable = table.Column<double>(type: "float", nullable: false),
                    Pay_Consumable_Per_Item = table.Column<double>(type: "float", nullable: false),
                    Pay_Container = table.Column<double>(type: "float", nullable: false),
                    Total_Costs = table.Column<double>(type: "float", nullable: false),
                    Presc_Activity_Pay = table.Column<double>(type: "float", nullable: false),
                    Presc_Activity_Pay_Per_Item = table.Column<double>(type: "float", nullable: false),
                    Presc_AddFee_2A_Unlicensed_Meds = table.Column<double>(type: "float", nullable: false),
                    Presc_AddFee_2B_Appliance_Measure = table.Column<double>(type: "float", nullable: false),
                    Presc_AddFee_2B_Appliance_Home = table.Column<double>(type: "float", nullable: false),
                    Presc_AddFee_2E_Controlled = table.Column<double>(type: "float", nullable: false),
                    Presc_AddFee_Methadone_Pay = table.Column<double>(type: "float", nullable: false),
                    Presc_AddFee_2F_Expensive_Fees = table.Column<double>(type: "float", nullable: false),
                    Presc_AddFee_2F_Expensive_Fee_Item = table.Column<int>(type: "int", nullable: false),
                    Presc_AddFee_Manual_Price = table.Column<double>(type: "float", nullable: false),
                    Transitional_Pay = table.Column<double>(type: "float", nullable: false),
                    Sub_Total_Presc_Fee = table.Column<double>(type: "float", nullable: false),
                    Other_Fee_Medicine = table.Column<double>(type: "float", nullable: false),
                    Other_Fee_Appliance_Patient = table.Column<double>(type: "float", nullable: false),
                    Other_Fee_Appliance_Premise = table.Column<double>(type: "float", nullable: false),
                    Other_Fee_Stoma_Custom = table.Column<double>(type: "float", nullable: false),
                    Other_Fee_Medicine_Service = table.Column<double>(type: "float", nullable: false),
                    Total_All_Fees = table.Column<double>(type: "float", nullable: false),
                    Charges_Collected_Excl_Hosiery_1 = table.Column<double>(type: "float", nullable: false),
                    Charges_Collected_Excl_Hosiery_1_Items = table.Column<int>(type: "int", nullable: false),
                    Charges_Collected_Excl_Hosiery_1_Per_Item = table.Column<double>(type: "float", nullable: false),
                    Charges_Collected_Excl_Hosiery_2 = table.Column<double>(type: "float", nullable: false),
                    Charges_Collected_Excl_Hosiery_2_Items = table.Column<int>(type: "int", nullable: false),
                    Charges_Collected_Excl_Hosiery_2_Per_Item = table.Column<double>(type: "float", nullable: false),
                    Charges_Collected_Elastic_Hosiery = table.Column<double>(type: "float", nullable: false),
                    Charges_FP57_Refund = table.Column<double>(type: "float", nullable: false),
                    Local_Scheme = table.Column<double>(type: "float", nullable: false),
                    LPC_Statutory_Levy = table.Column<double>(type: "float", nullable: false),
                    COVID_Premises_Refrigeration = table.Column<double>(type: "float", nullable: false),
                    Reimbursement_Covid_Costs = table.Column<double>(type: "float", nullable: false),
                    Total_Forms_Received = table.Column<int>(type: "int", nullable: false),
                    Total_Electronic_Prescription_Received = table.Column<int>(type: "int", nullable: false),
                    Total_Electronic_Prescription_Items = table.Column<int>(type: "int", nullable: false),
                    Items_Zero_Disc = table.Column<int>(type: "int", nullable: false),
                    Items_Standard_Disc = table.Column<int>(type: "int", nullable: false),
                    Total_Paid_Fee_Items = table.Column<int>(type: "int", nullable: false),
                    Avg_Item_Value = table.Column<double>(type: "float", nullable: false),
                    Referred_Back_Items = table.Column<int>(type: "int", nullable: false),
                    Referred_Back_Forms = table.Column<int>(type: "int", nullable: false),
                    Medicines_Reviews_Declared = table.Column<int>(type: "int", nullable: false),
                    YTD_MUR_Declaration = table.Column<int>(type: "int", nullable: false),
                    FP57_Declared = table.Column<int>(type: "int", nullable: false),
                    Appliance_Reviews_Carried_Patients_Home = table.Column<int>(type: "int", nullable: false),
                    Appliance_Reviews_Carried_Premises = table.Column<int>(type: "int", nullable: false),
                    New_Medicine_Service_Undertaken = table.Column<int>(type: "int", nullable: false),
                    New_Medicine_Service_Items = table.Column<int>(type: "int", nullable: false),
                    Exempt_Chargeable = table.Column<int>(type: "int", nullable: false),
                    Exempt_Chargeable_Old_Rate = table.Column<int>(type: "int", nullable: false),
                    Chargeable_Exempt = table.Column<int>(type: "int", nullable: false),
                    Chargeable_Old_Rate_Exempt = table.Column<int>(type: "int", nullable: false),
                    Items_Over_100 = table.Column<int>(type: "int", nullable: false),
                    Items_Over_100_Basic_Price = table.Column<double>(type: "float", nullable: false),
                    Items_Over_300 = table.Column<int>(type: "int", nullable: false),
                    Items_Over_300_Basic_Price = table.Column<double>(type: "float", nullable: false),
                    Total_Items_Over_100 = table.Column<int>(type: "int", nullable: false),
                    Total_Items_Over_100_Basic_Price = table.Column<double>(type: "float", nullable: false),
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
                name: "IX_ExpenseSummary_FilesVersionId",
                table: "ExpenseSummary",
                column: "FilesVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesVersion_UserReportId",
                table: "FilesVersion",
                column: "UserReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Mur_FilesVersionId",
                table: "Mur",
                column: "FilesVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DocsId",
                table: "Prescriptions",
                column: "DocsId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionSummary_FilesVersionId",
                table: "PrescriptionSummary",
                column: "FilesVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListHistory_DocsId",
                table: "PriceListHistory",
                column: "DocsId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesSummary_FilesVersionId",
                table: "SalesSummary",
                column: "FilesVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleOfPayments_DocsId",
                table: "ScheduleOfPayments",
                column: "DocsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReport_AppUserId",
                table: "UserReport",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VersionSetting_FilesVersionId",
                table: "VersionSetting",
                column: "FilesVersionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseSummary");

            migrationBuilder.DropTable(
                name: "Mur");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "PrescriptionSummary");

            migrationBuilder.DropTable(
                name: "PriceListHistory");

            migrationBuilder.DropTable(
                name: "SalesSummary");

            migrationBuilder.DropTable(
                name: "ScheduleOfPayments");

            migrationBuilder.DropTable(
                name: "VersionSetting");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "FilesVersion");

            migrationBuilder.DropTable(
                name: "UserReport");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
