using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nextium.Models
{
    public class Fundraiser
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Goal { get; set; }
        public decimal AmountRaised { get; set; }
    }
}
