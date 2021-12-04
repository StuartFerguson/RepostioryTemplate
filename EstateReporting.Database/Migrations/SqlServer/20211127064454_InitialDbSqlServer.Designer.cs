﻿// <auto-generated />
using System;
using EstateReporting.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EstateReporting.Database.Migrations.SqlServer
{
    [DbContext(typeof(EstateReportingSqlServerContext))]
    [Migration("20211127064454_InitialDbSqlServer")]
    partial class InitialDbSqlServer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EstateReporting.Database.Entities.Contract", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OperatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstateId", "OperatorId", "ContractId");

                    b.ToTable("contract");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.ContractProduct", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("EstateId", "ContractId", "ProductId");

                    b.ToTable("contractproduct");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.ContractProductTransactionFee", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionFeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CalculationType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FeeType")
                        .HasColumnType("int");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("EstateId", "ContractId", "ProductId", "TransactionFeeId");

                    b.ToTable("contractproducttransactionfee");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.Estate", b =>
                {
                    b.Property<Guid>("EstateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstateId");

                    b.ToTable("estate");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.EstateOperator", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OperatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RequireCustomMerchantNumber")
                        .HasColumnType("bit");

                    b.Property<bool>("RequireCustomTerminalNumber")
                        .HasColumnType("bit");

                    b.HasKey("EstateId", "OperatorId");

                    b.ToTable("estateoperator");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.EstateSecurityUser", b =>
                {
                    b.Property<Guid>("SecurityUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SecurityUserId", "EstateId");

                    b.ToTable("estatesecurityuser");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.File", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileImportLogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FileProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FileReceivedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EstateId", "FileImportLogId", "FileId");

                    b.ToTable("file");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.FileImportLog", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileImportLogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ImportLogDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("EstateId", "FileImportLogId");

                    b.ToTable("fileimportlog");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.FileImportLogFile", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileImportLogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FileProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FileUploadedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OriginalFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EstateId", "FileImportLogId", "FileId");

                    b.ToTable("fileimportlogfile");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.FileLine", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LineNumber")
                        .HasColumnType("int");

                    b.Property<string>("FileLineData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstateId", "FileId", "LineNumber");

                    b.ToTable("fileline");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.Merchant", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SettlementSchedule")
                        .HasColumnType("int");

                    b.HasKey("EstateId", "MerchantId");

                    b.ToTable("merchant");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.MerchantAddress", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Town")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstateId", "MerchantId", "AddressId");

                    b.ToTable("merchantaddress");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.MerchantBalanceHistory", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AvailableBalance")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("ChangeAmount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("EntryDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventId");

                    b.ToTable("merchantbalancehistory");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.MerchantContact", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstateId", "MerchantId", "ContactId");

                    b.ToTable("merchantcontact");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.MerchantDevice", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstateId", "MerchantId", "DeviceId");

                    b.ToTable("merchantdevice");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.MerchantOperator", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OperatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MerchantNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TerminalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstateId", "MerchantId", "OperatorId");

                    b.ToTable("merchantoperator");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.MerchantSecurityUser", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SecurityUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstateId", "MerchantId", "SecurityUserId");

                    b.ToTable("merchantsecurityuser");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.MerchantSettlementFee", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SettlementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CalculatedValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FeeCalculatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FeeValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsSettled")
                        .HasColumnType("bit");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EstateId", "SettlementId", "TransactionId", "FeeId");

                    b.ToTable("merchantsettlementfees");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.Reconciliation", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeviceIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAuthorised")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ResponseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TransactionDateTime")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("TransactionTime")
                        .HasColumnType("time");

                    b.Property<decimal>("TransactionValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TransactionId");

                    b.ToTable("reconciliation");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.Settlement", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SettlementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SettlementDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EstateId", "SettlementId");

                    b.ToTable("settlement");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.Transaction", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorisationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeviceIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAuthorised")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("OperatorIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ResponseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TransactionDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("TransactionTime")
                        .HasColumnType("time");

                    b.Property<string>("TransactionType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstateId", "MerchantId", "TransactionId");

                    b.ToTable("transaction");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.TransactionAdditionalRequestData", b =>
                {
                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerAccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstateId", "MerchantId", "TransactionId");

                    b.ToTable("transactionadditionalrequestdata");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.TransactionAdditionalResponseData", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TransactionId");

                    b.ToTable("transactionadditionalresponsedata");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.TransactionFee", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CalculatedValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CalculationType")
                        .HasColumnType("int");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FeeType")
                        .HasColumnType("int");

                    b.Property<decimal>("FeeValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TransactionId", "FeeId");

                    b.ToTable("transactionfee");
                });

            modelBuilder.Entity("EstateReporting.Database.Entities.Voucher", b =>
                {
                    b.Property<Guid>("VoucherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GenerateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsGenerated")
                        .HasColumnType("bit");

                    b.Property<bool>("IsIssued")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRedeemed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IssuedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("OperatorIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecipientEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecipientMobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RedeemedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("VoucherCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VoucherId");

                    b.ToTable("voucher");
                });

            modelBuilder.Entity("EstateReporting.Database.ViewEntities.FileImportLogView", b =>
                {
                    b.Property<int>("FileCount")
                        .HasColumnType("int");

                    b.Property<Guid>("FileImportLogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ImportLogDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ImportLogDateTime")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("ImportLogTime")
                        .HasColumnType("time");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.ToTable("uvwFileImportLogView");

                    b.ToView("uvwFileImportLog");
                });

            modelBuilder.Entity("EstateReporting.Database.ViewEntities.FileView", b =>
                {
                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FailedCount")
                        .HasColumnType("int");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FileReceivedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FileReceivedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("FileReceivedTime")
                        .HasColumnType("time");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<int>("LineCount")
                        .HasColumnType("int");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MerchantName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PendingCount")
                        .HasColumnType("int");

                    b.Property<int>("SuccessCount")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.ToTable("uvwFileView");

                    b.ToView("uvwFile");
                });

            modelBuilder.Entity("EstateReporting.Database.ViewEntities.MerchantBalanceView", b =>
                {
                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ChangeAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EntryDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EntryType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("In")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Out")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.ToTable("uvwMerchantBalanceView");

                    b.ToView("uvwMerchantBalance");
                });

            modelBuilder.Entity("EstateReporting.Database.ViewEntities.SettlementView", b =>
                {
                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CalculatedValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DayOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FeeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSettled")
                        .HasColumnType("bit");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MerchantName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Month")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MonthNumber")
                        .HasColumnType("int");

                    b.Property<string>("OperatorIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SettlementDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SettlementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("int");

                    b.Property<int>("YearNumber")
                        .HasColumnType("int");

                    b.ToView("uvwSettlements");
                });

            modelBuilder.Entity("EstateReporting.Database.ViewEntities.TransactionsView", b =>
                {
                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DayOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAuthorised")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Month")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MonthNumber")
                        .HasColumnType("int");

                    b.Property<string>("OperatorIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TransactionDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TransactionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("int");

                    b.Property<int>("YearNumber")
                        .HasColumnType("int");

                    b.ToTable("uvwTransactionsView");

                    b.ToView("uvwTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
