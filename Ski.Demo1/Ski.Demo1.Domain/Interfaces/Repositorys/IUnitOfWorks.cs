namespace Ski.Demo1.Domain
{
    public interface IUnitOfWorks : IDisposable
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Cart> CartRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Articles> ArticlesRepository { get; }

        void SaveChanges();
    }
}