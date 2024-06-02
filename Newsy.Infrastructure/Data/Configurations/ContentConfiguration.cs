using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Newsy.Domain.Entities;

namespace Newsy.Infrastructure.Data.Configurations;

internal class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.HasOne(x => x.Source)
               .WithMany(x => x.Contents)
               .HasForeignKey(x => x.SourceId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
    }
}

internal class RssContentConfiguration : IEntityTypeConfiguration<RssContent>
{
    public void Configure(EntityTypeBuilder<RssContent> builder)
    {
        builder.HasBaseType<Content>();
    }
}
