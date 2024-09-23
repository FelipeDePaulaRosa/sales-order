using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace UnitTest.TestHelpers.InMemoryDatabaseHelpers;

public static class OrderDbContextInMemoryFactory
{
    public static OrderDbContext Create()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>()
            .UseInMemoryDatabase($"InMemoryDb{Guid.NewGuid()}");

        return new OrderDbContext(optionsBuilder.Options);
    }
}