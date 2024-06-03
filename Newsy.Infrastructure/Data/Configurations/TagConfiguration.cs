using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newsy.Domain.Entities;

namespace Newsy.Infrastructure.Data.Configurations;
internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasMany(x => x.Contents).WithMany(x => x.Tags).UsingEntity("ContentTags");
    }
}

internal class SourceTagConfiguration : IEntityTypeConfiguration<SourceTag>
{
    public void Configure(EntityTypeBuilder<SourceTag> builder)
    {
        builder.HasBaseType<Tag>();
        builder.HasOne(x => x.Source).WithOne().HasForeignKey<SourceTag>(x => x.SourceId);
    }
}

internal class ContainsTagConfiguration : IEntityTypeConfiguration<ContainsTag>
{
    public void Configure(EntityTypeBuilder<ContainsTag> builder)
    {
        builder.HasBaseType<Tag>();
    }
}
