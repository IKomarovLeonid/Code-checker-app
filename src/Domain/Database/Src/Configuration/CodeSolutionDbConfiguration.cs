using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Objects.Dto;

namespace Database.Configuration
{
    internal class CodeSolutionDbConfiguration : IEntityTypeConfiguration<CodeSolutionDto>
    {
        public void Configure(EntityTypeBuilder<CodeSolutionDto> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id");
            builder.Property(t => t.Code).IsRequired().HasColumnName("code");
            builder.Property(t => t.UserId).HasColumnName("user_id");
            builder.Property(t => t.TaskId).HasColumnName("task_id");
            builder.Property(t => t.Errors).HasColumnName("errors");
            builder.Property(t => t.TestsCount).HasColumnName("total_tests");
            builder.Property(t => t.PassedCount).HasColumnName("passed_tests");
            builder.Property(t => t.Status).IsRequired().HasColumnName("status").HasConversion(new EnumToStringConverter<SolutionStatus>());

            builder.Property(t => t.CreatedUtc).IsRequired().HasColumnName("created_utc");
            builder.Property(t => t.UpdatedUtc).IsRequired().HasColumnName("updated_utc");
        }
    }
}
