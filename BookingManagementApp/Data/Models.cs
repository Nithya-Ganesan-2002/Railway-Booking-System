using System.ComponentModel.DataAnnotations;

namespace BookingManagementApp.Data
{
    public class UserAccount
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }

    public class TrainSchedule
    {
        public int TrainId { get; set; }
        public string TrainName { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AvailableSeats { get; set; }
    }

    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int TrainId { get; set; }
        public int SeatNumber { get; set; }
        public DateTime BookingDate { get; set; }

        // Navigation properties
        public UserAccount UserAccount { get; set; } = null!;
        public TrainSchedule TrainSchedule { get; set; } = null!;
        public Payment? Payment { get; set; }
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;

        // Navigation property
        public Booking Booking { get; set; } = null!;
    }

    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime NotificationDate { get; set; }

        // Navigation property
        public UserAccount UserAccount { get; set; } = null!;
    }

    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime FeedbackDate { get; set; }

        // Navigation property
        public UserAccount UserAccount { get; set; } = null!;
    }
}