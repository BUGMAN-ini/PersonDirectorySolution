namespace PersonDirectory.Infrastructure.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.PersonalNumber)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength();

            builder.HasIndex(p => p.PersonalNumber)
                .IsUnique();

            builder.Property(p => p.DateOfBirth)
                .IsRequired();

            builder.HasOne(p => p.City)
                .WithMany(c => c.Persons)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.PhoneNumbers).WithOne()
                .HasForeignKey(pn => pn.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.RelatedPersons)
                .WithOne(rp => rp.Person)
                .HasForeignKey(rp => rp.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.RelatedToPersons)
                .WithOne(rp => rp.RelatedToPerson)
                .HasForeignKey(rp => rp.RelatedToPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.PhoneNumbers)
                   .WithOne(pn => pn.Person)
                   .HasForeignKey(pn => pn.PersonId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.RelatedPersons)
                   .WithOne(rp => rp.Person)
                   .HasForeignKey(rp => rp.PersonId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
