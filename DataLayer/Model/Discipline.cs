using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Model
{
    public class Discipline
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Semester { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public int Credits { get; set; }

        public string? Lecturer { get; set; }

        public string? Professor { get; set; }

        public int MaxStudents { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 