using Microsoft.EntityFrameworkCore;

namespace Payment.Web.Infrastructure.Database;

internal class PaymentDbContext : DbContext
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    private readonly IConfiguration _configuration;

    public PaymentDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PaymentWebDb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Transaction>()
            .HasOne(e => e.Card)
            .WithMany(e => e.Transactions)
            .HasForeignKey(e => e.CardId)
            .IsRequired();
        base.OnModelCreating(modelBuilder);
    }
}

public class Card
{
    public Guid Id { get; set; }
    public ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}

public class Transaction
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public Card Card { get; set; } = null!;
    public int Amount { get; set; }
}