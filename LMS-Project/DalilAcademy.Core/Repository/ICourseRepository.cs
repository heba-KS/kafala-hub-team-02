using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.DTOs.Courses;
using DalilAcademy.Core.Entities;

namespace DalilAcademy.Core.Repository;

public interface ICourseRepository
{
    // Lecture 7: read-only queries project straight into DTOs (AsNoTracking)
    Task<PagedResult<CourseResponseDto>> GetPagedAsync(
        int page, int pageSize, decimal? minPrice, decimal? maxPrice);

    Task<CourseResponseDto?> GetDtoByIdAsync(int id);

    Task<int> CountAsync();

    // Entity methods — used for updates and business logic
    Task<Course?> GetByIdAsync(int id);

    Task<Course> CreateAsync(Course course);

    Task UpdateAsync(Course course);

    Task<bool> DeleteAsync(int id);
}
