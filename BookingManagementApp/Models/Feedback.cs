using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        [Required]
        public DateTime FeedbackDate { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public virtual UserAccount User { get; set; }

        // PUBLIC_INTERFACE
        public bool SubmitFeedback()
        {
            // Implementation will be added later
            return false;
        }

        // PUBLIC_INTERFACE
        public Feedback GetFeedback()
        {
            // Implementation will be added later
            return this;
        }
    }
}