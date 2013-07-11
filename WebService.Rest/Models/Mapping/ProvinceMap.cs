using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebService.Rest.Models.Mapping
{
    public class ProvinceMap : EntityTypeConfiguration<Province>
    {
        public ProvinceMap()
        {
            // Primary Key
            this.HasKey(t => t.ProvinceId);

            // Properties
            this.Property(t => t.ProName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProRemark)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Provinces");
            this.Property(t => t.ProvinceId).HasColumnName("ProvinceID");
            this.Property(t => t.ProName).HasColumnName("ProName");
            this.Property(t => t.ProSort).HasColumnName("ProSort");
            this.Property(t => t.ProRemark).HasColumnName("ProRemark");
        }
    }
}
