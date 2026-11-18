using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs.Courses
{
    public record CourseResponseDto(
      int Id,
      string Title,
      decimal Price,
      string? InstructorName);     

}
