namespace EstateReporting.Factories
{
    using BusinessLogic;
    using DataTransferObjects;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IModelFactory
    {
        #region Methods

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionsByDayResponse ConvertFrom(TransactionsByDayModel model);

        #endregion
    }
}