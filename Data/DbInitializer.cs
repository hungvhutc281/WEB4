using lap4.Models;
using Microsoft.EntityFrameworkCore;

namespace lap4.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SchoolContext(serviceProvider
            .GetRequiredService<DbContextOptions<SchoolContext>>()))
            {
                context.Database.EnsureCreated();
                if (context.Learners.Any())
                {
                    return;
                }
                var majors = new Major[] {
                new Major{MajorID = 1, MajorName="IT"},
                new Major{MajorID = 2, MajorName="Economics"},
                new Major{MajorID = 3, MajorName="Mathematics"},
                };
                foreach (var major in majors)
                {
                    var existingMajor = context.Majors.Find(major.MajorID);
                    if (existingMajor == null)
                    {
                        context.Majors.Add(major);
                    }
                }
               
                context.SaveChanges();
                var learners = new
                Learner[] {
                    new Learner {LearnerID=1, FirstMidName = "Carson", LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2005-09-01") , MajorID = 1},
                    new Learner {LearnerID=2, FirstMidName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2002-09-01"), MajorID = 2 }
                };
                foreach (Learner l in learners)
                {
                    context.Learners.Add(l);
                }
                context.SaveChanges();
                var courses = new Course[]{
                    new Course{CourseID=1050,Title="Chemistry",Credits=3},
                    new Course{CourseID=4022,Title="Microeconomics",Credits=3},
                    new Course{CourseID=4041,Title="Macroeconomics",Credits=3}
                };
                foreach (Course c in courses)
                {
                    context.Courses.Add(c);
                }
                context.SaveChanges();
                var enrollments = new Enrollment[]{
                    new Enrollment{EnrollmentID = 1, LearnerID=1,CourseID=1050,Grade=5.5f},
                    new Enrollment{EnrollmentID = 2, LearnerID=1,CourseID=4022,Grade=7.5f},
                    new Enrollment{EnrollmentID = 1, LearnerID=2,CourseID=1050,Grade=3.5f},
                    new Enrollment{EnrollmentID = 3, LearnerID=2,CourseID=4041,Grade=7f}

                };
                foreach (Enrollment e in enrollments)
                {
                    context.Enrollments.Add(e);
                }
                context.SaveChanges();
            }
        }
    }
}
