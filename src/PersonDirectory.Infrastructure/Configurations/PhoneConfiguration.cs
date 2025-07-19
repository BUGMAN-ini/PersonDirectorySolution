using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Infrastructure.Configurations
{
    public class PhoneConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(p => p.Type)
                .IsRequired();

            builder.HasOne(p => p.Person)                 
                .WithMany(p => p.PhoneNumbers)            
                .HasForeignKey(p => p.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
