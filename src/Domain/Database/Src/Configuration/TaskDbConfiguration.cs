using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Objects.Dto;

namespace Database.Configuration
{
    internal class TaskDbConfiguration : IEntityTypeConfiguration<CodeTaskDto>
    {
        public void Configure(EntityTypeBuilder<CodeTaskDto> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Title).IsRequired().HasColumnName("title");
            builder.Property(t => t.Description).IsRequired().HasColumnName("description");
            builder.Property(t => t.MethodName).IsRequired().HasColumnName("method");
            builder.Property(t => t.NamespaceName).IsRequired().HasColumnName("namespace");

            builder.Property(t => t.CreatedUtc).IsRequired().HasColumnName("created_utc");
            builder.Property(t => t.UpdatedUtc).IsRequired().HasColumnName("updated_utc");
        }
    }
}
