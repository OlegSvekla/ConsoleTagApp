using ConsoleTagApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTagApp.Infrastructure.EntitiesConfiguration
{
    public class TagToUserConfiguration : IEntityTypeConfiguration<UserTag>
    {
        public void Configure(EntityTypeBuilder<UserTag> builder)
        {
            builder
                .HasKey(ur => new { ur.UserId, ur.TagId });

            builder
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserTags)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(ur => ur.Tag)
                .WithMany(r => r.UserTags)
                .HasForeignKey(ur => ur.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
