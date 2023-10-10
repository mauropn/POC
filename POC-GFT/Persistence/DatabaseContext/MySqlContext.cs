using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseContext
{
    public class MySqlContext:DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MySqlContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Orders { get ; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added
                || q.State == EntityState.Modified
                || q.State == EntityState.Deleted
                ))
            {
                entry.Entity.DataCadastro = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DataCadastro = DateTime.Now;
                }

                if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.DataExclusao = DateTime.Now;
                    entry.Entity.Ativo = 0;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
