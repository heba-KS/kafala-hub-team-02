using DalilAcademy.Core.Entities;
using DalilAcademy.Core.DTOs.Courses;

namespace DalilAcademy.Core.Mappings;

public static class CourseMappings
{
    public static CourseResponseDto ToDto(
        this Course course) => new(
            course.Id,
            course.Title,
            course.Price,
            course.Instructor?.FullName
        );

    public static Course ToEntity(
        this CreateCourseDto dto) => new()
        {
            Title = dto.Title,
            Price = dto.Price,
            InstructorId = dto.InstructorId
        };

    
    public static void UpdateFromDto(
        this Course course,
        UpdateCourseDto dto)
    {
        course.Title = dto.Title;
        course.Price = dto.Price;
    }
}
