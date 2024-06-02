using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newsy.Domain.Entities;
using Newsy.Domain.ValueObjects;

namespace Newsy.Infrastructure.Data.Configurations;

internal class NrSourceConfiguration : IEntityTypeConfiguration<Source>
{
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.HasOne(x => x.SourceFolder).WithMany(x => x.Sources).HasForeignKey(x => x.SourceFolderId).IsRequired(false);
    }
}

internal class RssNewsSourceConfiguration : IEntityTypeConfiguration<RssSource>
{
    public void Configure(EntityTypeBuilder<RssSource> builder)
    {
        builder.HasBaseType<Source>();
        builder.Property(x => x.RssUrl).HasConversion(
            url => url.Value,
            value => RssUrl.Create(value)!);
    }
}

internal class SourceFolderConfiguration : IEntityTypeConfiguration<SourceFolder>
{
    public void Configure(EntityTypeBuilder<SourceFolder> builder)
    {
        builder.HasOne(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId).IsRequired(false);
    }
}
