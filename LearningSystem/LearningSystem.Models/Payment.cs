namespace LearningSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public string Username { get; set; }

        public string PayPalPaymentId { get; set; }

        public int CourseId { get; set; }

        public decimal Amount { get; set; }
    }
}