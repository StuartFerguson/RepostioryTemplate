namespace EstateReporting.Testing
{
    using System;
    using System.Collections.Generic;
    using BusinessLogic.Events;
    using EstateManagement.Contract.DomainEvents;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using EstateManagement.MerchantStatement.DomainEvents;
    using FileProcessor.File.DomainEvents;
    using FileProcessor.FileImportLog.DomainEvents;
    using Models;
    using TransactionProcessor.Reconciliation.DomainEvents;
    using TransactionProcessor.Settlement.DomainEvents;
    using TransactionProcessor.Transaction.DomainEvents;
    using VoucherManagement.Voucher.DomainEvents;

    /// <summary>
    /// 
    /// </summary>
    public class TestData
    {
        #region Fields

        /// <summary>
        /// The address identifier
        /// </summary>
        public static Guid AddressId = Guid.Parse("B1C68246-F867-43CC-ACA9-37D15D6437C6");

        /// <summary>
        /// The address line1
        /// </summary>
        public static String AddressLine1 = "AddressLine1";

        /// <summary>
        /// The address line2
        /// </summary>
        public static String AddressLine2 = "AddressLine2";

        /// <summary>
        /// The address line3
        /// </summary>
        public static String AddressLine3 = "AddressLine3";

        /// <summary>
        /// The address line4
        /// </summary>
        public static String AddressLine4 = "AddressLine4";

        /// <summary>
        /// The country
        /// </summary>
        public static String Country = "Country";

        /// <summary>
        /// The estate identifier
        /// </summary>
        public static Guid EstateId = Guid.Parse("8DC6C30F-AD7C-4533-83DF-EA1C05461486");

        /// <summary>
        /// The merchant identifier
        /// </summary>
        public static Guid MerchantId = Guid.Parse("D83ECC6B-E3C1-4CC9-9C63-7CEE8F8029C9");

        /// <summary>
        /// The post code
        /// </summary>
        public static String PostCode = "PostCode";

        /// <summary>
        /// The region
        /// </summary>
        public static String Region = "Region";

        /// <summary>
        /// The town
        /// </summary>
        public static String Town = "Town";

        /// <summary>
        /// The address added event
        /// </summary>
        public static AddressAddedEvent AddressAddedEvent = new AddressAddedEvent(TestData.MerchantId,
                                                                                  TestData.EstateId,
                                                                                  TestData.AddressId,
                                                                                  TestData.AddressLine1,
                                                                                  TestData.AddressLine2,
                                                                                  TestData.AddressLine3,
                                                                                  TestData.AddressLine4,
                                                                                  TestData.Town,
                                                                                  TestData.Region,
                                                                                  TestData.PostCode,
                                                                                  TestData.Country);

        public static Decimal AvailableBalance = 1000.00m;

        public static Decimal AvailableBalance2 = 2000.00m;

        public static Decimal Balance = 1100.00m;

        public static Decimal Balance2 = 2200.00m;

        public static String BalanceRecordReference = "Transaction Completed";

        public static Decimal ChangeAmount = 100.00m;

        public static Decimal ChangeAmount2 = 200.00m;

        /// <summary>
        /// The contact email
        /// </summary>
        public static String ContactEmail = "testcontact1@testmerchant1.co.uk";

        /// <summary>
        /// The contact identifier
        /// </summary>
        public static Guid ContactId = Guid.Parse("B1C68246-F867-43CC-ACA9-37D15D6437C6");

        /// <summary>
        /// The contact name
        /// </summary>
        public static String ContactName = "Test Contact";

        /// <summary>
        /// The contact phone
        /// </summary>
        public static String ContactPhone = "123456789";

        /// <summary>
        /// The contact added event
        /// </summary>
        public static ContactAddedEvent ContactAddedEvent = new ContactAddedEvent(TestData.MerchantId,
                                                                                  TestData.EstateId,
                                                                                  TestData.ContactId,
                                                                                  TestData.ContactName,
                                                                                  TestData.ContactPhone,
                                                                                  TestData.ContactEmail);

        /// <summary>
        /// The device identifier
        /// </summary>
        public static Guid DeviceId = Guid.Parse("B1C68246-F867-43CC-ACA9-37D15D6437C6");

        /// <summary>
        /// The device identifier
        /// </summary>
        public static String DeviceIdentifier = "ABCD1234";

        /// <summary>
        /// The device added to merchant event
        /// </summary>
        public static DeviceAddedToMerchantEvent DeviceAddedToMerchantEvent =
            new DeviceAddedToMerchantEvent(TestData.MerchantId, TestData.EstateId, TestData.DeviceId, TestData.DeviceIdentifier);

        /// <summary>
        /// The email address
        /// </summary>
        public static String EmailAddress = "testuser1@testestate1.co.uk";

        /// <summary>
        /// The estate name
        /// </summary>
        public static String EstateName = "Test Estate 1";

        /// <summary>
        /// The estate created event
        /// </summary>
        public static EstateCreatedEvent EstateCreatedEvent = new EstateCreatedEvent(TestData.EstateId, TestData.EstateName);

        /// <summary>
        /// The security user identifier
        /// </summary>
        public static Guid EstateSecurityUserId = Guid.Parse("CBEE25E6-1B08-4023-B20C-CFE0AD746808");

        /// <summary>
        /// The estate security user added event
        /// </summary>
        public static SecurityUserAddedToEstateEvent EstateSecurityUserAddedEvent =
            new SecurityUserAddedToEstateEvent(TestData.EstateId, TestData.EstateSecurityUserId, TestData.EmailAddress);

        /// <summary>
        /// The merchant name
        /// </summary>
        public static String MerchantName = "Test Merchant 1";

        /// <summary>
        /// The merchant created event
        /// </summary>
        public static MerchantCreatedEvent MerchantCreatedEvent = new MerchantCreatedEvent(TestData.MerchantId, TestData.EstateId, TestData.MerchantName, DateTime.Now);

        public static String MerchantNumber = "12345678";

        public static Guid MerchantSecurityUserId = Guid.Parse("DFCE7A95-CB6D-442A-928A-F1B41D2AA4A9");

        /// <summary>
        /// The merchant security user added event
        /// </summary>
        public static SecurityUserAddedToMerchantEvent MerchantSecurityUserAddedEvent =
            new SecurityUserAddedToMerchantEvent(TestData.MerchantId, TestData.EstateId, TestData.MerchantSecurityUserId, TestData.EmailAddress);

        public static Guid OperatorId = Guid.Parse("DCDC5054-C026-4492-AFED-C74E4DEFD00C");

        public static String OperatorName = "Test Operator 1";

        public static Boolean RequireCustomMerchantNumber = true;

        public static Boolean RequireCustomTerminalNumber = true;

        public static OperatorAddedToEstateEvent OperatorAddedToEstateEvent =
            new OperatorAddedToEstateEvent(TestData.EstateId,
                                           TestData.OperatorId,
                                           TestData.OperatorName,
                                           TestData.RequireCustomMerchantNumber,
                                           TestData.RequireCustomTerminalNumber);

        public static String TerminalNumber = "12345679";

        public static OperatorAssignedToMerchantEvent OperatorAssignedToMerchantEvent = new OperatorAssignedToMerchantEvent(TestData.MerchantId,
            TestData.EstateId,
            TestData.OperatorId,
            TestData.OperatorName,
            TestData.MerchantNumber,
            TestData.TerminalNumber);

        /// <summary>
        /// The reconcilation transaction count
        /// </summary>
        public static Int32 ReconcilationTransactionCount = 1;

        /// <summary>
        /// The reconcilation transaction value
        /// </summary>
        public static Decimal ReconcilationTransactionValue = 100.00m;

        public static DateTime StatementGeneratedDate = new DateTime(2021, 10, 6);

        public static Guid StatementId = Guid.Parse("17D432A3-E2A8-47FD-B067-CBF4C132447C");

        public static StatementGeneratedEvent StatementGeneratedEvent = new StatementGeneratedEvent(TestData.StatementId,
                                                                                                    TestData.EstateId,
                                                                                                    TestData.MerchantId,
                                                                                                    TestData.StatementGeneratedDate);

        public static DateTime TransactionDateTime = DateTime.Now;

        public static Guid TransactionId = Guid.Parse("4187AA70-4E36-451B-9B88-A164C08A9D4D");

        public static String TransactionNumber = "1";

        public static String TransactionReference = "123456";

        public static String TransactionType = "Logon";

        public static TransactionHasStartedEvent TransactionHasStartedEvent = new TransactionHasStartedEvent(TestData.TransactionId,
                                                                                                             TestData.EstateId,
                                                                                                             TestData.MerchantId,
                                                                                                             TestData.TransactionDateTime,
                                                                                                             TestData.TransactionNumber,
                                                                                                             TestData.TransactionType,
                                                                                                             TestData.TransactionReference,
                                                                                                             TestData.DeviceIdentifier,
                                                                                                             TestData.TransactionAmount);

        public static AdditionalRequestDataRecordedEvent AdditionalRequestDataRecordedEvent =
            new AdditionalRequestDataRecordedEvent(TestData.TransactionId, TestData.EstateId, TestData.MerchantId, TestData.OperatorName, TestData.AdditionalRequestData);

        public static AdditionalResponseDataRecordedEvent AdditionalResponseDataRecordedEvent =
            new AdditionalResponseDataRecordedEvent(TestData.TransactionId,
                                                    TestData.EstateId,
                                                    TestData.MerchantId,
                                                    TestData.OperatorName,
                                                    TestData.AdditionalResponseData);

        public static String AuthorisationCode = "ABCD1234";

        public static Decimal CalculatedValue = 2.95m;

        public static String ContractDescription = "Test Contract";

        public static Guid ContractId = Guid.Parse("D3F17288-2E3C-41F0-BD00-95047DC13BDA");

        public static ContractCreatedEvent ContractCreatedEvent =
            new ContractCreatedEvent(TestData.ContractId, TestData.EstateId, TestData.OperatorId, TestData.ContractDescription);

        public static String CurrencyCode = "KES";

        public static String DeclinedOperatorResponseCode = "400";

        public static String DeclinedOperatorResponseMessage = "Topup Failed";

        public static String DeclinedResponseCode = "0001";

        public static String DeclinedResponseMessage = "DeclinedResponseMessage";

        public static String EndDate = "20210105";

        public static String EstateReference = "Estate1";

        public static Int32 FeeCalculationType = 0;

        public static Decimal FeeValue = 0.0005m;

        public static Guid TransactionFeeId = Guid.Parse("F092CBF5-2F34-407B-8E50-21164C841A89");

        public static MerchantFeeAddedToTransactionEnrichedEvent MerchantFeeAddedToTransactionEnrichedEvent =
            new MerchantFeeAddedToTransactionEnrichedEvent(TestData.TransactionId,
                                                           Guid.Parse("9646B63F-1956-4261-99FF-E90B2F8BFC79"),
                                                           TestData.EstateId,
                                                           TestData.MerchantId,
                                                           TestData.CalculatedValue,
                                                           TestData.FeeCalculationType,
                                                           TestData.TransactionFeeId,
                                                           TestData.FeeValue,
                                                           TestData.FeeCalculatedDateTime);

        public static MerchantFeeAddedToTransactionEvent MerchantFeeAddedToTransactionEvent = new MerchantFeeAddedToTransactionEvent(TestData.TransactionId,
            TestData.EstateId,
            TestData.MerchantId,
            TestData.CalculatedValue,
            TestData.FeeCalculationType,
            TestData.TransactionFeeId,
            TestData.FeeValue,
            TestData.FeeCalculatedDateTime,
            TestData.SettlementDueDate,
            TestData.SettledDate);

        public static ServiceProviderFeeAddedToTransactionEnrichedEvent ServiceProviderFeeAddedToTransactionEvent =
            new ServiceProviderFeeAddedToTransactionEnrichedEvent(TestData.TransactionId,
                                                                  Guid.Parse("9646B63F-1956-4261-99FF-E90B2F8BFC79"),
                                                                  TestData.EstateId,
                                                                  TestData.MerchantId,
                                                                  TestData.CalculatedValue,
                                                                  TestData.FeeCalculationType,
                                                                  TestData.TransactionFeeId,
                                                                  TestData.FeeValue,
                                                                  TestData.FeeCalculatedDateTime);

        public static DateTime FeeCalculatedDateTime = new DateTime(2021, 3, 23);

        public static Int32 FeeType = 0;

        public static Guid FileId = Guid.Parse("5F7F45D6-0604-46C7-AA88-EAA885A6B208");

        public static Guid FileImportLogId = Guid.Parse("5F1149F8-0313-45E4-BE3A-3D7B07EEB414");

        public static String FilePath = "home/txnproc/bulkfiles";

        public static Guid FileProfileId = Guid.Parse("D0D3A4E5-870E-42F6-AD0E-5E24252BC95E");

        public static DateTime FileUploadedDateTime = new DateTime(2021, 5, 7);

        public static String OriginalFileName = "ExampleFile.csv";

        public static Guid UserId = Guid.Parse("BE52C5AC-72E5-4976-BAA0-98699E36C1EB");

        public static FileAddedToImportLogEvent FileAddedToImportLogEvent =
            new FileAddedToImportLogEvent(TestData.FileImportLogId,
                                          TestData.FileId,
                                          TestData.EstateId,
                                          TestData.MerchantId,
                                          TestData.UserId,
                                          TestData.FileProfileId,
                                          TestData.OriginalFileName,
                                          TestData.FilePath,
                                          TestData.FileUploadedDateTime);

        public static String FileLocation = "home/txnproc/bulkfiles/safaricom/ExampleFile.csv";

        public static FileCreatedEvent FileCreatedEvent = new FileCreatedEvent(TestData.FileId,
                                                                               TestData.FileImportLogId,
                                                                               TestData.EstateId,
                                                                               TestData.MerchantId,
                                                                               TestData.UserId,
                                                                               TestData.FileProfileId,
                                                                               TestData.FileLocation,
                                                                               TestData.FileUploadedDateTime);

        public static String FileLine = "D,124567,100";

        public static Int32 LineNumber = 1;

        public static FileLineAddedEvent FileLineAddedEvent = new FileLineAddedEvent(TestData.FileId, TestData.EstateId, TestData.LineNumber, TestData.FileLine);

        public static String ResponseCode = "0000";

        public static String ResponseMessage = "SUCCESS";

        public static FileLineProcessingFailedEvent FileLineProcessingFailedEvent =
            new FileLineProcessingFailedEvent(TestData.FileId,
                                              TestData.EstateId,
                                              TestData.LineNumber,
                                              TestData.TransactionId,
                                              TestData.ResponseCode,
                                              TestData.ResponseMessage);

        public static FileLineProcessingIgnoredEvent FileLineProcessingIgnoredEvent =
            new FileLineProcessingIgnoredEvent(TestData.FileId, TestData.EstateId, TestData.LineNumber);

        public static FileLineProcessingSuccessfulEvent FileLineProcessingSuccessfulEvent =
            new FileLineProcessingSuccessfulEvent(TestData.FileId, TestData.EstateId, TestData.LineNumber, TestData.TransactionId);

        public static DateTime ProcessingCompletedDateTime = new DateTime(2021, 5, 7);

        public static FileProcessingCompletedEvent FileProcessingCompletedEvent =
            new FileProcessingCompletedEvent(TestData.FileId, TestData.EstateId, TestData.ProcessingCompletedDateTime);

        public static String ProductDisplayText = "100 KES";

        public static Decimal ProductFixedValue = 100.00m;

        public static Guid ProductId = Guid.Parse("033BC002-FC65-4123-81E6-B7D21885BB0C");

        public static String ProductName = "Test Product 1";

        public static FixedValueProductAddedToContractEvent FixedValueProductAddedToContractEvent = new FixedValueProductAddedToContractEvent(TestData.ContractId,
            TestData.EstateId,
            TestData.ProductId,
            TestData.ProductName,
            TestData.ProductDisplayText,
            TestData.ProductFixedValue);

        public static DateTime ImportLogDateTime = new DateTime(2021, 5, 7);

        public static Boolean IsAuthorised = true;

        public static Boolean IsSettled = true;

        public static String MerchantReference = "Merchant1";

        public static DateTime NextSettlementDate = new DateTime(2021, 10, 6);

        public static Int32 NumberOfFeesSettled = 1;

        public static String OperatorAuthorisationCode = "OP1234";

        public static String OperatorIdentifier = "Voucher";

        public static String OperatorResponseCode = "200";

        public static String OperatorResponseMessage = "Topup Successful";

        public static String OperatorTransactionId = "SF12345";

        public static OverallTotalsRecordedEvent OverallTotalsRecordedEvent =
            new OverallTotalsRecordedEvent(TestData.TransactionId,
                                           TestData.EstateId,
                                           TestData.MerchantId,
                                           TestData.ReconcilationTransactionCount,
                                           TestData.ReconcilationTransactionValue);

        public static ProductDetailsAddedToTransactionEvent ProductDetailsAddedToTransactionEvent =
            new ProductDetailsAddedToTransactionEvent(TestData.TransactionId, TestData.EstateId, TestData.MerchantId, TestData.ContractId, TestData.ProductId);

        public static TransactionSourceAddedToTransactionEvent TransactionSourceAddedToTransactionEvent =
            new TransactionSourceAddedToTransactionEvent(TestData.TransactionId, TestData.EstateId, TestData.MerchantId, TestData.TransactionSource);

        public static String RecipientEmail = "testemail@recipient.co.uk";

        public static String RecipientMobile = "123455679";

        public static ReconciliationHasBeenLocallyAuthorisedEvent ReconciliationHasBeenLocallyAuthorisedEvent =
            new ReconciliationHasBeenLocallyAuthorisedEvent(TestData.TransactionId,
                                                            TestData.EstateId,
                                                            TestData.MerchantId,
                                                            TestData.ResponseCode,
                                                            TestData.ResponseMessage);

        public static ReconciliationHasBeenLocallyDeclinedEvent ReconciliationHasBeenLocallyDeclinedEvent =
            new ReconciliationHasBeenLocallyDeclinedEvent(TestData.TransactionId,
                                                          TestData.EstateId,
                                                          TestData.MerchantId,
                                                          TestData.ResponseCode,
                                                          TestData.ResponseMessage);

        public static ReconciliationHasCompletedEvent ReconciliationHasCompletedEvent =
            new ReconciliationHasCompletedEvent(TestData.TransactionId, TestData.EstateId, TestData.MerchantId);

        public static ReconciliationHasStartedEvent ReconciliationHasStartedEvent =
            new ReconciliationHasStartedEvent(TestData.TransactionId, TestData.EstateId, TestData.MerchantId, TestData.TransactionDateTime);

        public static DateTime SettledDate = new DateTime(2021, 10, 6, 1, 2, 3);

        public static DateTime SettlementDate = new DateTime(2021, 10, 6);

        public static DateTime SettlementDueDate = new DateTime(2021, 10, 6);

        public static Guid SettlementId = Guid.Parse("7CF02BE4-4BF0-4BB2-93C1-D6E5EC769E56");

        public static Boolean SettlementIsCompleted = true;

        public static Int32 SettlementSchedule = 1;

        public static String StartDate = "20210104";

        public static TransactionHasBeenCompletedEvent TransactionHasBeenCompletedEvent = new TransactionHasBeenCompletedEvent(TestData.TransactionId,
            TestData.EstateId,
            TestData.MerchantId,
            TestData.ResponseCode,
            TestData.ResponseMessage,
            TestData.IsAuthorised,
            TestData.TransactionCompletedDateTime,
            TestData.TransactionAmount);

        public static Decimal? TransactionAmount = 100.00m;

        public static TransactionAuthorisedByOperatorEvent TransactionAuthorisedByOperatorEvent =
            new TransactionAuthorisedByOperatorEvent(TestData.TransactionId,
                                                     TestData.EstateId,
                                                     TestData.MerchantId,
                                                     TestData.OperatorName,
                                                     TestData.AuthorisationCode,
                                                     TestData.OperatorResponseCode,
                                                     TestData.OperatorResponseMessage,
                                                     TestData.OperatorTransactionId,
                                                     TestData.ResponseCode,
                                                     TestData.ResponseMessage);

        public static TransactionDeclinedByOperatorEvent TransactionDeclinedByOperatorEvent =
            new TransactionDeclinedByOperatorEvent(TestData.TransactionId,
                                                   TestData.EstateId,
                                                   TestData.MerchantId,
                                                   TestData.OperatorName,
                                                   TestData.DeclinedOperatorResponseCode,
                                                   TestData.DeclinedOperatorResponseMessage,
                                                   TestData.DeclinedResponseCode,
                                                   TestData.DeclinedResponseMessage);

        public static String TransactionFeeDescription = "Merchant Commission";

        public static TransactionFeeForProductAddedToContractEvent TransactionFeeForProductAddedToContractEvent =
            new TransactionFeeForProductAddedToContractEvent(TestData.ContractId,
                                                             TestData.EstateId,
                                                             TestData.ProductId,
                                                             TestData.TransactionFeeId,
                                                             TestData.TransactionFeeDescription,
                                                             TestData.FeeCalculationType,
                                                             TestData.FeeType,
                                                             TestData.FeeValue);

        public static TransactionFeeForProductDisabledEvent TransactionFeeForProductDisabledEvent =
            new TransactionFeeForProductDisabledEvent(TestData.ContractId, TestData.EstateId, TestData.ProductId, TestData.TransactionFeeId);

        public static TransactionHasBeenLocallyAuthorisedEvent TransactionHasBeenLocallyAuthorisedEvent =
            new TransactionHasBeenLocallyAuthorisedEvent(TestData.TransactionId,
                                                         TestData.EstateId,
                                                         TestData.MerchantId,
                                                         TestData.AuthorisationCode,
                                                         TestData.ResponseCode,
                                                         TestData.ResponseMessage);

        public static TransactionHasBeenLocallyDeclinedEvent TransactionHasBeenLocallyDeclinedEvent =
            new TransactionHasBeenLocallyDeclinedEvent(TestData.TransactionId,
                                                       TestData.EstateId,
                                                       TestData.MerchantId,
                                                       TestData.DeclinedResponseCode,
                                                       TestData.DeclinedResponseMessage);

        public static Int32 ValueOfFeesSettled = 1;

        public static VariableValueProductAddedToContractEvent VariableValueProductAddedToContractEvent =
            new VariableValueProductAddedToContractEvent(TestData.ContractId, TestData.EstateId, TestData.ProductId, TestData.ProductName, TestData.ProductDisplayText);

        public static String VoucherCode = "1234GHT";

        public static DateTime VoucherExpiryDate = new DateTime(2021, 12, 16);

        public static Guid VoucherId = Guid.Parse("1736C058-5AC3-4AAC-8167-10DBAC2B7968");

        public static VoucherFullyRedeemedEvent VoucherFullyRedeemedEvent = new VoucherFullyRedeemedEvent(TestData.VoucherId,
                                                                                                          TestData.EstateId,
                                                                                                          TestData.VoucherRedeemedDate);

        public static DateTime VoucherGeneratedDate = new DateTime(2021, 12, 16);

        public static String VoucherMessage = String.Empty;

        public static Decimal VoucherValue = 10.00m;

        public static VoucherGeneratedEvent VoucherGeneratedEvent = new VoucherGeneratedEvent(TestData.VoucherId,
                                                                                              TestData.EstateId,
                                                                                              TestData.TransactionId,
                                                                                              TestData.VoucherGeneratedDate,
                                                                                              TestData.OperatorIdentifier,
                                                                                              TestData.VoucherValue,
                                                                                              TestData.VoucherCode,
                                                                                              TestData.VoucherExpiryDate,
                                                                                              TestData.VoucherMessage);

        public static DateTime VoucherIssuedDate = new DateTime(2021, 12, 16);

        public static VoucherIssuedEvent VoucherIssuedEvent = new VoucherIssuedEvent(TestData.VoucherId,
                                                                                     TestData.EstateId,
                                                                                     TestData.VoucherIssuedDate,
                                                                                     TestData.RecipientEmail,
                                                                                     TestData.RecipientMobile);

        private static readonly DateTime TransactionCompletedDateTime = new DateTime(2021, 12, 16);

        private static readonly DateTime VoucherRedeemedDate = new DateTime(2021, 12, 16);

        private static Int32 TransactionSource = 1;

        #endregion

        #region Properties

        public static Dictionary<String, String> AdditionalRequestData =>
            new Dictionary<String, String> {
                                               {"Amount", "100.00"},
                                               {"CustomerAccountNumber", "123456789"}
                                           };

        public static Dictionary<String, String> AdditionalResponseData =>
            new Dictionary<String, String> {
                                               {"Amount", "100.00"},
                                               {"CustomerAccountNumber", "123456789"}
                                           };

        public static EstateReferenceAllocatedEvent EstateReferenceAllocatedEvent => new EstateReferenceAllocatedEvent(TestData.EstateId, TestData.EstateReference);

        public static ImportLogCreatedEvent ImportLogCreatedEvent => new ImportLogCreatedEvent(TestData.FileImportLogId, TestData.EstateId, TestData.ImportLogDateTime);

        public static MerchantBalanceChangedEvent MerchantBalanceChangedEvent =>
            new MerchantBalanceChangedEvent(TestData.MerchantId,
                                            Guid.Parse("E736CC81-5155-4119-84CD-537B81AA7F6D"),
                                            DateTime.Now,
                                            TestData.EstateId,
                                            TestData.MerchantId,
                                            TestData.AvailableBalance,
                                            TestData.Balance,
                                            TestData.ChangeAmount,
                                            TestData.BalanceRecordReference);

        public static MerchantBalanceChangedEvent MerchantBalanceChangedEvent2 =>
            new MerchantBalanceChangedEvent(TestData.MerchantId,
                                            Guid.Parse("E736CC81-5155-4119-84CD-537B81AA7F6D"),
                                            DateTime.Now,
                                            TestData.EstateId,
                                            TestData.MerchantId,
                                            TestData.AvailableBalance2,
                                            TestData.Balance2,
                                            TestData.ChangeAmount2,
                                            TestData.BalanceRecordReference);

        public static MerchantFeeAddedPendingSettlementEvent MerchantFeeAddedPendingSettlementEvent =>
            new MerchantFeeAddedPendingSettlementEvent(TestData.SettlementId,
                                                       TestData.EstateId,
                                                       TestData.MerchantId,
                                                       TestData.TransactionId,
                                                       TestData.CalculatedValue,
                                                       TestData.FeeCalculationType,
                                                       TestData.TransactionFeeId,
                                                       TestData.FeeValue,
                                                       TestData.FeeCalculatedDateTime);

        public static MerchantFeeSettledEvent MerchantFeeSettledEvent =>
            new MerchantFeeSettledEvent(TestData.SettlementId,
                                        TestData.EstateId,
                                        TestData.MerchantId,
                                        TestData.TransactionId,
                                        TestData.CalculatedValue,
                                        TestData.FeeCalculationType,
                                        TestData.TransactionFeeId,
                                        TestData.FeeValue,
                                        TestData.FeeCalculatedDateTime);

        public static MerchantReferenceAllocatedEvent MerchantReferenceAllocatedEvent =>
            new MerchantReferenceAllocatedEvent(TestData.MerchantId, TestData.EstateId, TestData.MerchantReference);

        public static SettlementCompletedEvent SettlementCompletedEvent => new SettlementCompletedEvent(TestData.SettlementId, TestData.EstateId);

        public static SettlementCreatedForDateEvent SettlementCreatedForDateEvent =>
            new SettlementCreatedForDateEvent(TestData.SettlementId, TestData.EstateId, TestData.SettlementDate);

        public static SettlementFeeModel SettlementFeeModel =>
            new SettlementFeeModel {
                                       SettlementDate = TestData.SettlementDate,
                                       SettlementId = TestData.SettlementId,
                                       CalculatedValue = TestData.CalculatedValue,
                                       MerchantId = TestData.MerchantId,
                                       MerchantName = TestData.MerchantName,
                                       FeeDescription = TestData.TransactionFeeDescription,
                                       IsSettled = TestData.IsSettled,
                                       TransactionId = TestData.TransactionId
                                   };

        public static List<SettlementFeeModel> SettlementFeeModels =>
            new List<SettlementFeeModel> {
                                             new SettlementFeeModel {
                                                                        SettlementDate = TestData.SettlementDate,
                                                                        SettlementId = TestData.SettlementId,
                                                                        CalculatedValue = TestData.CalculatedValue,
                                                                        MerchantId = TestData.MerchantId,
                                                                        MerchantName = TestData.MerchantName,
                                                                        FeeDescription = TestData.TransactionFeeDescription,
                                                                        IsSettled = TestData.IsSettled,
                                                                        TransactionId = TestData.TransactionId
                                                                    }
                                         };

        public static SettlementModel SettlementModel =>
            new SettlementModel {
                                    IsCompleted = TestData.SettlementIsCompleted,
                                    SettlementDate = TestData.SettlementDate,
                                    NumberOfFeesSettled = TestData.NumberOfFeesSettled,
                                    SettlementId = TestData.SettlementId,
                                    ValueOfFeesSettled = TestData.ValueOfFeesSettled,
                                    SettlementFees = TestData.SettlementFeeModels
                                };

        public static List<SettlementModel> SettlementModels =>
            new List<SettlementModel> {
                                          new SettlementModel {
                                                                  IsCompleted = TestData.SettlementIsCompleted,
                                                                  SettlementDate = TestData.SettlementDate,
                                                                  NumberOfFeesSettled = TestData.NumberOfFeesSettled,
                                                                  SettlementId = TestData.SettlementId,
                                                                  ValueOfFeesSettled = TestData.ValueOfFeesSettled
                                                              }
                                      };

        public static SettlementScheduleChangedEvent SettlementScheduleChangedEvent =>
            new SettlementScheduleChangedEvent(TestData.MerchantId, TestData.EstateId, TestData.SettlementSchedule, TestData.NextSettlementDate);

        #endregion
    }
}