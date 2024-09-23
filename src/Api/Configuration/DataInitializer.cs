using Domain.Products;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api.Configuration;

public static class DataInitializer
{
    public static void InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var orderDbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
        orderDbContext.Database.Migrate();
        InjectProductsSeed(orderDbContext);
    }

    private static void InjectProductsSeed(OrderDbContext orderDbContext)
    {
        if(orderDbContext.Products.Any()) return;
        
        var products = new List<Product>
        {
            new(Guid.Parse("d8d26491-a874-43d9-b8de-bdc241fc805d"), "001", "Hammer", "ToolBrand", 1500, 2, 50, true),
            new(Guid.Parse("b5d35aa0-8b8c-4ff8-a595-7b27942b305c"), "002", "Screwdriver Set", "ToolBrand", 2500, 5, 100, true),
            new(Guid.Parse("ddc70788-4c49-4e88-9d6e-a6ca32c14121"), "003", "Wrench", "ToolBrand", 1200, 3, 30, true),
            new(Guid.Parse("ab900400-8297-4e0d-b299-8e349f5b0e28"), "004", "Pliers", "ToolBrand", 800, 1, 20, true),
            new(Guid.Parse("4b751120-53ff-4325-aba0-5ea259ac16ce"), "005", "Drill", "ToolBrand", 5000, 4, 200, true),
            new(Guid.Parse("f651510c-16c0-4813-8489-02ab06bf43fd"), "006", "Saw", "ToolBrand", 3000, 6, 150, true),
            new(Guid.Parse("c63f9a1d-d775-4f42-ad94-08a4fb0d59f8"), "007", "Tape Measure", "ToolBrand", 500, 0, 10,
                true),
            new(Guid.Parse("c240e575-b5c6-4b1c-b6b6-a47ba7f4e7a3"), "008", "Level", "ToolBrand", 700, 2, 15, true),
            new(Guid.Parse("a75f03fd-0c4a-4fb7-9cb9-8390bc230120"), "009", "Chisel", "ToolBrand", 600, 1, 12, true),
            new(Guid.Parse("df476968-c6fc-4b42-9ef6-4240a8bf8719"), "010", "Utility Knife", "ToolBrand", 400, 0, 8,
                true),
            new(Guid.Parse("5a628883-4f33-40f2-829e-1d9e90503477"), "011", "Sander", "ToolBrand", 4500, 3, 180, true),
            new(Guid.Parse("d7ed62e5-d263-4c57-b8c1-4cd89376660c"), "012", "Angle Grinder", "ToolBrand", 5500, 4, 220,
                true),
            new(Guid.Parse("18378472-0858-4e1a-a9f8-544ebae08051"), "013", "Circular Saw", "ToolBrand", 6000, 5, 240,
                true),
            new(Guid.Parse("85e487a8-0092-4e95-923a-d3bc4c25e52b"), "014", "Jigsaw", "ToolBrand", 3500, 2, 140, true),
            new(Guid.Parse("0b11173d-b106-4fe0-ad5e-951bd2f712ee"), "015", "Router", "ToolBrand", 4800, 3, 192, true),
            new(Guid.Parse("4d3930f7-ae8e-4676-8639-2afe42023185"), "016", "Planer", "ToolBrand", 5200, 4, 208, true),
            new(Guid.Parse("3a24d36d-67c1-420a-8331-78e09f0ec15c"), "017", "Heat Gun", "ToolBrand", 2500, 1, 100, true),
            new(Guid.Parse("4fd8e18b-397b-456e-9b2c-f5b31244d46c"), "018", "Glue Gun", "ToolBrand", 1500, 0, 60, true),
            new(Guid.Parse("5e45946c-ca50-48b9-b540-b44824b825e8"), "019", "Staple Gun", "ToolBrand", 2000, 2, 80,
                true),
            new(Guid.Parse("7a1c2526-8cc8-47f0-9754-a2c11a158743"), "020", "Paint Sprayer", "ToolBrand", 7000, 5, 280,
                true),
            new(Guid.Parse("398fa1b3-49bd-4077-8332-d152bf677029"), "021", "Air Compressor", "ToolBrand", 10000, 6, 400,
                true),
            new(Guid.Parse("af2cb0af-6ed9-43db-8e1f-0980cb840c15"), "022", "Impact Wrench", "ToolBrand", 6500, 4, 260,
                true),
            new(Guid.Parse("0eb3fc60-fbab-475f-ab19-70f8a5b30d57"), "023", "Socket Set", "ToolBrand", 3000, 3, 120,
                true),
            new(Guid.Parse("6ad96ae0-e00f-428d-90f8-7b7eded33a23"), "024", "Torque Wrench", "ToolBrand", 3500, 2, 140,
                true),
            new(Guid.Parse("0d17e21f-6396-4e7d-abf6-4d61ac1460ba"), "025", "Pipe Wrench", "ToolBrand", 1800, 1, 72,
                true),
            new(Guid.Parse("00fe3cdf-28ec-4c80-8f31-5ff4d1b82ac1"), "026", "Allen Wrench Set", "ToolBrand", 1200, 0, 48,
                true),
            new(Guid.Parse("aebb34e3-d8cf-4aaf-b4b2-e39ec71a0b53"), "027", "Hacksaw", "ToolBrand", 900, 1, 36, true),
            new(Guid.Parse("21057c0d-1d4f-440d-8e02-a03a0bd93a2d"), "028", "Bolt Cutter", "ToolBrand", 2500, 2, 100,
                true),
            new(Guid.Parse("3740bcfd-acf5-4397-a664-5db53cb19a22"), "029", "Wire Stripper", "ToolBrand", 800, 0, 32,
                true),
            new(Guid.Parse("703905ef-4ade-4926-9424-2c42a3f76dfc"), "030", "Crimping Tool", "ToolBrand", 1500, 1, 60,
                true),
            new(Guid.Parse("45b19a16-0051-4fc8-b40b-384b5dba64c0"), "031", "Multimeter", "ToolBrand", 3000, 3, 120,
                true),
            new(Guid.Parse("671a5d61-6921-44d2-bed2-bac76d57c03f"), "032", "Clamp Meter", "ToolBrand", 3500, 2, 140,
                true),
            new(Guid.Parse("6aa7a574-9fe4-4372-8d74-a92d63387f74"), "033", "Oscilloscope", "ToolBrand", 15000, 5, 600,
                true),
            new(Guid.Parse("275e3654-8b78-487b-aa74-36ffd6d6b274"), "034", "Soldering Iron", "ToolBrand", 2000, 1, 80,
                true),
            new(Guid.Parse("c04c1039-6a31-45c2-bcb0-f9041fbdf53c"), "035", "Heat Shrink Gun", "ToolBrand", 2500, 2, 100,
                true),
            new(Guid.Parse("2dd4a54e-b160-4b06-b655-696237e5cee4"), "036", "Voltage Tester", "ToolBrand", 1000, 0, 40,
                true),
            new(Guid.Parse("cdd0efd1-07a1-4caa-9888-34c689b7d4dc"), "037", "Stud Finder", "ToolBrand", 1500, 1, 60,
                true),
            new(Guid.Parse("055c3067-4311-40a0-bf9e-52268de2b158"), "038", "Laser Level", "ToolBrand", 5000, 3, 200,
                true),
            new(Guid.Parse("3c0736ce-3b55-4d44-919e-40945751bc0a"), "039", "Moisture Meter", "ToolBrand", 3000, 2, 120,
                true),
            new(Guid.Parse("f516f3d0-10fa-4569-bc6a-3710f171153c"), "040", "Inspection Camera", "ToolBrand", 7000, 4,
                280, true),
            new(Guid.Parse("e8bf6b45-59c5-4cfd-87d2-f962231a6636"), "041", "Thermal Imager", "ToolBrand", 15000, 5, 600,
                true),
            new(Guid.Parse("0d06cedc-8559-43ed-98c9-f4db11973081"), "042", "Digital Caliper", "ToolBrand", 2500, 2, 100,
                true),
            new(Guid.Parse("5a15916d-20a3-4874-b379-c4995a65ed4d"), "043", "Micrometer", "ToolBrand", 3000, 3, 120,
                true),
            new(Guid.Parse("11b355d4-5e63-40ac-b928-56d013739f87"), "044", "Depth Gauge", "ToolBrand", 2000, 1, 80,
                true),
            new(Guid.Parse("5b9fe7c1-a06d-420e-aad0-4baf29a489b7"), "045", "Feeler Gauge", "ToolBrand", 1000, 0, 40,
                false),
            new(Guid.Parse("80468545-e6ff-4f23-98f0-15922a24ea92"), "046", "Dial Indicator", "ToolBrand", 3500, 2, 140,
                false),
            new(Guid.Parse("f39949e3-2f44-49f5-808f-d352c392388e"), "047", "Torque Screwdriver", "ToolBrand", 4000, 3,
                160, false),
            new(Guid.Parse("7ee3e130-a095-46f3-afd7-3512cad7bd49"), "048", "Nut Driver Set", "ToolBrand", 2000, 1, 80,
                false),
            new(Guid.Parse("e1f43cdb-a852-40b1-9a43-b9b2da9fd276"), "049", "Hex Key Set", "ToolBrand", 1200, 0, 48,
                false),
            new(Guid.Parse("678dfbed-1313-4d86-b92b-3ed33907ff18"), "050", "Bit Set", "ToolBrand", 1500, 1, 60, false)
        };

        orderDbContext.Products.AddRange(products);
        orderDbContext.SaveChanges();
    }
}