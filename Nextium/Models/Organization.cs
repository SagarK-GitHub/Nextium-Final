using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nextium.Models
{
    public class Organization
    {
        [Key]
        public int organization_Id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }
        public int no_of_requirements { get; set; }

        public List<Requirements> Requirements { get; set; }

    }
}
