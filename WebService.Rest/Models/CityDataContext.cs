using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebService.Rest.Models.Mapping;

namespace WebService.Rest.Models
{
    public partial class CityDataContext : DbContext
    {
        static CityDataContext()
        {
            Database.SetInitializer<CityDataContext>(null);
        }

        public CityDataContext()
            : base("Name=CityDataContext")
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new ProvinceMap());
        }
    }
}
