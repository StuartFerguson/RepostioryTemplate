using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateReporting.Common.Examples
{
    using DataTransferObjects;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Swashbuckle.AspNetCore.Filters;

    public class TransactionDayResponseExample : IExamplesProvider<TransactionDayResponse>
    {
        public TransactionDayResponse GetExamples()
        {
            return new TransactionDayResponse
                   {
                       CurrencyCode = String.Empty,
                       Date = ExampleData.TransactionDayResponseDate,
                       NumberOfTransactions = ExampleData.TransactionDayNumberOfTransactions,
                       ValueOfTransactions = ExampleData.TransactionDayValueOfTransactions
                   };
        }
    }

    public class TransactionMerchantResponseExample : IExamplesProvider<TransactionMerchantResponse>
    {
        public TransactionMerchantResponse GetExamples()
        {
            return new TransactionMerchantResponse
                   {
                       CurrencyCode = String.Empty,
                       NumberOfTransactions = ExampleData.TransactionMerchantNumberOfTransactions,
                       ValueOfTransactions = ExampleData.TransactionMerchantValueOfTransactions,
                       MerchantId = ExampleData.MerchantId,
                       MerchantName = ExampleData.MerchantName
                   };
        }
    }

    public class TransactionWeekResponseExample : IExamplesProvider<TransactionWeekResponse>
    {
        public TransactionWeekResponse GetExamples()
        {
            return new TransactionWeekResponse
                   {
                       CurrencyCode = String.Empty,
                       NumberOfTransactions = ExampleData.TransactionWeekNumberOfTransactions,
                       ValueOfTransactions = ExampleData.TransactionWeekValueOfTransactions,
                       WeekNumber = ExampleData.WeekNumber,
                       Year = ExampleData.YearNumber
                   };
        }
    }

    public class TransactionMonthResponseExample : IExamplesProvider<TransactionMonthResponse>
    {
        public TransactionMonthResponse GetExamples()
        {
            return new TransactionMonthResponse
                   {
                       CurrencyCode = String.Empty,
                       NumberOfTransactions = ExampleData.TransactionMonthNumberOfTransactions,
                       ValueOfTransactions = ExampleData.TransactionMonthValueOfTransactions,
                       MonthNumber = ExampleData.MonthNumber,
                       Year = ExampleData.YearNumber
                   };
        }
    }

    public class TransactionOperatorResponseExample : IExamplesProvider<TransactionOperatorResponse>
    {
        public TransactionOperatorResponse GetExamples()
        {
            return new TransactionOperatorResponse
                   {
                       NumberOfTransactions = ExampleData.TransactionOperatorNumberOfTransactions,
                       ValueOfTransactions = ExampleData.TransactionOperatorValueOfTransactions,
                       CurrencyCode = String.Empty,
                       OperatorName = ExampleData.OperatorName
                   };
        }
    }

    public class TransactionsByDayResponseExample : IExamplesProvider<TransactionsByDayResponse>
    {
        public TransactionsByDayResponse GetExamples()
        {
            return new TransactionsByDayResponse
                   {
                       TransactionDayResponses = new List<TransactionDayResponse>
                                                 {
                                                     new TransactionDayResponse
                                                     {
                                                         CurrencyCode = String.Empty,
                                                         Date = ExampleData.TransactionDayResponseDate,
                                                         NumberOfTransactions = ExampleData.TransactionDayNumberOfTransactions,
                                                         ValueOfTransactions = ExampleData.TransactionDayValueOfTransactions
                                                     }
                                                 }
                   };
        }
    }

    public class TransactionsByMonthResponseExample : IExamplesProvider<TransactionsByMonthResponse>
    {
        public TransactionsByMonthResponse GetExamples()
        {
            return new TransactionsByMonthResponse
                   {
                       TransactionMonthResponses = new List<TransactionMonthResponse>
                                                   {
                                                       new TransactionMonthResponse
                                                       {
                                                           CurrencyCode = String.Empty,
                                                           NumberOfTransactions = ExampleData.TransactionMonthNumberOfTransactions,
                                                           ValueOfTransactions = ExampleData.TransactionMonthValueOfTransactions,
                                                           MonthNumber = ExampleData.MonthNumber,
                                                           Year = ExampleData.YearNumber
                                                       }
                                                   }
                   };
        }
    }

    public class TransactionsByMerchantResponseExample : IExamplesProvider<TransactionsByMerchantResponse>
    {
        public TransactionsByMerchantResponse GetExamples()
        {
            return new TransactionsByMerchantResponse
                   {
                       TransactionMerchantResponses = new List<TransactionMerchantResponse>
                                                      {
                                                          new TransactionMerchantResponse
                                                          {
                                                              CurrencyCode = String.Empty,
                                                              NumberOfTransactions = ExampleData.TransactionMerchantNumberOfTransactions,
                                                              ValueOfTransactions = ExampleData.TransactionMerchantValueOfTransactions,
                                                              MerchantId = ExampleData.MerchantId,
                                                              MerchantName = ExampleData.MerchantName
                                                          }
                                                      }
                   };
        }
    }

    public class TransactionsByOperatorResponseExample : IExamplesProvider<TransactionsByOperatorResponse>
    {
        public TransactionsByOperatorResponse GetExamples()
        {
            return new TransactionsByOperatorResponse
                   {
                       TransactionOperatorResponses = new List<TransactionOperatorResponse>
                                                      {
                                                          new TransactionOperatorResponse
                                                          {
                                                              NumberOfTransactions = ExampleData.TransactionOperatorNumberOfTransactions,
                                                              ValueOfTransactions = ExampleData.TransactionOperatorValueOfTransactions,
                                                              CurrencyCode = String.Empty,
                                                              OperatorName = ExampleData.OperatorName
                                                          }
                                                      }
                   };
        }
    }

    public class TransactionsByWeekResponseExample : IExamplesProvider<TransactionsByWeekResponse>
    {
        public TransactionsByWeekResponse GetExamples()
        {
            return new TransactionsByWeekResponse
                   {
                       TransactionWeekResponses = new List<TransactionWeekResponse>
                                                      {
                                                          new TransactionWeekResponse
                                                          {
                                                              NumberOfTransactions = ExampleData.TransactionWeekNumberOfTransactions,
                                                              ValueOfTransactions = ExampleData.TransactionWeekValueOfTransactions,
                                                              CurrencyCode = String.Empty,
                                                              WeekNumber = ExampleData.WeekNumber,
                                                              Year = ExampleData.YearNumber
                                                          }
                                                      }
                   };
        }
    }

    public static class ExampleData
    {
        internal static String OperatorName = "Example Operator";

        internal static DateTime TransactionDayResponseDate = new DateTime(2021, 4, 15);

        internal static Int32 TransactionDayNumberOfTransactions = 10;

        internal static Int32 TransactionWeekNumberOfTransactions = 25;

        internal static Decimal TransactionWeekValueOfTransactions = 2500.00m;

        internal static Decimal TransactionDayValueOfTransactions = 1000.00m;

        internal static Int32 TransactionMonthNumberOfTransactions = 100;

        internal static Decimal TransactionMonthValueOfTransactions = 10000.00m;

        internal static Int32 TransactionOperatorNumberOfTransactions = 50;

        internal static Decimal TransactionOperatorValueOfTransactions = 5000.00m;

        internal static Int32 TransactionMerchantNumberOfTransactions = 100;

        internal static Decimal TransactionMerchantValueOfTransactions = 10000.00m;

        internal static Guid MerchantId = Guid.Parse("DEBABACA-5646-4758-9ED0-F1452AEEBDA6");

        internal static String MerchantName = "Example Merchant";

        internal static Int32 MonthNumber = 4;

        internal static Int32 WeekNumber = 16;

        internal static Int32 YearNumber = 2021;

    }
}