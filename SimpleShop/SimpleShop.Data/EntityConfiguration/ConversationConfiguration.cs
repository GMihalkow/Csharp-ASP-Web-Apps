using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleShop.Data.Models;

namespace SimpleShop.Data.EntityConfiguration
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasOne(c => c.Author)
                .WithMany(u => u.StartedConversations)
                .HasForeignKey(c => c.AuthorId);
            
            builder
                .HasOne(c => c.Reciever)
                .WithMany(u => u.RecievedConversations)
                .HasForeignKey(c => c.RecieverId);
        }
    }
}