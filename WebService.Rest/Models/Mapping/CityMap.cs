using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebService.Rest.Models.Mapping
{
    public class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            // Primary Key
            this.HasKey(t => t.CityId);

            // Properties
            this.Property(t => t.CityName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Cities");
            this.Property(t => t.CityId).HasColumnName("CityID");
            this.Property(t => t.CityName).HasColumnName("CityName");
            this.Property(t => t.ProvinceId).HasColumnName("ProvinceID");
            this.Property(t => t.CitySort).HasColumnName("CitySort");
        }
    }
}
