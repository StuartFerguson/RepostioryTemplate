namespace EstateReporting.BusinessLogic.Tests
{
    using System;
    using System.Threading;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using EstateReporting.Tests;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Moq;
    using Repository;
    using Shared.Logger;
    using Shouldly;
    using Xunit;
    using EstateSecurityUserAddedEvent = EstateManagement.Estate.DomainEvents.SecurityUserAddedEvent;
    using MerchantSecurityUserAddedEvent = EstateManagement.Merchant.DomainEvents.SecurityUserAddedEvent;

    public class EstateDomainEventHandlerTests
    {
        [Fact]
        public void EstateDomainEventHandler_CanBeCreated_IsCreated()
        {
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            eventHandler.ShouldNotBeNull();
        }

        [Fact]
        public void EstateDomainEventHandler_EstateCreatedEvent_EventIsHandled()
        {
            EstateCreatedEvent estateCreatedEvent = TestData.EstateCreatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                                {
                                    await eventHandler.Handle(estateCreatedEvent, CancellationToken.None);
                                });
        }

        [Fact]
        public void EstateDomainEventHandler_SecurityUserAddedEvent_EventIsHandled()
        {
            EstateSecurityUserAddedEvent securityUserAddedEvent = TestData.EstateSecurityUserAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(securityUserAddedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void EstateDomainEventHandler_OperatorAddedToEstateEvent_EventIsHandled()
        {
            OperatorAddedToEstateEvent operatorAddedToEstateEvent = TestData.OperatorAddedToEstateEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(operatorAddedToEstateEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_MerchantCreatedEvent_EventIsHandled()
        {
            MerchantCreatedEvent merchantCreatedEvent = TestData.MerchantCreatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(merchantCreatedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_AddressAddedEvent_EventIsHandled()
        {
            AddressAddedEvent addressAddedEvent = TestData.AddressAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(addressAddedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_ContactAddedEvent_EventIsHandled()
        {
            ContactAddedEvent contactAddedEvent = TestData.ContactAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(contactAddedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_DeviceAddedToMerchantEvent_EventIsHandled()
        {
            DeviceAddedToMerchantEvent deviceAddedToMerchantEvent = TestData.DeviceAddedToMerchantEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(deviceAddedToMerchantEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_SecurityUserAddedEvent_EventIsHandled()
        {
            MerchantSecurityUserAddedEvent merchantSecurityUserAddedEvent = TestData.MerchantSecurityUserAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(merchantSecurityUserAddedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_OperatorAssignedToMerchantEvent_EventIsHandled()
        {
            OperatorAssignedToMerchantEvent operatorAssignedToMerchantEvent = TestData.OperatorAssignedToMerchantEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(operatorAssignedToMerchantEvent, CancellationToken.None);
                            });
        }
    }
}