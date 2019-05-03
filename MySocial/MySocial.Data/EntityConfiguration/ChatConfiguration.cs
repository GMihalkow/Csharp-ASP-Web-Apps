using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySocial.Data.Models;

namespace MySocial.Data.EntityConfiguration
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(ch => ch.Id);

            builder
                .HasOne(ch => ch.Author)
                .WithMany(a => a.Chats)
                .HasForeignKey(ch => ch.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}