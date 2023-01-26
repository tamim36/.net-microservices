using PlatformService.Models;

namespace PlatformService.Data
{
    public static class DummyData
    {
        public static void DataPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext dbContext)
        {
            if (!dbContext.Platforms.Any())
            {
                Console.WriteLine("==> Seeding Data ...");

                dbContext.AddRange(
                        new Platform() { Name="DotNet", Publisher="MicroSoft", Cost="Free"},
                        new Platform() { Name="Python", Publisher="Tesla", Cost="500$"},
                        new Platform() { Name="Java", Publisher="Oracle", Cost="Free"},
                        new Platform() { Name="Visualt Studio", Publisher="MicroSoft", Cost="1000$"}
                    );

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("==> We already have data !");
            }
        }
    }
}
