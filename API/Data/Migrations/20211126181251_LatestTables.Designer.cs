// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211126181251_LatestTables")]
    partial class LatestTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Entities.Docs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FilesVersionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FilesVersionId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("API.Entities.ExpenseSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Accountancy")
                        .HasColumnType("float");

                    b.Property<double>("Amortalisation")
                        .HasColumnType("float");

                    b.Property<double>("Banking")
                        .HasColumnType("float");

                    b.Property<double>("Communication")
                        .HasColumnType("float");

                    b.Property<double>("ComputerIt")
                        .HasColumnType("float");

                    b.Property<double>("Depreciation")
                        .HasColumnType("float");

                    b.Property<double>("DirectorSalary")
                        .HasColumnType("float");

                    b.Property<double>("EmployeeSalary")
                        .HasColumnType("float");

                    b.Property<double>("Entertainment")
                        .HasColumnType("float");

                    b.Property<int>("ExpYear")
                        .HasColumnType("int");

                    b.Property<int>("FilesVersionId")
                        .HasColumnType("int");

                    b.Property<double>("Insurance")
                        .HasColumnType("float");

                    b.Property<double>("Interest")
                        .HasColumnType("float");

                    b.Property<double>("Leasing")
                        .HasColumnType("float");

                    b.Property<double>("LocumCost")
                        .HasColumnType("float");

                    b.Property<double>("Marketing")
                        .HasColumnType("float");

                    b.Property<double>("OtherCost")
                        .HasColumnType("float");

                    b.Property<double>("OtherExpense")
                        .HasColumnType("float");

                    b.Property<double>("ProIndemnity")
                        .HasColumnType("float");

                    b.Property<double>("Rates")
                        .HasColumnType("float");

                    b.Property<double>("Recruitment")
                        .HasColumnType("float");

                    b.Property<double>("RegistrationFee")
                        .HasColumnType("float");

                    b.Property<double>("Rent")
                        .HasColumnType("float");

                    b.Property<double>("Repair")
                        .HasColumnType("float");

                    b.Property<double>("Telephone")
                        .HasColumnType("float");

                    b.Property<double>("Transport")
                        .HasColumnType("float");

                    b.Property<double>("Travel")
                        .HasColumnType("float");

                    b.Property<double>("Utilities")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FilesVersionId");

                    b.ToTable("ExpenseSummary");
                });

            modelBuilder.Entity("API.Entities.FilesVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserReportId")
                        .HasColumnType("int");

                    b.Property<string>("VersionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserReportId");

                    b.ToTable("FilesVersion");
                });

            modelBuilder.Entity("API.Entities.Mur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FilesVersionId")
                        .HasColumnType("int");

                    b.Property<int>("MurYear")
                        .HasColumnType("int");

                    b.Property<int>("TotalMur")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FilesVersionId");

                    b.ToTable("Mur");
                });

            modelBuilder.Entity("API.Entities.PrescriptionSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FilesVersionId")
                        .HasColumnType("int");

                    b.Property<int>("PrescAvgItem")
                        .HasColumnType("int");

                    b.Property<int>("PrescItems")
                        .HasColumnType("int");

                    b.Property<int>("PrescMonth")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FilesVersionId");

                    b.ToTable("PrescriptionSummary");
                });

            modelBuilder.Entity("API.Entities.Prescriptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Basic_Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CD_Schedule_2_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CD_Schedule_3_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Charge_Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Charges_Payable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DA_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dispensing_Month")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dispensing_UID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dispensing_Year")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocsId")
                        .HasColumnType("int");

                    b.Property<string>("ED_Unlicensed_Meds_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EX")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EX_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Element_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Expensive_Item_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form_Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fragment_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Group_Type_Declared")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Home_Del_HR_Appl_Add_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Home_Del_SR_Appl_Add_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IP_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Item_Number")
                        .HasColumnType("int");

                    b.Property<string>("LB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LP_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MC_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MF_Belt_and_Girdle_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MF_Hosiery_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MF_Truss_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MI_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MP_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Methadone_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Methadone_Pckgd_Dose_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NC_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ND")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NHS_Patient_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OCS_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pack_Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pack_Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Payment_for_Consumables")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Payment_for_Containers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prescriber_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Product_Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RB_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDR_Professional_Fee_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDR_Professional_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SF_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SP_Unlicensed_Meds_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SSP_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SSP_Vat_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Snomed_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stoma_Customisation_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit_Of_Measure")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZDR_Professional_Fee_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZDR_Professional_Fee_Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocsId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("API.Entities.PriceListHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AMPPID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdd")
                        .HasColumnType("datetime2");

                    b.Property<int>("DocsId")
                        .HasColumnType("int");

                    b.Property<string>("PIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("PriceListsID")
                        .HasColumnType("int");

                    b.Property<string>("Product")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VMPPID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocsId");

                    b.ToTable("PriceListHistory");
                });

            modelBuilder.Entity("API.Entities.SalesSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FilesVersionId")
                        .HasColumnType("int");

                    b.Property<int>("SalesYear")
                        .HasColumnType("int");

                    b.Property<int>("VATExclusiveOTCSale")
                        .HasColumnType("int");

                    b.Property<int>("ZeroRatedOTCSale")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FilesVersionId");

                    b.ToTable("SalesSummary");
                });

            modelBuilder.Entity("API.Entities.ScheduleOfPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Account_Item")
                        .HasColumnType("int");

                    b.Property<int>("Account_Number")
                        .HasColumnType("int");

                    b.Property<double>("Adv_Payment_Late")
                        .HasColumnType("float");

                    b.Property<int>("Appliance_Reviews_Carried_Patients_Home")
                        .HasColumnType("int");

                    b.Property<int>("Appliance_Reviews_Carried_Premises")
                        .HasColumnType("int");

                    b.Property<double>("Avg_Item_Value")
                        .HasColumnType("float");

                    b.Property<double>("Balance_Due")
                        .HasColumnType("float");

                    b.Property<string>("Balance_Due_Month")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("COVID_Premises_Refrigeration")
                        .HasColumnType("float");

                    b.Property<int>("Chargeable_Exempt")
                        .HasColumnType("int");

                    b.Property<int>("Chargeable_Old_Rate_Exempt")
                        .HasColumnType("int");

                    b.Property<double>("Charges_Collected_Elastic_Hosiery")
                        .HasColumnType("float");

                    b.Property<double>("Charges_Collected_Excl_Hosiery_1")
                        .HasColumnType("float");

                    b.Property<int>("Charges_Collected_Excl_Hosiery_1_Items")
                        .HasColumnType("int");

                    b.Property<double>("Charges_Collected_Excl_Hosiery_1_Per_Item")
                        .HasColumnType("float");

                    b.Property<double>("Charges_Collected_Excl_Hosiery_2")
                        .HasColumnType("float");

                    b.Property<int>("Charges_Collected_Excl_Hosiery_2_Items")
                        .HasColumnType("int");

                    b.Property<double>("Charges_Collected_Excl_Hosiery_2_Per_Item")
                        .HasColumnType("float");

                    b.Property<double>("Charges_FP57_Refund")
                        .HasColumnType("float");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<string>("Discount_Percent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dispensing_Month")
                        .HasColumnType("int");

                    b.Property<int>("Dispensing_Year")
                        .HasColumnType("int");

                    b.Property<int>("DocsId")
                        .HasColumnType("int");

                    b.Property<int>("Exempt_Chargeable")
                        .HasColumnType("int");

                    b.Property<int>("Exempt_Chargeable_Old_Rate")
                        .HasColumnType("int");

                    b.Property<int>("FP57_Declared")
                        .HasColumnType("int");

                    b.Property<int>("Items_Over_100")
                        .HasColumnType("int");

                    b.Property<double>("Items_Over_100_Basic_Price")
                        .HasColumnType("float");

                    b.Property<int>("Items_Over_300")
                        .HasColumnType("int");

                    b.Property<double>("Items_Over_300_Basic_Price")
                        .HasColumnType("float");

                    b.Property<int>("Items_Standard_Disc")
                        .HasColumnType("int");

                    b.Property<int>("Items_Zero_Disc")
                        .HasColumnType("int");

                    b.Property<double>("LPC_Statutory_Levy")
                        .HasColumnType("float");

                    b.Property<double>("Local_Scheme")
                        .HasColumnType("float");

                    b.Property<int>("Medicines_Reviews_Declared")
                        .HasColumnType("int");

                    b.Property<double>("Net_Payment")
                        .HasColumnType("float");

                    b.Property<DateTime>("Net_Payment_Made")
                        .HasColumnType("datetime2");

                    b.Property<int>("New_Medicine_Service_Items")
                        .HasColumnType("int");

                    b.Property<int>("New_Medicine_Service_Undertaken")
                        .HasColumnType("int");

                    b.Property<string>("OCS_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Other_Fee_Appliance_Patient")
                        .HasColumnType("float");

                    b.Property<double>("Other_Fee_Appliance_Premise")
                        .HasColumnType("float");

                    b.Property<double>("Other_Fee_Medicine")
                        .HasColumnType("float");

                    b.Property<double>("Other_Fee_Medicine_Service")
                        .HasColumnType("float");

                    b.Property<double>("Other_Fee_Stoma_Custom")
                        .HasColumnType("float");

                    b.Property<double>("Out_Pocket_Expenses")
                        .HasColumnType("float");

                    b.Property<double>("Pay_Consumable")
                        .HasColumnType("float");

                    b.Property<double>("Pay_Consumable_Per_Item")
                        .HasColumnType("float");

                    b.Property<double>("Pay_Container")
                        .HasColumnType("float");

                    b.Property<double>("Payment_Account")
                        .HasColumnType("float");

                    b.Property<double>("Presc_Activity_Pay")
                        .HasColumnType("float");

                    b.Property<double>("Presc_Activity_Pay_Per_Item")
                        .HasColumnType("float");

                    b.Property<double>("Presc_AddFee_2A_Unlicensed_Meds")
                        .HasColumnType("float");

                    b.Property<double>("Presc_AddFee_2B_Appliance_Home")
                        .HasColumnType("float");

                    b.Property<double>("Presc_AddFee_2B_Appliance_Measure")
                        .HasColumnType("float");

                    b.Property<double>("Presc_AddFee_2E_Controlled")
                        .HasColumnType("float");

                    b.Property<int>("Presc_AddFee_2F_Expensive_Fee_Item")
                        .HasColumnType("int");

                    b.Property<double>("Presc_AddFee_2F_Expensive_Fees")
                        .HasColumnType("float");

                    b.Property<double>("Presc_AddFee_Manual_Price")
                        .HasColumnType("float");

                    b.Property<double>("Presc_AddFee_Methadone_Pay")
                        .HasColumnType("float");

                    b.Property<double>("Recovery_Adv_Payment")
                        .HasColumnType("float");

                    b.Property<double>("Recovery_Adv_Payment_Late_Registered")
                        .HasColumnType("float");

                    b.Property<int>("Referred_Back_Forms")
                        .HasColumnType("int");

                    b.Property<int>("Referred_Back_Items")
                        .HasColumnType("int");

                    b.Property<double>("Reimbursement_Covid_Costs")
                        .HasColumnType("float");

                    b.Property<double>("Sub_Total_Presc_Fee")
                        .HasColumnType("float");

                    b.Property<double>("Sub_Total_Price")
                        .HasColumnType("float");

                    b.Property<double>("Total_Account")
                        .HasColumnType("float");

                    b.Property<double>("Total_All_Fees")
                        .HasColumnType("float");

                    b.Property<double>("Total_Authorised")
                        .HasColumnType("float");

                    b.Property<double>("Total_Authorised_LPP")
                        .HasColumnType("float");

                    b.Property<double>("Total_Charges")
                        .HasColumnType("float");

                    b.Property<double>("Total_Costs")
                        .HasColumnType("float");

                    b.Property<double>("Total_Costs_wFees")
                        .HasColumnType("float");

                    b.Property<double>("Total_Drug")
                        .HasColumnType("float");

                    b.Property<int>("Total_Electronic_Prescription_Items")
                        .HasColumnType("int");

                    b.Property<int>("Total_Electronic_Prescription_Received")
                        .HasColumnType("int");

                    b.Property<double>("Total_Fees")
                        .HasColumnType("float");

                    b.Property<int>("Total_Forms_Received")
                        .HasColumnType("int");

                    b.Property<int>("Total_Items_Over_100")
                        .HasColumnType("int");

                    b.Property<double>("Total_Items_Over_100_Basic_Price")
                        .HasColumnType("float");

                    b.Property<double>("Total_Other")
                        .HasColumnType("float");

                    b.Property<int>("Total_Paid_Fee_Items")
                        .HasColumnType("int");

                    b.Property<double>("Total_Price_Standrt_Disc")
                        .HasColumnType("float");

                    b.Property<double>("Total_Price_Zero_Disc")
                        .HasColumnType("float");

                    b.Property<double>("Transitional_Pay")
                        .HasColumnType("float");

                    b.Property<int>("YTD_MUR_Declaration")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocsId");

                    b.ToTable("ScheduleOfPayments");
                });

            modelBuilder.Entity("API.Entities.UserReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("UserReport");
                });

            modelBuilder.Entity("API.Entities.VersionSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FilesVersionId")
                        .HasColumnType("int");

                    b.Property<double>("InflationRate")
                        .HasColumnType("float");

                    b.Property<int>("NoYear")
                        .HasColumnType("int");

                    b.Property<int>("StartYear")
                        .HasColumnType("int");

                    b.Property<double>("VolumeDecrease")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FilesVersionId");

                    b.ToTable("VersionSetting");
                });

            modelBuilder.Entity("API.Entities.Docs", b =>
                {
                    b.HasOne("API.Entities.FilesVersion", "Version")
                        .WithMany("Documents")
                        .HasForeignKey("FilesVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Version");
                });

            modelBuilder.Entity("API.Entities.ExpenseSummary", b =>
                {
                    b.HasOne("API.Entities.FilesVersion", "Version")
                        .WithMany("ExpenseSummary")
                        .HasForeignKey("FilesVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Version");
                });

            modelBuilder.Entity("API.Entities.FilesVersion", b =>
                {
                    b.HasOne("API.Entities.UserReport", "Report")
                        .WithMany("VersionCreated")
                        .HasForeignKey("UserReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("API.Entities.Mur", b =>
                {
                    b.HasOne("API.Entities.FilesVersion", "Version")
                        .WithMany("Mur")
                        .HasForeignKey("FilesVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Version");
                });

            modelBuilder.Entity("API.Entities.PrescriptionSummary", b =>
                {
                    b.HasOne("API.Entities.FilesVersion", "Version")
                        .WithMany("PrescriptionSummary")
                        .HasForeignKey("FilesVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Version");
                });

            modelBuilder.Entity("API.Entities.Prescriptions", b =>
                {
                    b.HasOne("API.Entities.Docs", "Document")
                        .WithMany("Prescriptions")
                        .HasForeignKey("DocsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("API.Entities.PriceListHistory", b =>
                {
                    b.HasOne("API.Entities.Docs", "Document")
                        .WithMany("PriceListHistory")
                        .HasForeignKey("DocsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("API.Entities.SalesSummary", b =>
                {
                    b.HasOne("API.Entities.FilesVersion", "Version")
                        .WithMany("SalesSummary")
                        .HasForeignKey("FilesVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Version");
                });

            modelBuilder.Entity("API.Entities.ScheduleOfPayment", b =>
                {
                    b.HasOne("API.Entities.Docs", "Document")
                        .WithMany("ScheduleOfPayment")
                        .HasForeignKey("DocsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("API.Entities.UserReport", b =>
                {
                    b.HasOne("API.Entities.AppUser", "Creator")
                        .WithMany("ReportCreated")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("API.Entities.VersionSetting", b =>
                {
                    b.HasOne("API.Entities.FilesVersion", "Version")
                        .WithMany("VersionSetting")
                        .HasForeignKey("FilesVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Version");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("ReportCreated");
                });

            modelBuilder.Entity("API.Entities.Docs", b =>
                {
                    b.Navigation("Prescriptions");

                    b.Navigation("PriceListHistory");

                    b.Navigation("ScheduleOfPayment");
                });

            modelBuilder.Entity("API.Entities.FilesVersion", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("ExpenseSummary");

                    b.Navigation("Mur");

                    b.Navigation("PrescriptionSummary");

                    b.Navigation("SalesSummary");

                    b.Navigation("VersionSetting");
                });

            modelBuilder.Entity("API.Entities.UserReport", b =>
                {
                    b.Navigation("VersionCreated");
                });
#pragma warning restore 612, 618
        }
    }
}
