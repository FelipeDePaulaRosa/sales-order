using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database;

public class ConnectionDatabaseSettings
{
    public string DefaultConnection { get; set; } = null!;

    public static ConnectionDatabaseSettings GetInstance(IConfiguration configuration)
    {
        var connectionSettings = new ConnectionDatabaseSettings();

        configuration
            .GetSection("ConnectionStrings")
            .Bind(connectionSettings);

        return connectionSettings;
    }
}