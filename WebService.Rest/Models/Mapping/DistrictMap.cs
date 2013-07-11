using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebService.Rest.Models.Mapping
{
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            // Primary Key
            this.HasKey(t => t.DistrictId);

            // Properties
            this.Property(t => t.DisName)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Districts");
            this.Property(t => t.DistrictId).HasColumnName("DistrictID");
            this.Property(t => t.DisName).HasColumnName("DisName");
            this.Property(t => t.CityId).HasColumnName("CityID");
            this.Property(t => t.DisSort).HasColumnName("DisSort");
        }
    }
}
