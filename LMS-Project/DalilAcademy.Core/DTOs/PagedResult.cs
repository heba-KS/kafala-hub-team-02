namespace DalilAcademy.Core.DTOs;

// Lecture 7: one response with the page items + everything the front end needs to draw page buttons
public record PagedResult<T>(List<T> Items, int Page, int PageSize, int TotalCount)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
