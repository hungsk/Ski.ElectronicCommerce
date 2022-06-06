using Ski.Demo1.Domain;

namespace Ski.Demo1.Data
{
    public class UnitOfWork : IUnitOfWorks
    {
        private bool _disposed = false;
        private Demo1DbContext _context;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Cart> _cartRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<User> _userRepository;

        public UnitOfWork(Demo1DbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                {
                    this._productRepository = new GenericRepository<Product>(_context);
                }
                return _productRepository;
            }
        }

        public IGenericRepository<Cart> CartRepository
        {
            get
            {
                if (this._cartRepository == null)
                {
                    this._cartRepository = new GenericRepository<Cart>(_context);
                }
                return _cartRepository;
            }
        }

        public IGenericRepository<Order> OrderRepository
        {
            get
            {
                if (this._orderRepository == null)
                {
                    this._orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }

        public IGenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}