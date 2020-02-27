using System;

namespace EstateReporting.Tests
{
    using EstateManagement.Estate.DomainEvents;

    public class TestData
    {
        public static Guid EstateId = Guid.Parse("8DC6C30F-AD7C-4533-83DF-EA1C05461486");

        public static Guid SecurityUserId = Guid.Parse("CBEE25E6-1B08-4023-B20C-CFE0AD746808");

        public static String EstateName = "Test Estate 1";

        public static String EmailAddress = "testuser1@testestate1.co.uk";
        
        public static EstateCreatedEvent EstateCreatedEvent = EstateCreatedEvent.Create(TestData.EstateId, TestData.EstateName);

        public static SecurityUserAddedEvent SecurityUserAddedEvent = SecurityUserAddedEvent.Create(TestData.EstateId, TestData.SecurityUserId, TestData.EmailAddress);
    }
}
