using chat_server.Model;
using Microsoft.EntityFrameworkCore;

namespace chat_server.Contexts
{   
    public class ChatContext : DbContext
    {
        public DbSet<Login> Logins { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Contact> Contacts {get;set;}
        public DbSet<ChatRoomMember> ChatRoomMembers {get;set;}

        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {

        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(p => p.Contacts)
            .WithOne(c => c.Owner);

            modelBuilder.Entity<Contact>()
            .HasOne(p => p.Owner)
            .WithMany(p => p.Contacts);

           modelBuilder.Entity<ChatRoom>()
            .HasMany(p => p.Members)
            .WithOne(c => c.ChatRoom);

            modelBuilder.Entity<ChatRoomMember>()
            .HasOne(p => p.User)
            .WithMany(c => c.MemberOf);
        }    
    }
}