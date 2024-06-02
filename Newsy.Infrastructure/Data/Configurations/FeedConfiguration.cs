using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newsy.Domain.Entities;

namespace Newsy.Infrastructure.Data.Configurations;
internal class FeedConfiguration : IEntityTypeConfiguration<Feed>
{
    public void Configure(EntityTypeBuilder<Feed> builder)
    {
        builder.HasMany(x => x.Rules)
               .WithOne(x => x.Feed)
               .HasForeignKey(x => x.FeedId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

internal class FeedRuleConfiguration : IEntityTypeConfiguration<FeedRule>
{
    public void Configure(EntityTypeBuilder<FeedRule> builder)
    {
        builder.ToTable("FeedRules");
    }
}

internal class IncludeTagRuleConfiguration : IEntityTypeConfiguration<IncludeTagRule>
{
    public void Configure(EntityTypeBuilder<IncludeTagRule> builder)
    {
        builder.HasBaseType<FeedRule>();
        builder.HasOne(x => x.Tag).WithMany().HasForeignKey(x => x.TagId).IsRequired().IsRequired(false).OnDelete(DeleteBehavior.Cascade);
    }
}

internal class ExcludeTagRuleConfiguration : IEntityTypeConfiguration<ExcludeTagRule>
{
    public void Configure(EntityTypeBuilder<ExcludeTagRule> builder)
    {
        builder.HasBaseType<FeedRule>();
        builder.HasOne(x => x.Tag).WithMany().HasForeignKey(x => x.TagId).IsRequired().IsRequired(false).OnDelete(DeleteBehavior.Cascade);
    }
}
