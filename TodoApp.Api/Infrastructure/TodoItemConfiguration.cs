using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Api.Domain;

namespace TodoApp.Api.Infrastructure;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.Title)
            .HasMaxLength(TodoItem.MaxTitleLength);
        builder.Property(x => x.Author)
            .HasMaxLength(TodoItem.MaxAuthorLength);
        builder.Property(x => x.Url)
            .HasMaxLength(TodoItem.MaxUrlLength);
    }
}
