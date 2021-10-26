namespace EstateReporting.Common.Examples
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ExampleData
    {
        #region Fields

        /// <summary>
        /// The merchant identifier
        /// </summary>
        internal static Guid MerchantId = Guid.Parse("DEBABACA-5646-4758-9ED0-F1452AEEBDA6");

        /// <summary>
        /// The merchant name
        /// </summary>
        internal static String MerchantName = "Example Merchant";

        /// <summary>
        /// The month number
        /// </summary>
        internal static Int32 MonthNumber = 4;

        /// <summary>
        /// The operator name
        /// </summary>
        internal static String OperatorName = "Example Operator";

        /// <summary>
        /// The transaction day number of transactions
        /// </summary>
        internal static Int32 TransactionDayNumberOfTransactions = 10;

        internal static Int32 SettlementDayNumberOfTransactions = 10;

        /// <summary>
        /// The transaction day response date
        /// </summary>
        internal static DateTime TransactionDayResponseDate = new DateTime(2021, 4, 15);

        internal static DateTime SettlementDayResponseDate = new DateTime(2021, 4, 15);

        /// <summary>
        /// The transaction day value of transactions
        /// </summary>
        internal static Decimal TransactionDayValueOfTransactions = 1000.00m;

        internal static Decimal SettlementDayValueOfTransactions = 1000.00m;

        /// <summary>
        /// The transaction merchant number of transactions
        /// </summary>
        internal static Int32 TransactionMerchantNumberOfTransactions = 100;

        /// <summary>
        /// The transaction merchant value of transactions
        /// </summary>
        internal static Decimal TransactionMerchantValueOfTransactions = 10000.00m;


        /// <summary>
        /// The transaction merchant number of transactions
        /// </summary>
        internal static Int32 SettlementMerchantNumberOfTransactions = 100;

        /// <summary>
        /// The transaction merchant value of transactions
        /// </summary>
        internal static Decimal SettlementMerchantValueOfTransactions = 10000.00m;

        /// <summary>
        /// The transaction month number of transactions
        /// </summary>
        internal static Int32 TransactionMonthNumberOfTransactions = 100;

        /// <summary>
        /// The transaction month value of transactions
        /// </summary>
        internal static Decimal TransactionMonthValueOfTransactions = 10000.00m;

        /// <summary>
        /// The transaction month number of transactions
        /// </summary>
        internal static Int32 SettlementMonthNumberOfTransactions = 100;

        /// <summary>
        /// The transaction month value of transactions
        /// </summary>
        internal static Decimal SettlementMonthValueOfTransactions = 10000.00m;

        /// <summary>
        /// The transaction operator number of transactions
        /// </summary>
        internal static Int32 TransactionOperatorNumberOfTransactions = 50;

        /// <summary>
        /// The transaction operator value of transactions
        /// </summary>
        internal static Decimal TransactionOperatorValueOfTransactions = 5000.00m;

        /// <summary>
        /// The transaction operator number of transactions
        /// </summary>
        internal static Int32 SettlementOperatorNumberOfTransactions = 50;

        /// <summary>
        /// The transaction operator value of transactions
        /// </summary>
        internal static Decimal SettlementOperatorValueOfTransactions = 5000.00m;

        /// <summary>
        /// The transaction week number of transactions
        /// </summary>
        internal static Int32 TransactionWeekNumberOfTransactions = 25;

        internal static Int32 SettlementWeekNumberOfTransactions = 25;

        /// <summary>
        /// The transaction week value of transactions
        /// </summary>
        internal static Decimal TransactionWeekValueOfTransactions = 2500.00m;

        internal static Decimal SettlementWeekValueOfTransactions = 2500.00m;

        /// <summary>
        /// The week number
        /// </summary>
        internal static Int32 WeekNumber = 16;

        /// <summary>
        /// The year number
        /// </summary>
        internal static Int32 YearNumber = 2021;

        #endregion
    }
}