﻿namespace EstateReporting.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 
    /// </summary>
    [Table("voucher")]
    public class Voucher
    {
        #region Properties

        /// <summary>
        /// Gets or sets the estate identifier.
        /// </summary>
        /// <value>
        /// The estate identifier.
        /// </value>
        public Guid EstateId { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is generated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is generated; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsGenerated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is issued.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is issued; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsIssued { get; set; }

        /// <summary>
        /// Gets or sets the operator identifier.
        /// </summary>
        /// <value>
        /// The operator identifier.
        /// </value>
        public String OperatorIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the recipient email.
        /// </summary>
        /// <value>
        /// The recipient email.
        /// </value>
        public String RecipientEmail { get; set; }

        /// <summary>
        /// Gets or sets the recipient mobile.
        /// </summary>
        /// <value>
        /// The recipient mobile.
        /// </value>
        public String RecipientMobile { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public Decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the voucher code.
        /// </summary>
        /// <value>
        /// The voucher code.
        /// </value>
        public String VoucherCode { get; set; }

        /// <summary>
        /// Gets or sets the voucher identifier.
        /// </summary>
        /// <value>
        /// The voucher identifier.
        /// </value>
        [Key]
        public Guid VoucherId { get; set; }

        #endregion
    }
}