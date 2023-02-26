namespace BookShop.FunctionalTests.Web.Controllers;

public class CatalogControllerIndex : IClassFixture<TestApplication>
{
    public CatalogControllerIndex(TestApplication factory)
    {
        Client = factory.CreateClient();
    }

    public HttpClient Client { get; }

    [Fact]
    public async Task ReturnsCatalogPageWithProductListing()
    {

        var response = await Client.GetAsync("/Catalog");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();

        Assert.Contains("b_04.jpg", stringResponse);
    }
}