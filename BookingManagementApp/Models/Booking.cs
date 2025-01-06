using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int TrainId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SeatNumber { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual UserAccount User { get; set; }

        [ForeignKey("TrainId")]
        public virtual TrainSchedule Train { get; set; }

        public virtual Payment Payment { get; set; }

        // PUBLIC_INTERFACE
        public Booking CreateBooking()
        {
            // Implementation will be added later
            return this;
        }

        // PUBLIC_INTERFACE
        public bool CancelBooking()
        {
            // Implementation will be added later
            return false;
        }

        // PUBLIC_INTERFACE
        public Booking GetBookingDetails()
        {
            // Implementation will be added later
            return this;
        }
    }
}