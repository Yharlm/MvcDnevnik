using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcDnevnik.Data;
using MvcDnevnik.Models;
using System;
using System.Linq;
using System.Reflection.Metadata;

public static class SeedData
{

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcDnevnikContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcDnevnikContext>>()))
        {
            // Look for any movies.
            

            
            if (context.Grades.Any())
            {
                return;   // DB has been seeded
            }
            

            Student neznaiko = new Student();
            //neznaiko.ID = 1;
            neznaiko.Name = "Neznaiko";
            context.Students.Add(neznaiko);

            Subject math = new Subject();
            math.Name = "Math";
            Subject physics = new Subject();
            physics.Name = "Physics";
            Subject bel = new Subject();
            bel.Name = "Bulgarian language and literature";


            context.Subjects.AddRange(
                math, physics, bel
            );



            context.Grades.AddRange(
                new Grade
                {
                    Value = 5,
                    Date = DateTime.Parse("2023-09-01"),
                    Description = "Excellent performance",
                    Type = GradeType.CURRENT,
                    Student = neznaiko,
                    Subject = math
                },
                new Grade
                {
                    Value = 4,
                    Date = DateTime.Parse("2023-09-01"),
                    Description = "No comment",
                    Type = GradeType.SEMESTER_ONE,
                    Student = neznaiko,
                    Subject = math
                },
                new Grade
                {
                    Value = 6,
                    Date = DateTime.Parse("2023-09-01"),
                    Description = "Very good",
                    Type = GradeType.SEMESTER_TWO,
                    Student = neznaiko,
                    Subject = math
                }

            );



            context.SaveChanges();
        }
    }
}
