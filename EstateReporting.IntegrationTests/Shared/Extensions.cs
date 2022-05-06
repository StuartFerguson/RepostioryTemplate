namespace EstateReporting.IntegrationTests.Shared
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        #region Methods

        /// <summary>
        /// Converts to datetime.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this Guid guid)
        {
            var bytes = guid.ToByteArray();

            Array.Resize(ref bytes, 8);

            return new DateTime(BitConverter.ToInt64(bytes));
        }

        /// <summary>
        /// Converts to guid.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static Guid ToGuid(this DateTime dt)
        {
            var bytes = BitConverter.GetBytes(dt.Ticks);

            Array.Resize(ref bytes, 16);

            return new Guid(bytes);
        }

        #endregion
    }

    public static class Helpers
    {
        public static Guid CalculateSettlementAggregateId(DateTime settlementDate,
                                                    Guid estateId)
        {
            Guid aggregateId = GuidCalculator.Combine(estateId, settlementDate.ToGuid());
            return aggregateId;
        }
    }

    public static class GuidCalculator
    {
        #region Methods

        /// <summary>
        /// Combines the specified GUIDs into a new GUID.
        /// </summary>
        /// <param name="firstGuid">The first unique identifier.</param>
        /// <param name="secondGuid">The second unique identifier.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>Guid.</returns>
        public static Guid Combine(Guid firstGuid,
                                   Guid secondGuid,
                                   Byte offset)
        {
            Byte[] firstAsBytes = firstGuid.ToByteArray();
            Byte[] secondAsBytes = secondGuid.ToByteArray();

            Byte[] newBytes = new Byte[16];

            for (Int32 i = 0; i < 16; i++)
            {
                // Add and truncate any overflow
                newBytes[i] = (Byte)(firstAsBytes[i] + secondAsBytes[i] + offset);
            }

            return new Guid(newBytes);
        }

        /// <summary>
        /// Combines the specified GUIDs into a new GUID.
        /// </summary>
        /// <param name="firstGuid">The first unique identifier.</param>
        /// <param name="secondGuid">The second unique identifier.</param>
        /// <returns>Guid.</returns>
        public static Guid Combine(Guid firstGuid,
                                   Guid secondGuid)
        {
            return GuidCalculator.Combine(firstGuid,
                                          secondGuid,
                                          0);
        }

        /// <summary>
        /// Combines the specified first unique identifier.
        /// </summary>
        /// <param name="firstGuid">The first unique identifier.</param>
        /// <param name="secondGuid">The second unique identifier.</param>
        /// <param name="thirdGuid">The third unique identifier.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>Guid.</returns>
        public static Guid Combine(Guid firstGuid,
                                   Guid secondGuid,
                                   Guid thirdGuid,
                                   Byte offset)
        {
            Byte[] firstAsBytes = firstGuid.ToByteArray();
            Byte[] secondAsBytes = secondGuid.ToByteArray();
            Byte[] thirdAsBytes = thirdGuid.ToByteArray();

            Byte[] newBytes = new Byte[16];

            for (Int32 i = 0; i < 16; i++)
            {
                // Add and truncate any overflow
                newBytes[i] = (Byte)(firstAsBytes[i] + secondAsBytes[i] + thirdAsBytes[i] + offset);
            }

            return new Guid(newBytes);
        }

        /// <summary>
        /// Combines the specified first unique identifier.
        /// </summary>
        /// <param name="firstGuid">The first unique identifier.</param>
        /// <param name="secondGuid">The second unique identifier.</param>
        /// <param name="thirdGuid">The third unique identifier.</param>
        /// <returns>Guid.</returns>
        public static Guid Combine(Guid firstGuid,
                                   Guid secondGuid,
                                   Guid thirdGuid)
        {
            return GuidCalculator.Combine(firstGuid,
                                          secondGuid,
                                          thirdGuid,
                                          0);
        }

        #endregion
    }
}