using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MIS.Models.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.QuestionText).IsRequired();
            builder.Property(q => q.CorrectAnswerIndex).IsRequired();
            // No need to ignore the Options property anymore
        }
    }
}
