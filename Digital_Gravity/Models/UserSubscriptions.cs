using System.ComponentModel.DataAnnotations;

namespace Digital_Gravity.Models
{
    public class UserSubscriptions
    {
        [Key]
        public int UserSubscriptionId { get; set; }  // Primary key
        public int UserId { get; set; }               // Foreign key to the User table
        public int SubscriptionId { get; set; }       // Foreign key to the Subscription table
        public DateTime CreatedAt { get; set; }       // Timestamp for when the subscription was created

        // Navigation properties
        public Users User { get; set; }                // Navigation to User
        public Subscription Subscription { get; set; } // Navigation to Subscription
    }
}
