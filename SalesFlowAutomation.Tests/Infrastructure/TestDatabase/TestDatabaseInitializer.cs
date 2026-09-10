
using Microsoft.EntityFrameworkCore;

namespace SalesFlowAutomation.Tests.Infrastructure.TestDatabase
{
    public static class TestDatabaseInitializer
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        private static bool _initialized = false;
        public static async Task InitializeAsync()
        {
            if (_initialized)
                return;

            await _semaphore.WaitAsync();

            try
            {
                if (_initialized)
                    return;

                await using var context = TestDbContextFactory.Create();
                await context.Database.MigrateAsync();

                _initialized = true;
            }
            finally
            {
                _semaphore.Release();
            }

        }
    }
}
