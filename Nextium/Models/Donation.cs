using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nextium.Models
{
    public class Donation
    {
        public int DonationId { get; set; }

        public int DonorId { get; set; }

        [ForeignKey("Organization")]
        public int organization_Id { get; set; }

        [ForeignKey("Requirements")]
        public int requirement_Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime DonationDate { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string PaymentMethod { get; set; }
       
        [MaxLength(50)]
        public string RequirementName { get; set; }
        [Required]
        public int UnitsDonated { get; set; }

        [MaxLength(100)]
        public string DonationType { get; set; }

        [MaxLength(100)]
        public string TransactionId { get; set; }

        [MaxLength(100)]
        public string PayeeId { get; set; }

        public byte[] DonatedImage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Donor Donor { get; set; }
    }
}
