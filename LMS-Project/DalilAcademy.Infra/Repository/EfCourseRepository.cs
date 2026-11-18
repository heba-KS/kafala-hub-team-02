using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.DTOs.Courses;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DalilAcademy.Infra.Repository;

public class EfCourseRepository(AppDbContext db)
    : ICourseRepository
{
    // Lecture 7: Where → Count → OrderBy → Skip → Take → Select — all inside PostgreSQL
    public async Task<PagedResult<CourseResponseDto>> GetPagedAsync(
        int page, int pageSize, decimal? minPrice, decimal? maxPrice)
    {
        var query = db.Courses.AsNoTracking();

        if (minPrice is not null)
            query = query.Where(c => c.Price >= minPrice.Value);

        if (maxPrice is not null)
            query = query.Where(c => c.Price <= maxPrice.Value);

        var total = await query.CountAsync();                 // query 1: the total

        var items = await query
            .OrderBy(c => c.Id)                               // always order before Skip
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CourseResponseDto(
                c.Id,
                c.Title,
                c.Price,
                c.Instructor!.FullName))
            .ToListAsync();                                   // query 2: one page

        return new PagedResult<CourseResponseDto>(items, page, pageSize, total);
    }

    public Task<CourseResponseDto?> GetDtoByIdAsync(int id)
        => db.Courses
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CourseResponseDto(
                c.Id,
                c.Title,
                c.Price,
                c.Instructor!.FullName))
            .FirstOrDefaultAsync();

    // SELECT count(*)::int FROM "Courses"
    public Task<int> CountAsync()
        => db.Courses.CountAsync();

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await db.Courses
            .Include(c => c.Instructor)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Course> CreateAsync(Course course)
    {
        try
        {
            db.Courses.Add(course);
            await db.SaveChangesAsync();

            // Load the instructor so the response can show InstructorName
            await db.Entry(course).Reference(c => c.Instructor).LoadAsync();

            return course;
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.UniqueViolation
            })
        {
            throw new ConflictException(
                "A course with this title already exists.");
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.ForeignKeyViolation
            })
        {
            throw new NotFoundException(
                $"Instructor {course.InstructorId} was not found.");
        }
    }

    public async Task UpdateAsync(Course course)
    {
        try
        {
            db.Courses.Update(course);
            await db.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.UniqueViolation
            })
        {
            throw new ConflictException(
                "A course with this title already exists.");
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var course = await db.Courses.FindAsync(id);

        if (course is null)
            return false;

        db.Courses.Remove(course);
        await db.SaveChangesAsync();

        return true;
    }
}
