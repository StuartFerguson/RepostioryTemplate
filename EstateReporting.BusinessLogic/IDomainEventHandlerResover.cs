namespace EstateReporting.BusinessLogic
{
    using System.Collections.Generic;
    using Shared.DomainDrivenDesign.EventSourcing;

    /// <summary>
    /// 
    /// </summary>
    public interface IDomainEventHandlerResolver
    {
        #region Methods

        /// <summary>
        /// Gets the domain event handlers.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <returns></returns>
        List<IDomainEventHandler> GetDomainEventHandlers(DomainEvent domainEvent);

        #endregion
    }
}