using Xunit;
using BookingManagementApp.Tests.Helpers;

namespace BookingManagementApp.Tests.Unit
{
    public class SampleTest
    {
        [Fact]
        public void TestDatabaseHelper_CreateTestDatabase_ShouldCreateNewInstance()
        {
            // Arrange & Act
            var dbContext = TestDatabaseHelper.CreateTestDatabase();

            // Assert
            Assert.NotNull(dbContext);
            
            // Cleanup
            TestDatabaseHelper.ClearDatabase(dbContext);
        }
    }
}