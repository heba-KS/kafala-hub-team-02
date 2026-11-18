namespace DalilAcademy.API.Controllers;

// Lab 7 – Task 3: protect the server — never more than 50 items per page
internal static class Paging
{
    public const int DefaultPageSize = 10;
    public const int MaxPageSize = 50;

    public static (int Page, int PageSize) Normalize(int page, int pageSize)
        => (Math.Max(page, 1), Math.Clamp(pageSize, 1, MaxPageSize));
}
