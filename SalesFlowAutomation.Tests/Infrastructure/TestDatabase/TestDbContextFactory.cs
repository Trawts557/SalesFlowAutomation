
using Microsoft.EntityFrameworkCore;
using SalesFlowAutomation.Infrastructure.Persistence;

namespace SalesFlowAutomation.Tests.Infrastructure.TestDatabase
{
    public static class TestDbContextFactory
    {
        // Conexion a la BD de prueba
        private const string ConnectionString =
            "Server=localhost\\SQLEXPRESS;" +
            "Database=SalesFlowAutomationTestDb;" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True;";
        
        // Crear AppDbContext usando la bd de pruebas
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new AppDbContext(options);
        }
    }
}
