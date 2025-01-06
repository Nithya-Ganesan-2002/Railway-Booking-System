using System;
using System.ComponentModel.DataAnnotations;

namespace BookingManagementApp.Models
{
    public class TrainSchedule
    {
        [Key]
        public int TrainId { get; set; }

        [Required]
        [StringLength(100)]
        public string TrainName { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int AvailableSeats { get; set; }

        // Navigation property
        public virtual ICollection<Booking> Bookings { get; set; }

        public TrainSchedule()
        {
            Bookings = new HashSet<Booking>();
        }

        // PUBLIC_INTERFACE
        public IEnumerable<TrainSchedule> SearchTrains()
        {
            // Implementation will be added later
            return new List<TrainSchedule>();
        }

        // PUBLIC_INTERFACE
        public IEnumerable<TrainSchedule> FilterTrains()
        {
            // Implementation will be added later
            return new List<TrainSchedule>();
        }

        // PUBLIC_INTERFACE
        public TrainSchedule GetTrainDetails()
        {
            // Implementation will be added later
            return this;
        }
    }
}