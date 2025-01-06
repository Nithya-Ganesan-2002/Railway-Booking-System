using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        [Required]
        public DateTime NotificationDate { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public virtual UserAccount User { get; set; }

        // PUBLIC_INTERFACE
        public bool SendNotification()
        {
            // Implementation will be added later
            return false;
        }

        // PUBLIC_INTERFACE
        public IEnumerable<Notification> GetNotifications()
        {
            // Implementation will be added later
            return new List<Notification>();
        }
    }
}