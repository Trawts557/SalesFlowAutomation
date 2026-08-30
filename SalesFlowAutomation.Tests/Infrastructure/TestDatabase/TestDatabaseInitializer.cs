
using Microsoft.EntityFrameworkCore;

namespace SalesFlowAutomation.Tests.Infrastructure.TestDatabase
{
    public static class TestDatabaseInitializer
    {
        // Iniciar la BD de prueba con las tablas de la BD de desarrollo
        public static async Task InitializeAsync()
        {
            await using var context = TestDbContextFactory.Create();

            await context.Database.MigrateAsync();
        }
    }
}
