using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.DTOs.Courses;
using DalilAcademy.Core.DTOs.Students;
using DalilAcademy.Core.Entities;

namespace DalilAcademy.Core.Repository;

public interface IStudentRepository
{
    // Lecture 7: read-only queries project straight into DTOs (AsNoTracking)
    Task<PagedResult<StudentResponseDto>> GetPagedAsync(int page, int pageSize);

    Task<StudentResponseDto?> GetDtoByIdAsync(int id);

    // null → the student does not exist
    Task<List<CourseResponseDto>?> GetCoursesAsync(int studentId);

    // Entity methods — used for updates and business logic
    Task<Student?> GetByIdAsync(int id);

    Task<Student> CreateAsync(Student student);

    Task UpdateAsync(Student student);

    Task<bool> DeleteAsync(int id);
}
