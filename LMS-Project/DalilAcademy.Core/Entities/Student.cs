using System;
using System.Collections.Generic;
using System.Text;

using System.Text.Json.Serialization;


namespace DalilAcademy.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }


        public List<Enrollment> Enrollments { get; set; } = [];
    }
}

