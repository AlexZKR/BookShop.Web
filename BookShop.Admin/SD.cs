namespace BookShop.Admin;

public static class SD
{
    public static string? MainAPIBase { get; set; }

    public enum APIType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}