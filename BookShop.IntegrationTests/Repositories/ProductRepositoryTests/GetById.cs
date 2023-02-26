using BookShop.BLL.Entities.Products;
using BookShop.DAL.Data;
using BookShop.UnitTests.Buidlers;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace BookShop.IntegrationTests.Repositories.ProductRepositoryTests;

public class GetById
{
    private readonly AppDbContext _catalogContext;
    private readonly Repository<BaseProduct> _prodRepository;
    private BookBuilder BookBuilder { get; } = new();
    private readonly ITestOutputHelper _output;
    public GetById(ITestOutputHelper output)
    {
        _output = output;
        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCatalog")
            .Options;
        _catalogContext = new AppDbContext(dbOptions);
        _prodRepository = new Repository<BaseProduct>(_catalogContext);
    }

    [Fact]
    public async Task GetsExistingOrder()
    {
        var existingProd = BookBuilder.WithDefaultValues();
        _catalogContext.Books.Add(existingProd);
        _catalogContext.SaveChanges();
        int bookId = existingProd.Id;
        _output.WriteLine($"BookId: {bookId}");

        var bookFromRepo = await _prodRepository.GetByIdAsync(bookId);
        Assert.Equal(BookBuilder.testName, bookFromRepo!.Name);
    }
}