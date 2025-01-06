using Microsoft.EntityFrameworkCore;
using BookingManagementApp.Data;

namespace BookingManagementApp.Tests.Helpers
{
    public static class TestDatabaseHelper
    {
        public static ApplicationDbContext CreateTestDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        public static void ClearDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
        }
    }
}