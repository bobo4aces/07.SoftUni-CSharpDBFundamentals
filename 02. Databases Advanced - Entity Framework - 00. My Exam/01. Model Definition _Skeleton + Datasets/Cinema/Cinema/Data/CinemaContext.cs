namespace Cinema.Data
{
    using Cinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CinemaContext : DbContext
    {
        public CinemaContext()  { }

        public CinemaContext(DbContextOptions options)
            : base(options)   { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasKey(t => new { t.CustomerId, t.ProjectionId });
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Tickets)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId);
            modelBuilder.Entity<Projection>()
                .HasMany(p => p.Tickets)
                .WithOne(p => p.Projection)
                .HasForeignKey(p=>p.ProjectionId);
        }
    }
}