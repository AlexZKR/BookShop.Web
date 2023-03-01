namespace BookShop.Admin;

public static class SD
{
    public static string? MainAPIBase { get; set; }
    public static int PageSize { get; set; } = 2;

    public enum APIType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}