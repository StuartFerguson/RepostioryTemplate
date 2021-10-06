namespace EstateReporting.BusinessLogic.Tests
{
    using System.Threading;
    using FileProcessor.File.DomainEvents;
    using FileProcessor.FileImportLog.DomainEvents;
    using Moq;
    using Repository;
    using Shared.Logger;
    using Shouldly;
    using Testing;
    using Xunit;

    public class FileProcessorDomainEventHandlerTests
    {
        #region Methods

        [Fact]
        public void FileProcessorDomainEventHandler_CanBeCreated_IsCreated()
        {
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            FileProcessorDomainEventHandler eventHandler = new FileProcessorDomainEventHandler(estateReportingRepository.Object);

            eventHandler.ShouldNotBeNull();
        }

        [Fact]
        public void FileProcessorDomainEventHandler_FileAddedToImportLogEvent_EventIsHandled()
        {
            FileAddedToImportLogEvent domainEvent = TestData.FileAddedToImportLogEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            FileProcessorDomainEventHandler eventHandler = new FileProcessorDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void FileProcessorDomainEventHandler_FileCreatedEvent_EventIsHandled()
        {
            FileCreatedEvent domainEvent = TestData.FileCreatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            FileProcessorDomainEventHandler eventHandler = new FileProcessorDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void FileProcessorDomainEventHandler_FileLineAddedEvent_EventIsHandled()
        {
            FileLineAddedEvent domainEvent = TestData.FileLineAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            FileProcessorDomainEventHandler eventHandler = new FileProcessorDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void FileProcessorDomainEventHandler_FileLineProcessingFailedEvent_EventIsHandled()
        {
            FileLineProcessingFailedEvent domainEvent = TestData.FileLineProcessingFailedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            FileProcessorDomainEventHandler eventHandler = new FileProcessorDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void FileProcessorDomainEventHandler_FileLineProcessingIgnoredEvent_EventIsHandled()
        {
            FileLineProcessingIgnoredEvent domainEvent = TestData.FileLineProcessingIgnoredEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            FileProcessorDomainEventHandler eventHandler = new FileProcessorDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void FileProcessorDomainEventHandler_FileLineProcessingSuccessfulEvent_EventIsHandled()
        {
            FileLineProcessingSuccessfulEvent domainEvent = TestData.FileLineProcessingSuccessfulEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            FileProcessorDomainEventHandler eventHandler = new FileProcessorDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void FileProcessorDomainEventHandler_FileProcessingCompletedEvent_EventIsHandled()
        {
            FileProcessingCompletedEvent domainEvent = TestData.FileProcessingCompletedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            FileProcessorDomainEventHandler eventHandler = new FileProcessorDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void FileProcessorDomainEventHandler_ImportLogCreatedEvent_EventIsHandled()
        {
            ImportLogCreatedEvent domainEvent = TestData.ImportLogCreatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            FileProcessorDomainEventHandler eventHandler = new FileProcessorDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        #endregion
    }
}