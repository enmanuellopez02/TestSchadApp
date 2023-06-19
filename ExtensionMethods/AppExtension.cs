using System;
using Microsoft.EntityFrameworkCore;
using TestSchadApp.Models;
using TestSchadApp.Persistances;

namespace TestSchadApp.ExtensionMethods
{
	public static class AppExtension
	{
        public static void MigrateDatabase(this IApplicationBuilder application)
        {
            using (var scope = application.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                if (context.CustomerTypes.Any())
                {
                    return;
                }

                context.CustomerTypes.AddRange(
                    new CustomerType { Description = "Cliente amigable" },
                    new CustomerType { Description = "Cliente detallista" },
                    new CustomerType { Description = "Cliente conversador" }
                );
            }
        }
    }
}