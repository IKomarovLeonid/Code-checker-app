using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Objects.Dto;

namespace Persistence.Configurations
{
    internal class TaskDbConfiguration : IEntityTypeConfiguration<CodeTaskDto>
    {
        public void Configure(EntityTypeBuilder<CodeTaskDto> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Title).IsRequired().HasColumnName("title");
            builder.Property(t => t.UserId).HasColumnName("userId");
            builder.Property(t => t.Description).IsRequired().HasColumnName("description");
            builder.Property(t => t.Code).IsRequired().HasColumnName("code");
            builder.Property(t => t.Title).IsRequired().HasColumnName("title");
            builder.Property(t => t.Status).IsRequired().HasColumnName("status").HasConversion(new EnumToStringConverter<TaskStatus>());
            
            builder.Property(t => t.CreatedUtc).IsRequired().HasColumnName("created_utc");
            builder.Property(t => t.UpdatedUtc).IsRequired().HasColumnName("updated_utc");
        }
    }
}
