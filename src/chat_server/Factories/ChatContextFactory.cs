using chat_server.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySQL.Data.Entity.Extensions;

namespace chat_server.Factories
{
    public static class ChatContextFactory
    {
        private static ChatContext context;
        public static ChatContext Get()
        {
            if (ChatContextFactory.context == null)
            {             
                var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                
                var configuration = builder.Build();

                var optionsBuilder = new DbContextOptionsBuilder<ChatContext>();
                optionsBuilder.UseMySQL(configuration.GetConnectionString("DbConnection"));
                
                //Ensure database creation
                var context = new ChatContext(optionsBuilder.Options);
                context.Database.EnsureCreated();
                ChatContextFactory.context = context;
            }

            return ChatContextFactory.context;
        }
    }
}