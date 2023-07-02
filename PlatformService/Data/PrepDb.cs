namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
            var logger = serviceScope.ServiceProvider.GetService<ILogger<AppDbContext>>();

            if (appDbContext is null) throw new NotImplementedException(nameof(appDbContext));
            if (logger is null) throw new NotImplementedException(nameof(logger));
            SeedData(appDbContext, logger);
        }
        private static void SeedData(AppDbContext context, ILogger logger)
        {
            if (!context.Platforms.Any())
            {
                logger.LogInformation("--> Seeding data");
                context.Platforms.AddRange(
                    new Models.Platform() { Cost = "11$", Name = "Dotnet", Publisher = "Microsoft" },
                    new Models.Platform() { Cost = "100$", Name = "Node", Publisher = "Nodejs" },
                    new Models.Platform() { Cost = "Free", Name = "Arch", Publisher = "Linux" });
                context.SaveChanges();
                logger.LogInformation("--> Data seeded");
            }
            else
            {
                logger.LogWarning("--> We already have data");
            }
        }
    }
}