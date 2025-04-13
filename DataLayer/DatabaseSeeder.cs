using System;
using System.Linq;
using DataLayer.Model;
using Welcome.Others;

namespace DataLayer.Database
{
    public static class DatabaseSeeder
    {
        public static void SeedData(DatabaseContext context)
        {
            if (!context.Users.Any())
            {
                var users = new[]
                {
                    new DatabaseUser
                    {
                        Username = "admin",
                        Password = "admin123",
                        Role = UserRole.ADMIN,
                        Expires = DateTime.Now.AddYears(1),
                        FacultyNumber = "20240001",
                        Email = "admin@university.com",
                        FirstName = "Admin",
                        LastName = "User",
                        PhoneNumber = "+359888123456",
                        Department = "Administration",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Address = "Sofia, Bulgaria",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    },
                    new DatabaseUser
                    {
                        Username = "student1",
                        Password = "student123",
                        Role = UserRole.STUDENT,
                        Expires = DateTime.Now.AddYears(1),
                        FacultyNumber = "20240002",
                        Email = "student1@university.com",
                        FirstName = "John",
                        LastName = "Doe",
                        PhoneNumber = "+359888234567",
                        Department = "Computer Science",
                        DateOfBirth = new DateTime(2000, 5, 15),
                        Address = "Plovdiv, Bulgaria",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    },
                    new DatabaseUser
                    {
                        Username = "student2",
                        Password = "student123",
                        Role = UserRole.STUDENT,
                        Expires = DateTime.Now.AddYears(1),
                        FacultyNumber = "20240003",
                        Email = "student2@university.com",
                        FirstName = "Jane",
                        LastName = "Smith",
                        PhoneNumber = "+359888345678",
                        Department = "Computer Science",
                        DateOfBirth = new DateTime(2000, 8, 20),
                        Address = "Varna, Bulgaria",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    },
                    new DatabaseUser
                    {
                        Username = "teacher1",
                        Password = "teacher123",
                        Role = UserRole.PROFESSOR,
                        Expires = DateTime.Now.AddYears(1),
                        FacultyNumber = "20240004",
                        Email = "teacher1@university.com",
                        FirstName = "Prof",
                        LastName = "Johnson",
                        PhoneNumber = "+359888456789",
                        Department = "Computer Science",
                        DateOfBirth = new DateTime(1975, 3, 10),
                        Address = "Sofia, Bulgaria",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    }
                };

                context.Users.AddRange(users);
            }

            if (!context.Disciplines.Any())
            {
                var disciplines = new[]
                {
                    new Discipline
                    {
                        Name = "Introduction to Programming",
                        Description = "Basic programming concepts and practices",
                        Year = 1,
                        Semester = 1,
                        Credits = 6,
                        Lecturer = "Dr. Smith",
                        MaxStudents = 100
                    },
                    new Discipline
                    {
                        Name = "Data Structures",
                        Description = "Study of fundamental data structures and algorithms",
                        Year = 1,
                        Semester = 2,
                        Credits = 6,
                        Lecturer = "Dr. Johnson",
                        MaxStudents = 80
                    },
                    new Discipline
                    {
                        Name = "Database Systems",
                        Description = "Introduction to database design and management",
                        Year = 2,
                        Semester = 1,
                        Credits = 6,
                        Lecturer = "Dr. Williams",
                        MaxStudents = 60
                    },
                    new Discipline
                    {
                        Name = "Web Development",
                        Description = "Modern web development technologies and practices",
                        Year = 2,
                        Semester = 2,
                        Credits = 6,
                        Lecturer = "Dr. Brown",
                        MaxStudents = 70
                    },
                    new Discipline
                    {
                        Name = "Software Engineering",
                        Description = "Software development methodologies and practices",
                        Year = 3,
                        Semester = 1,
                        Credits = 6,
                        Lecturer = "Dr. Davis",
                        MaxStudents = 50
                    },
                    new Discipline
                    {
                        Name = "Artificial Intelligence",
                        Description = "Introduction to AI concepts and machine learning",
                        Year = 3,
                        Semester = 2,
                        Credits = 6,
                        Lecturer = "Dr. Miller",
                        MaxStudents = 40
                    },
                    new Discipline
                    {
                        Name = "Computer Networks",
                        Description = "Network protocols and architecture",
                        Year = 4,
                        Semester = 1,
                        Credits = 6,
                        Lecturer = "Dr. Wilson",
                        MaxStudents = 45
                    },
                    new Discipline
                    {
                        Name = "Cybersecurity",
                        Description = "Information security and protection",
                        Year = 4,
                        Semester = 2,
                        Credits = 6,
                        Lecturer = "Dr. Moore",
                        MaxStudents = 35
                    },
                    new Discipline
                    {
                        Name = "Mobile Development",
                        Description = "Mobile app development for iOS and Android",
                        Year = 3,
                        Semester = 1,
                        Credits = 6,
                        Lecturer = "Dr. Taylor",
                        MaxStudents = 55
                    },
                    new Discipline
                    {
                        Name = "Cloud Computing",
                        Description = "Cloud services and distributed systems",
                        Year = 4,
                        Semester = 1,
                        Credits = 6,
                        Lecturer = "Dr. Anderson",
                        MaxStudents = 40
                    },
                    new Discipline
                    {
                        Name = "Game Development",
                        Description = "Game design and development principles",
                        Year = 3,
                        Semester = 2,
                        Credits = 6,
                        Lecturer = "Dr. Thomas",
                        MaxStudents = 30
                    },
                    new Discipline
                    {
                        Name = "Blockchain Technology",
                        Description = "Blockchain fundamentals and applications",
                        Year = 4,
                        Semester = 2,
                        Credits = 6,
                        Lecturer = "Dr. Jackson",
                        MaxStudents = 25
                    }
                };

                context.Disciplines.AddRange(disciplines);
            }

            context.SaveChanges();
        }
    }
} 