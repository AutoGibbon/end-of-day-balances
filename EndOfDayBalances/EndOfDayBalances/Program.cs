using EndOfDayBalances.Data.Contexts;
using EndOfDayBalances.Domain;
using EndOfDayBalances.Middleware;

namespace EndOfDayBalances
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<HttpResponseExceptionFilter>();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //added as a singleton due to the basic nature of the data store
            builder.Services.AddSingleton<IAccountsContext, AccountsContext>();
            builder.Services.AddTransient<IEndOfDayBalancesCalculator, EndOfDayBalancesCalculator>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}