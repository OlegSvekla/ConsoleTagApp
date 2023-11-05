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
    public class TagToUserConfiguration : IEntityTypeConfiguration<TagToUser>
    {
        public void Configure(EntityTypeBuilder<TagToUser> builder)
        {
            builder.HasKey(tu => tu.Id);

            builder.HasOne(tu => tu.User)
                .WithMany(u => u.TagsToUser)
                .HasForeignKey(tu => tu.UserId);

            builder.HasOne(tu => tu.Tags)
                .WithMany()
                .HasForeignKey(tu => tu.TagId);
        }
    }
}
