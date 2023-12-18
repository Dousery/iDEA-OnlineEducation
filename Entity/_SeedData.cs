using Microsoft.EntityFrameworkCore;

namespace iDEA.Entity {
    public static class SeedData {
        public static void InitTestValues(IApplicationBuilder app) {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<DataContext>();

            if (context == null) return;

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }

            if (context.Assignments.Any()) {
                
            }
        }
    }
}
