using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public double Amount { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; }

        // Navigation property
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }

        // PUBLIC_INTERFACE
        public bool ProcessPayment()
        {
            // Implementation will be added later
            return false;
        }

        // PUBLIC_INTERFACE
        public bool RefundPayment()
        {
            // Implementation will be added later
            return false;
        }
    }
}