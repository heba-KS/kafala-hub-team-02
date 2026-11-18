using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs.Courses
{

    public record CreateCourseDto(string Title, decimal Price, int InstructorId);

}

