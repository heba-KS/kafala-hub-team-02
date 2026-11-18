# Lab 7 — Query Like a Pro

SQL logging is on (`Microsoft.EntityFrameworkCore.Database.Command: Information` in appsettings).
Count the `Executed DbCommand` lines in the console for each request.

| Endpoint | Before (Lecture 6) | After (Lecture 7) |
|---|---|---|
| `GET /api/students/{id}/courses` | 1 query, full tracked entities (Student + Enrollments + Courses + Instructors, every column) | **1 query**, projection + AsNoTracking, only the 4 DTO columns |
| `GET /api/students` | 1 query, every student, every column | **2 queries** (COUNT + one page), max 50 rows |
| `GET /api/courses` | 1 query, every course + instructor (Include) | **2 queries** (COUNT + one page), projection, max 50 rows |
| `GET /api/courses?minPrice=&maxPrice=` | — | **2 queries**, `WHERE` runs in PostgreSQL |
| `GET /api/courses/count` | — | **1 query** `SELECT count(*)::int` |

## Tasks
1. **A student's courses** — `StudentRepository.GetCoursesAsync`: one projected query; `null` → 404, empty list → not enrolled.
2. **Paginate students** — `GET /api/students?page=1&pageSize=10` → `PagedResult<StudentResponseDto>`.
3. **Cap the page size** — `Paging.Normalize` → `Math.Clamp(pageSize, 1, 50)`, page ≥ 1.
4. **Price filter** — `GET /api/courses?minPrice=50&maxPrice=70` → `Where()` before `CountAsync`/`ToListAsync`.
5. **Count endpoint** — `GET /api/courses/count` → `CountAsync()`.
