namespace PersonDirectory.Infrastructure.Configurations
{
    public class RelatedPersonconfiguration : IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<RelatedPerson> builder)
        {
            builder.HasOne(rp => rp.Person)
                .WithMany(p => p.RelatedPersons)
                .HasForeignKey(rp => rp.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.RelatedToPerson)
                .WithMany(p => p.RelatedToPersons)
                .HasForeignKey(r => r.RelatedToPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.RelationType)
                .IsRequired();
        }
    }
}
