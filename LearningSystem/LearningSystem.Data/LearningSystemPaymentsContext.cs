namespace LearningSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class LearningSystemPaymentsContext : DbContext
    {
        public LearningSystemPaymentsContext(DbContextOptions<LearningSystemPaymentsContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }
    }
}