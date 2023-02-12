namespace BookShop.Admin;

public static class SD
{
    public static string? AdminAPIBase { get; set; }

    public enum APIType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}