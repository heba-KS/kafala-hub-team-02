using DalilAcademy.Core.DTOs.Enrollments;
using DalilAcademy.Core.Entities;

namespace DalilAcademy.Core.Mappings;

public static class EnrollmentMappings
{
    public static EnrollmentResponseDto ToDto(this Enrollment enrollment) => new(
        enrollment.Id,
        enrollment.StudentId,
        enrollment.Student?.FullName ?? string.Empty,
        enrollment.CourseId,
        enrollment.Course?.Title ?? string.Empty,
        enrollment.EnrolledAt);
}
