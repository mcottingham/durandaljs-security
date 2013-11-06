using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DurandalSecurity.Models {
    public class SecurityDbContext : DbContext {
        public SecurityDbContext()
            : base("name=SecurityDb") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}