using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.DTOs.Courses;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using FluentValidation;

namespace DalilAcademy.Infra.Service;

public class CourseService(
    ICourseRepository courseRepository,
    IValidator<CreateCourseDto> createValidator,
    IValidator<UpdateCourseDto> updateValidator)
    : ICourseService
{
    public Task<PagedResult<CourseResponseDto>> GetAllAsync(
        int page, int pageSize, decimal? minPrice, decimal? maxPrice)
        => courseRepository.GetPagedAsync(page, pageSize, minPrice, maxPrice);

    public async Task<CourseResponseDto> GetByIdAsync(int id)
        => await courseRepository.GetDtoByIdAsync(id)
            ?? throw new NotFoundException($"Course {id} was not found.");

    public Task<int> CountAsync()
        => courseRepository.CountAsync();

    public async Task<CourseResponseDto> CreateAsync(CreateCourseDto dto)
    {
        await createValidator.ValidateAndThrowAsync(dto);                    // → 400

        var course = dto.ToEntity();
        var created = await courseRepository.CreateAsync(course);           // duplicate title → 409

        return created.ToDto();
    }

    public async Task UpdateAsync(int id, UpdateCourseDto dto)
    {
        await updateValidator.ValidateAndThrowAsync(dto);                    // → 400

        var course = await courseRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Course {id} was not found.");   // → 404

        course.UpdateFromDto(dto);

        await courseRepository.UpdateAsync(course);                         // duplicate title → 409
    }

    public async Task DeleteAsync(int id)
    {
        if (!await courseRepository.DeleteAsync(id))
            throw new NotFoundException($"Course {id} was not found.");      // → 404
    }
}
