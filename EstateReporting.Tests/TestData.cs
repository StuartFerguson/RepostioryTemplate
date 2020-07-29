namespace EstateReporting.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using EstateManagement.Contract.DomainEvents;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using TransactionProcessor.Transaction.DomainEvents;
    using EstateSecurityUserAddedEvent = EstateManagement.Estate.DomainEvents.SecurityUserAddedEvent;
    using MerchantSecurityUserAddedEvent = EstateManagement.Merchant.DomainEvents.SecurityUserAddedEvent;

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
        public static AddressAddedEvent AddressAddedEvent = AddressAddedEvent.Create(TestData.MerchantId,
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
        public static ContactAddedEvent ContactAddedEvent = ContactAddedEvent.Create(TestData.MerchantId,
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
            DeviceAddedToMerchantEvent.Create(TestData.MerchantId, TestData.EstateId, TestData.DeviceId, TestData.DeviceIdentifier);

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
        public static EstateCreatedEvent EstateCreatedEvent = EstateCreatedEvent.Create(TestData.EstateId, TestData.EstateName);

        /// <summary>
        /// The security user identifier
        /// </summary>
        public static Guid EstateSecurityUserId = Guid.Parse("CBEE25E6-1B08-4023-B20C-CFE0AD746808");

        /// <summary>
        /// The estate security user added event
        /// </summary>
        public static EstateSecurityUserAddedEvent EstateSecurityUserAddedEvent =
            EstateSecurityUserAddedEvent.Create(TestData.EstateId, TestData.EstateSecurityUserId, TestData.EmailAddress);

        /// <summary>
        /// The merchant name
        /// </summary>
        public static String MerchantName = "Test Merchant 1";

        /// <summary>
        /// The merchant created event
        /// </summary>
        public static MerchantCreatedEvent MerchantCreatedEvent =
            MerchantCreatedEvent.Create(TestData.MerchantId, TestData.EstateId, TestData.MerchantName, DateTime.Now);

        public static String MerchantNumber = "12345678";

        public static Guid MerchantSecurityUserId = Guid.Parse("DFCE7A95-CB6D-442A-928A-F1B41D2AA4A9");

        /// <summary>
        /// The merchant security user added event
        /// </summary>
        public static MerchantSecurityUserAddedEvent MerchantSecurityUserAddedEvent =
            MerchantSecurityUserAddedEvent.Create(TestData.MerchantId, TestData.EstateId, TestData.MerchantSecurityUserId, TestData.EmailAddress);

        public static Guid OperatorId = Guid.Parse("DCDC5054-C026-4492-AFED-C74E4DEFD00C");

        public static String OperatorName = "Test Operator 1";

        public static Boolean RequireCustomMerchantNumber = true;

        public static Boolean RequireCustomTerminalNumber = true;

        public static OperatorAddedToEstateEvent OperatorAddedToEstateEvent =
            OperatorAddedToEstateEvent.Create(TestData.EstateId,
                                              TestData.OperatorId,
                                              TestData.OperatorName,
                                              TestData.RequireCustomMerchantNumber,
                                              TestData.RequireCustomTerminalNumber);

        public static String TerminalNumber = "12345679";

        public static OperatorAssignedToMerchantEvent OperatorAssignedToMerchantEvent = OperatorAssignedToMerchantEvent.Create(TestData.MerchantId,
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

        public static TransactionHasStartedEvent TransactionHasStartedEvent = TransactionHasStartedEvent.Create(TestData.TransactionId,
                                                                                                                TestData.EstateId,
                                                                                                                TestData.MerchantId,
                                                                                                                TestData.TransactionDateTime,
                                                                                                                TestData.TransactionNumber,
                                                                                                                TestData.TransactionType,
                                                                                                                TestData.TransactionReference,
                                                                                                                TestData.DeviceIdentifier,
                                                                                                                TestData.TransactionAmount);

        public static AdditionalRequestDataRecordedEvent AdditionalRequestDataRecordedEvent =
            AdditionalRequestDataRecordedEvent.Create(TestData.TransactionId, TestData.EstateId, TestData.MerchantId,
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
            AdditionalResponseDataRecordedEvent.Create(TestData.TransactionId, TestData.EstateId, TestData.MerchantId,
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
            TransactionHasBeenLocallyAuthorisedEvent.Create(TestData.TransactionId,
                                                            TestData.EstateId,
                                                            TestData.MerchantId,
                                                            TestData.AuthorisationCode,
                                                            TestData.ResponseCode,
                                                            TestData.ResponseMessage);

        public static TransactionHasBeenLocallyDeclinedEvent TransactionHasBeenLocallyDeclinedEvent =
            TransactionHasBeenLocallyDeclinedEvent.Create(TestData.TransactionId,
                                                          TestData.EstateId,
                                                          TestData.MerchantId,
                                                          TestData.DeclinedResponseCode,
                                                          TestData.DeclinedResponseMessage);

        public static TransactionAuthorisedByOperatorEvent TransactionAuthorisedByOperatorEvent =
            TransactionAuthorisedByOperatorEvent.Create(TestData.TransactionId,
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
            TransactionDeclinedByOperatorEvent.Create(TestData.TransactionId,
                                                      TestData.EstateId,
                                                      TestData.MerchantId,
                                                      TestData.OperatorName,
                                                      TestData.DeclinedOperatorResponseCode,
                                                      TestData.DeclinedOperatorResponseMessage,
                                                      TestData.DeclinedResponseCode,
                                                      TestData.DeclinedResponseMessage);

        public static TransactionHasBeenCompletedEvent TransactionHasBeenCompletedEvent = TransactionHasBeenCompletedEvent.Create(TestData.TransactionId,
                                                                                                                                  TestData.EstateId,
                                                                                                                                  TestData.MerchantId,
                                                                                                                                  TestData.ResponseCode,
                                                                                                                                  TestData.ResponseMessage,
                                                                                                                                  TestData.IsAuthorised);

        public static Decimal? TransactionAmount = 100.00m;

        public static Guid ContractId = Guid.Parse("D3F17288-2E3C-41F0-BD00-95047DC13BDA");

        public static String ContractDescription = "Test Contract";

        public static Guid ProductId = Guid.Parse("033BC002-FC65-4123-81E6-B7D21885BB0C");

        public static String ProductName = "Test Product 1";

        public static String ProductDisplayText = "100 KES";

        public static Decimal ProductFixedValue = 100.00m;

        public static Guid TransactionFeeId = Guid.Parse("F092CBF5-2F34-407B-8E50-21164C841A89");

        public static Int32 FeeCalculationType = 0;

        public static String TransactionFeeDescription = "Merchant Commission";

        public static Decimal FeeValue = 0.0005m;

        public static ContractCreatedEvent ContractCreatedEvent = ContractCreatedEvent.Create(TestData.ContractId, TestData.EstateId,
                                                                                              TestData.OperatorId, TestData.ContractDescription);

        public static FixedValueProductAddedToContractEvent FixedValueProductAddedToContractEvent = FixedValueProductAddedToContractEvent.Create(TestData.ContractId,
                                                                                                                                                 TestData.EstateId,
                                                                                                                                                 TestData.ProductId,
                                                                                                                                                 TestData.ProductName,
                                                                                                                                                 TestData.ProductDisplayText,
                                                                                                                                                 TestData.ProductFixedValue);

        public static VariableValueProductAddedToContractEvent VariableValueProductAddedToContractEvent = VariableValueProductAddedToContractEvent.Create(TestData.ContractId,
                                                                                                                                                 TestData.EstateId,
                                                                                                                                                 TestData.ProductId,
                                                                                                                                                 TestData.ProductName,
                                                                                                                                                 TestData.ProductDisplayText);

        public static TransactionFeeForProductAddedToContractEvent TransactionFeeForProductAddedToContractEvent = TransactionFeeForProductAddedToContractEvent.Create(TestData.ContractId,
                                                                                                                                                                      TestData.EstateId,
                                                                                                                                                                      TestData.ProductId,
                                                                                                                                                                      TestData.TransactionFeeId,
                                                                                                                                                                      TestData.TransactionFeeDescription,
                                                                                                                                                                      TestData.FeeCalculationType,
                                                                                                                                                                      TestData.FeeValue);


        #endregion
    }
}