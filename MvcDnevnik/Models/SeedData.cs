using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcDnevnik.Data;
using MvcDnevnik.Models;
using System;
using System.Linq;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcDnevnikContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcDnevnikContext>>()))
        {
            // Look for any movies.
            


            if (context.Grade.Any())
            {
                return;   // DB has been seeded
            }
            context.Grade.AddRange(
                new Grade
                {
                    Value = 5,
                    Date = DateTime.Parse("2023-09-01"),
                    Description = "Excellent performance",
                    Student = new Student { Name = "John Doe" },
                    Subject = new Subject { Name = "Mathematics" }
                }
               
            );
            context.SaveChanges();
        }
    }
}
