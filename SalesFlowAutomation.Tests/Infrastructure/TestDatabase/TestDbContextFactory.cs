
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesFlowAutomation.Infrastructure.Persistence;

namespace SalesFlowAutomation.Tests.Infrastructure.TestDatabase
{
    // Crear AppDbContext usando la bd de pruebas
    public static class TestDbContextFactory
    {
        private static readonly IConfiguration _configuration;

        // Con este constructor estatico nos ahorramos tener que pasar la configuracion por parametro a nuestro metodo create
        static TestDbContextFactory()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
        }
        
        public static AppDbContext Create()
        {
            var connectionString = _configuration.GetConnectionString("TestConnection") ?? throw new InvalidOperationException("TestConnection is not configured");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new AppDbContext(options);
        }
    }
}
