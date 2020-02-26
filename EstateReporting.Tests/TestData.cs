using System;

namespace EstateReporting.Tests
{
    using EstateManagement.Estate.DomainEvents;

    public class TestData
    {
        public static Guid EstateId = Guid.Parse("8DC6C30F-AD7C-4533-83DF-EA1C05461486");

        public static String EstateName = "Test Estate 1";
        
        public static EstateCreatedEvent EstateCreatedEvent = EstateCreatedEvent.Create(TestData.EstateId, TestData.EstateName);
    }
}
