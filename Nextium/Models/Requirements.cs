using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nextium.Models
{
    public class Requirements
    {
        [Key]
        public int requirement_Id { get; set; }
        public int organization_Id { get; set; }
        public string requirement { get; set; }
        public int units_needed { get; set; }

        public decimal Amount { get; set; }

        [ForeignKey("organization_Id")]
        public Organization Organization { get; set; }

    }
}
