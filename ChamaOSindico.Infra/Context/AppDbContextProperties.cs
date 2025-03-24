using ChamaOSindico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChamaOSindico.Infra.Context
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
