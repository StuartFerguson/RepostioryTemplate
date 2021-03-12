namespace EstateReporting.Testing
{
    using System;
    using System.Collections.Generic;
    using EstateManagement.Contract.DomainEvents;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using EstateReporting.BusinessLogic.Events;
    using Models;
    using TransactionProcessor.Reconciliation.DomainEvents;
    using TransactionProcessor.Transaction.DomainEvents;
    using VoucherManagement.Voucher.DomainEvents;
    using EstateSecurityUserAddedEvent = EstateManagement.Estate.DomainEvents.SecurityUserAddedEvent;
    using MerchantSecurityUserAddedEvent = EstateManagement.Merchant.DomainEvents.SecurityUserAddedEvent;

    /// <summary>
    /// 
    /// </summary>
    public class TestData
    {
        #region Fields
        
        /// <summary>
        /// The reconcilation transaction count
        /// </summary>
        public static Int32 ReconcilationTransactionCount = 1;

        /// <summary>
        /// The reconcilation transaction value
        /// </summary>
        public static Decimal ReconcilationTransactionValue = 100.00m;

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

        public static Decimal AvailableBalance = 1000.00m;
        public static Decimal AvailableBalance2 = 2000.00m;
        public static Decimal Balance = 1100.00m;
        public static Decimal Balance2 = 2200.00m;
        public static Decimal ChangeAmount = 100.00m;
        public static Decimal ChangeAmount2 = 200.00m;

        public static String BalanceRecordReference = "Transaction Completed";

        /// <summary>
        /// The estate created event
        /// </summary>
        public static EstateCreatedEvent EstateCreatedEvent = new EstateCreatedEvent(TestData.EstateId, TestData.EstateName);

        public static MerchantBalanceChangedEvent MerchantBalanceChangedEvent = MerchantBalanceChangedEvent.Create(TestData.MerchantId,
            Guid.Parse("E736CC81-5155-4119-84CD-537B81AA7F6D"),
            TestData.EstateId,
            TestData.MerchantId,
            TestData.AvailableBalance,
            TestData.Balance,
            TestData.ChangeAmount,
            TestData.BalanceRecordReference);

        public static MerchantBalanceChangedEvent MerchantBalanceChangedEvent2 = MerchantBalanceChangedEvent.Create(TestData.MerchantId,
            Guid.Parse("E736CC81-5155-4119-84CD-537B81AA7F6D"),
            TestData.EstateId,
            TestData.MerchantId,
            TestData.AvailableBalance2,
            TestData.Balance2,
            TestData.ChangeAmount2,
            TestData.BalanceRecordReference);

        /// <summary>
        /// The security user identifier
        /// </summary>
        public static Guid EstateSecurityUserId = Guid.Parse("CBEE25E6-1B08-4023-B20C-CFE0AD746808");

        /// <summary>
        /// The estate security user added event
        /// </summary>
        public static EstateSecurityUserAddedEvent EstateSecurityUserAddedEvent =
            new EstateSecurityUserAddedEvent(TestData.EstateId, TestData.EstateSecurityUserId, TestData.EmailAddress);

        /// <summary>
        /// The merchant name
        /// </summary>
        public static String MerchantName = "Test Merchant 1";

        /// <summary>
        /// The merchant created event
        /// </summary>
        public static MerchantCreatedEvent MerchantCreatedEvent =
            new MerchantCreatedEvent(TestData.MerchantId, TestData.EstateId, TestData.MerchantName, DateTime.Now);

        public static String MerchantNumber = "12345678";

        public static Guid MerchantSecurityUserId = Guid.Parse("DFCE7A95-CB6D-442A-928A-F1B41D2AA4A9");

        /// <summary>
        /// The merchant security user added event
        /// </summary>
        public static MerchantSecurityUserAddedEvent MerchantSecurityUserAddedEvent =
            new MerchantSecurityUserAddedEvent(TestData.MerchantId, TestData.EstateId, TestData.MerchantSecurityUserId, TestData.EmailAddress);

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

        public static Guid TransactionId = Guid.Parse("4187AA70-4E36-451B-9B88-A164C08A9D4D");

        public static DateTime TransactionDateTime = DateTime.Now;

        public static String TransactionNumber = "1";

        public static String TransactionType = "Logon";

        public static String TransactionReference = "123456";

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
            new AdditionalRequestDataRecordedEvent(TestData.TransactionId, TestData.EstateId, TestData.MerchantId,
                                                      TestData.OperatorName, TestData.AdditionalRequestData);

        public static Dictionary<String, String> AdditionalRequestData =>
            new Dictionary<String, String>
            {
                {"Amount", "100.00"},
                {"CustomerAccountNumber", "123456789" }
            };

        public static Dictionary<String, String> AdditionalResponseData =>
            new Dictionary<String, String>
            {
                {"Amount", "100.00"},
                {"CustomerAccountNumber", "123456789" }
            };

        public static AdditionalResponseDataRecordedEvent AdditionalResponseDataRecordedEvent =
            new AdditionalResponseDataRecordedEvent(TestData.TransactionId, TestData.EstateId, TestData.MerchantId,
                                                       TestData.OperatorName, TestData.AdditionalResponseData);

        public static String AuthorisationCode = "ABCD1234";
        public static String ResponseCode = "0000";
        public static String ResponseMessage = "SUCCESS";
        public static String DeclinedResponseCode = "0001";
        public static String DeclinedResponseMessage = "DeclinedResponseMessage";

        public static String OperatorAuthorisationCode = "OP1234";

        public static String OperatorResponseCode = "200";

        public static String OperatorResponseMessage = "Topup Successful";

        public static String OperatorTransactionId = "SF12345";

        public static String DeclinedOperatorResponseCode = "400";

        public static String DeclinedOperatorResponseMessage = "Topup Failed";

        public static Boolean IsAuthorised = true;

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

        public static TransactionAuthorisedByOperatorEvent TransactionAuthorisedByOperatorEvent =
            new TransactionAuthorisedByOperatorEvent(TestData.TransactionId,                                                        TestData.EstateId,
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

        public static TransactionHasBeenCompletedEvent TransactionHasBeenCompletedEvent = new TransactionHasBeenCompletedEvent(TestData.TransactionId,
                                                                                                                                  TestData.EstateId,
                                                                                                                                  TestData.MerchantId,
                                                                                                                                  TestData.ResponseCode,
                                                                                                                                  TestData.ResponseMessage,
                                                                                                                                  TestData.IsAuthorised,
                                                                                                                                  TestData.TransactionCompletedDateTime,
                                                                                                                                  TestData.TransactionAmount);

        public static ReconciliationHasStartedEvent ReconciliationHasStartedEvent =
            new ReconciliationHasStartedEvent(TestData.TransactionId, TestData.EstateId, TestData.MerchantId, TestData.TransactionDateTime);

        public static OverallTotalsRecordedEvent OverallTotalsRecordedEvent =
            new OverallTotalsRecordedEvent(TestData.TransactionId,
                                              TestData.EstateId,
                                              TestData.MerchantId,
                                              TestData.ReconcilationTransactionCount,
                                              TestData.ReconcilationTransactionValue);

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

        public static Guid VoucherId = Guid.Parse("1736C058-5AC3-4AAC-8167-10DBAC2B7968");

        public static String OperatorIdentifier = "Voucher";

        public static Decimal VoucherValue = 10.00m;

        public static String VoucherCode = "1234GHT";
        public static DateTime VoucherGeneratedDate = new DateTime(2021, 12, 16);
        public static DateTime VoucherIssuedDate = new DateTime(2021, 12, 16);
        public static DateTime VoucherExpiryDate = new DateTime(2021,12,16);
        public static String VoucherMessage = String.Empty;

        public static String RecipientEmail = "testemail@recipient.co.uk";
        public static String RecipientMobile = "123455679";

        public static VoucherGeneratedEvent VoucherGeneratedEvent = new VoucherGeneratedEvent(TestData.VoucherId,
                                                                                                 TestData.EstateId,
                                                                                                 TestData.TransactionId,
                                                                                                 TestData.VoucherGeneratedDate,
                                                                                                 TestData.OperatorIdentifier,
                                                                                                 TestData.VoucherValue,
                                                                                                 TestData.VoucherCode,
                                                                                                 TestData.VoucherExpiryDate,
                                                                                                 TestData.VoucherMessage);

        public static VoucherIssuedEvent VoucherIssuedEvent = new VoucherIssuedEvent(TestData.VoucherId,
                                                                                        TestData.EstateId,
                                                                                        TestData.VoucherIssuedDate,
                                                                                        TestData.RecipientEmail,
                                                                                        TestData.RecipientMobile);

        public static VoucherFullyRedeemedEvent VoucherFullyRedeemedEvent = new VoucherFullyRedeemedEvent(TestData.VoucherId,
                                                                                                             TestData.EstateId,
                                                                                                             TestData.VoucherRedeemedDate);


        public static Decimal? TransactionAmount = 100.00m;

        public static Guid ContractId = Guid.Parse("D3F17288-2E3C-41F0-BD00-95047DC13BDA");

        public static String ContractDescription = "Test Contract";

        public static Guid ProductId = Guid.Parse("033BC002-FC65-4123-81E6-B7D21885BB0C");

        public static String ProductName = "Test Product 1";

        public static String ProductDisplayText = "100 KES";

        public static Decimal ProductFixedValue = 100.00m;

        public static Guid TransactionFeeId = Guid.Parse("F092CBF5-2F34-407B-8E50-21164C841A89");

        public static Int32 FeeCalculationType = 0;

        public static Int32 FeeType = 0;

        public static String TransactionFeeDescription = "Merchant Commission";

        public static Decimal FeeValue = 0.0005m;

        private static Decimal CalculatedValue = 2.95m;

        public static ContractCreatedEvent ContractCreatedEvent = new ContractCreatedEvent(TestData.ContractId, TestData.EstateId,
                                                                                              TestData.OperatorId, TestData.ContractDescription);

        public static FixedValueProductAddedToContractEvent FixedValueProductAddedToContractEvent = new FixedValueProductAddedToContractEvent(TestData.ContractId,
                                                                                                                                                 TestData.EstateId,
                                                                                                                                                 TestData.ProductId,
                                                                                                                                                 TestData.ProductName,
                                                                                                                                                 TestData.ProductDisplayText,
                                                                                                                                                 TestData.ProductFixedValue);

        public static VariableValueProductAddedToContractEvent VariableValueProductAddedToContractEvent = new VariableValueProductAddedToContractEvent(TestData.ContractId,
                                                                                                                                                 TestData.EstateId,
                                                                                                                                                 TestData.ProductId,
                                                                                                                                                 TestData.ProductName,
                                                                                                                                                 TestData.ProductDisplayText);

        public static TransactionFeeForProductAddedToContractEvent TransactionFeeForProductAddedToContractEvent = new TransactionFeeForProductAddedToContractEvent(TestData.ContractId,
                                                                                                                                                                      TestData.EstateId,
                                                                                                                                                                      TestData.ProductId,
                                                                                                                                                                      TestData.TransactionFeeId,
                                                                                                                                                                      TestData.TransactionFeeDescription,
                                                                                                                                                                      TestData.FeeCalculationType,
                                                                                                                                                                      TestData.FeeType,
                                                                                                                                                                      TestData.FeeValue);

        public static ProductDetailsAddedToTransactionEvent ProductDetailsAddedToTransactionEvent = new ProductDetailsAddedToTransactionEvent(TestData.TransactionId,
                                                                                                                                                 TestData.EstateId,
                                                                                                                                                 TestData.MerchantId,
                                                                                                                                                 TestData.ContractId,
                                                                                                                                                 TestData.ProductId);

        public static TransactionFeeForProductDisabledEvent TransactionFeeForProductDisabledEvent = new TransactionFeeForProductDisabledEvent(TestData.ContractId,
                                                                                                                                                 TestData.EstateId,
                                                                                                                                                 TestData.ProductId,
                                                                                                                                                 TestData.TransactionFeeId);

        public static MerchantFeeAddedToTransactionEvent MerchantFeeAddedToTransactionEvent = new MerchantFeeAddedToTransactionEvent(TestData.TransactionId,
            TestData.EstateId,
            TestData.MerchantId,
            TestData.CalculatedValue,
            TestData.FeeCalculationType,
            TestData.TransactionFeeId,
            TestData.FeeValue);

        public static ServiceProviderFeeAddedToTransactionEvent ServiceProviderFeeAddedToTransactionEvent = new ServiceProviderFeeAddedToTransactionEvent(TestData.TransactionId,
            TestData.EstateId,
            TestData.MerchantId,
            TestData.CalculatedValue,
            TestData.FeeCalculationType,
            TestData.TransactionFeeId,
            TestData.FeeValue);

        #endregion

        public static String StartDate = "20210104";

        public static String EndDate = "20210105";

        public static String CurrencyCode = "KES";

        public static TransactionsByDayModel TransactionsByDayModelEmptyTransactionDayModelList = new TransactionsByDayModel
                                                                                               {
                                                                                                   TransactionDayModels = new List<TransactionDayModel>()
                                                                                               };

        public static TransactionsByDayModel TransactionsByDayModelNullTransactionDayModelList = new TransactionsByDayModel
                                                                                              {
                                                                                                  TransactionDayModels = null
                                                                                              };
        

        public static TransactionsByDayModel TransactionsByDayModel = new TransactionsByDayModel
                                                                      {
                                                                          TransactionDayModels = new List<TransactionDayModel>
                                                                                                 {
                                                                                                     new TransactionDayModel
                                                                                                     {
                                                                                                         CurrencyCode = TestData.CurrencyCode,
                                                                                                         Date = new DateTime(2020, 10, 1),
                                                                                                         ValueOfTransactions = 1000.00m,
                                                                                                         NumberOfTransactions = 50
                                                                                                     },
                                                                                                     new TransactionDayModel
                                                                                                     {
                                                                                                         CurrencyCode = TestData.CurrencyCode,
                                                                                                         Date = new DateTime(2020, 10, 2),
                                                                                                         ValueOfTransactions = 1510.00m,
                                                                                                         NumberOfTransactions = 65
                                                                                                     }
                                                                                                 }
                                                                      };

        public static TransactionsByWeekModel TransactionsByWeekModel = new TransactionsByWeekModel
        {
            TransactionWeekModels = new List<TransactionWeekModel>
                                                                                                 {
                                                                                                     new TransactionWeekModel
                                                                                                     {
                                                                                                         CurrencyCode = TestData.CurrencyCode,
                                                                                                         WeekNumber = 1,
                                                                                                         Year = 2020,
                                                                                                         ValueOfTransactions = 1000.00m,
                                                                                                         NumberOfTransactions = 50
                                                                                                     },
                                                                                                     new TransactionWeekModel
                                                                                                     {
                                                                                                         CurrencyCode = TestData.CurrencyCode,
                                                                                                         WeekNumber = 2,
                                                                                                         Year = 2020,
                                                                                                         ValueOfTransactions = 1510.00m,
                                                                                                         NumberOfTransactions = 65
                                                                                                     }
                                                                                                 }
        };

        public static TransactionsByWeekModel TransactionsByWeekModelNullTransactionWeekModelList = new TransactionsByWeekModel
        {
                                                                                                     TransactionWeekModels = null
                                                                                                 };

        public static TransactionsByWeekModel TransactionsByWeekModelEmptyTransactionWeekModelList = new TransactionsByWeekModel
        {
                                                                                                        TransactionWeekModels = new List<TransactionWeekModel>()
                                                                                                    };

        public static TransactionDayModel TransactionDayModel = new TransactionDayModel
                                                                {
                                                                    CurrencyCode = TestData.CurrencyCode,
                                                                    Date = new DateTime(2020, 10, 1),
                                                                    ValueOfTransactions = 1000.00m,
                                                                    NumberOfTransactions = 50
                                                                };

        public static TransactionWeekModel TransactionWeekModel = new TransactionWeekModel
        {
                                                                      CurrencyCode = TestData.CurrencyCode,
                                                                      WeekNumber = 1,
                                                                      Year = 2020,
                                                                      ValueOfTransactions = 1000.00m,
                                                                      NumberOfTransactions = 50
                                                                  };

        public static TransactionMonthModel TransactionMonthModel = new TransactionMonthModel
        {
                                                                    CurrencyCode = TestData.CurrencyCode,
                                                                    MonthNumber = 1,
                                                                    Year = 2020,
                                                                    ValueOfTransactions = 1000.00m,
                                                                    NumberOfTransactions = 50
                                                                };

        public static TransactionsByMonthModel TransactionsByMonthModel = new TransactionsByMonthModel
        {
            TransactionMonthModels = new List<TransactionMonthModel>
                                                                                                 {
                                                                                                     new TransactionMonthModel
                                                                                                     {
                                                                                                         CurrencyCode = TestData.CurrencyCode,
                                                                                                         MonthNumber = 1,
                                                                                                         Year = 2020,
                                                                                                         ValueOfTransactions = 1000.00m,
                                                                                                         NumberOfTransactions = 50
                                                                                                     },
                                                                                                     new TransactionMonthModel
                                                                                                     {
                                                                                                         CurrencyCode = TestData.CurrencyCode,
                                                                                                         MonthNumber = 2,
                                                                                                         Year = 2020,
                                                                                                         ValueOfTransactions = 1510.00m,
                                                                                                         NumberOfTransactions = 65
                                                                                                     }
                                                                                                 }
        };

        public static TransactionsByMonthModel TransactionsByMonthModelNullTransactionMonthModelList = new TransactionsByMonthModel
                                                                                                    {
                                                                                                        TransactionMonthModels = null
                                                                                                    };

        public static TransactionsByMonthModel TransactionsByMonthModelEmptyTransactionMonthModelList = new TransactionsByMonthModel
                                                                                                        {
                                                                                                            TransactionMonthModels = new List<TransactionMonthModel>()
                                                                                                        };

        private static DateTime VoucherRedeemedDate = new DateTime(2021, 12, 16);

        private static DateTime TransactionCompletedDateTime = new DateTime(2021, 12, 16);

        public static TransactionsByMerchantModel TransactionsByMerchantModel = new TransactionsByMerchantModel
                                                                                {
                                                                                    TransactionMerchantModels = new List<TransactionMerchantModel>
                                                                                                                {
                                                                                                                    new TransactionMerchantModel
                                                                                                                    {
                                                                                                                        CurrencyCode = TestData.CurrencyCode,
                                                                                                                        MerchantId = TestData.MerchantId,
                                                                                                                        ValueOfTransactions = 1000.00m,
                                                                                                                        NumberOfTransactions = 50,
                                                                                                                        MerchantName = TestData.MerchantName
                                                                                                                    }
                                                                                                                }
                                                                                };

        public static TransactionsByMerchantModel TransactionsByMerchantModelNullTransactionMerchantModelList = new TransactionsByMerchantModel
                                                                                                                {
                                                                                                                    TransactionMerchantModels = null
                                                                                                                };

        public static TransactionsByMerchantModel TransactionsByMerchantModelEmptyTransactionMerchantModelList = new TransactionsByMerchantModel
                                                                                                                {
                                                                                                                    TransactionMerchantModels = new List<TransactionMerchantModel>()
                                                                                                                };

        public static TransactionMerchantModel TransactionMerchantModel = new TransactionMerchantModel
                                                                          {
                                                                              CurrencyCode = TestData.CurrencyCode,
                                                                              MerchantId = TestData.MerchantId,
                                                                              ValueOfTransactions = 1000.00m,
                                                                              NumberOfTransactions = 50,
                                                                              MerchantName = TestData.MerchantName
                                                                          };

        public static TransactionsByOperatorModel TransactionsByOperatorModel = new TransactionsByOperatorModel
                                                                                {
                                                                                    TransactionOperatorModels = new List<TransactionOperatorModel>
                                                                                                                {
                                                                                                                    new TransactionOperatorModel
                                                                                                                    {
                                                                                                                        CurrencyCode = TestData.CurrencyCode,
                                                                                                                        ValueOfTransactions = 1000.00m,
                                                                                                                        NumberOfTransactions = 50,
                                                                                                                        OperatorName = TestData.OperatorName
                                                                                                                    }
                                                                                                                }
                                                                                };

        public static TransactionsByOperatorModel TransactionsByOperatorModelNullTransactionOperatorModelList = new TransactionsByOperatorModel
        {
                                                                                                                    TransactionOperatorModels = null
                                                                                                                };

        public static TransactionsByOperatorModel TransactionsByOperatorModelEmptyTransactionOperatorModelList = new TransactionsByOperatorModel
            {
                TransactionOperatorModels = new List<TransactionOperatorModel>()
            };

        public static TransactionOperatorModel TransactionOperatorModel = new TransactionOperatorModel
                                                                          {
                                                                              CurrencyCode = TestData.CurrencyCode,
                                                                              ValueOfTransactions = 1000.00m,
                                                                              NumberOfTransactions = 50,
                                                                              OperatorName = TestData.OperatorName
                                                                          };
    }
}