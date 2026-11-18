using DalilAcademy.Core.DTOs.Enrollments;

namespace DalilAcademy.Core.Service;

public interface IEnrollmentService
{
    Task<EnrollmentResponseDto> EnrollAsync(
        int studentId, int courseId);
}
