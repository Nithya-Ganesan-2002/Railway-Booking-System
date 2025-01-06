using System;
using System.ComponentModel.DataAnnotations;

namespace BookingManagementApp.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        // Navigation properties
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public UserAccount()
        {
            Bookings = new HashSet<Booking>();
            Notifications = new HashSet<Notification>();
            Feedbacks = new HashSet<Feedback>();
        }

        // PUBLIC_INTERFACE
        public void Register()
        {
            // Implementation will be added later
        }

        // PUBLIC_INTERFACE
        public bool Login()
        {
            // Implementation will be added later
            return false;
        }

        // PUBLIC_INTERFACE
        public void Logout()
        {
            // Implementation will be added later
        }

        // PUBLIC_INTERFACE
        public void UpdateProfile()
        {
            // Implementation will be added later
        }
    }
}