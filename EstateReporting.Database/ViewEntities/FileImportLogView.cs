using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateReporting.Database.ViewEntities
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 
    /// </summary>
    [Table("uvwFileImportLogView")]
    public class FileImportLogView
    {
        /// <summary>
        /// Gets or sets the file import log identifier.
        /// </summary>
        /// <value>
        /// The file import log identifier.
        /// </value>
        public Guid FileImportLogId { get; set; }

        /// <summary>
        /// Gets or sets the import log date time.
        /// </summary>
        /// <value>
        /// The import log date time.
        /// </value>
        public DateTime ImportLogDateTime { get; set; }

        /// <summary>
        /// Gets or sets the import log date.
        /// </summary>
        /// <value>
        /// The import log date.
        /// </value>
        public DateTime ImportLogDate { get; set; }

        /// <summary>
        /// Gets or sets the import log time.
        /// </summary>
        /// <value>
        /// The import log time.
        /// </value>
        public TimeSpan ImportLogTime { get; set; }

        /// <summary>
        /// Gets or sets the file count.
        /// </summary>
        /// <value>
        /// The file count.
        /// </value>
        public Int32 FileCount { get; set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier.
        /// </value>
        public Guid MerchantId { get; set; }
    }
}
