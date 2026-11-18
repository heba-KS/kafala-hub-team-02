namespace DalilAcademy.Core.DTOs.Enrollments;

// Lecture 6: flat output DTO — student and course names, no nested entities
public record EnrollmentResponseDto(
    int Id,
    int StudentId,
    string StudentName,
    int CourseId,
    string CourseTitle,
    DateTime EnrolledAt);
