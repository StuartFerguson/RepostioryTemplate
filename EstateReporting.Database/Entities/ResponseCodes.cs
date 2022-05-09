using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateReporting.Database.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("responsecodes")]
    public class ResponseCodes
    {
        public Int32 ResponseCode { get; set; }

        public String Description { get; set; }
    }
}
