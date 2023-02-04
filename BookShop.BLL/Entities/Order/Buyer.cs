using Ardalis.GuardClauses;
using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Entities.Order;

public class Buyer : IAggregateRoot
{
    public string Id { get; private set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }


#pragma warning disable CS8618 // Required by Entity Framework
    // private Buyer() { }

    public Buyer(string identity)
    {
        Guard.Against.NullOrEmpty(identity, nameof(identity));
        Id = identity;
    }

}