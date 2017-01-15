using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using chat_server.Repositories;
using chat_server.Repositories.Interfaces;

namespace chat_server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageRepository, MessageRepository>();
            services.AddSingleton<ILoginRepository, LoginRepository>();
            services.AddSingleton<IChatRoomRepository, ChatRoomRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSignalR(options =>
            {
                options.Hubs.EnableDetailedErrors = true;
                options.EnableJSONP = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.UseSignalR();           

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Not for http use!");
            });
        }
    }
}
