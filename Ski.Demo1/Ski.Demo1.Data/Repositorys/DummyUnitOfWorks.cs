using Ski.Demo1.Domain;

namespace Ski.Demo1.Data
{
    public class DummyUnitOfWorks : IUnitOfWorks
    {
        private DummyGenericRepository<Product> _productRepository;
        private DummyGenericRepository<Cart> _cartRepository;
        private DummyGenericRepository<Order> _orderRepository;
        private DummyGenericRepository<User> _userRepository;

        private List<Product> _prodcutContext = null;
        private List<Cart> _cartContext = null;
        private List<Order> _orderContext = null;
        private List<User> _userContext = null;

        public DummyUnitOfWorks(List<Product> context)
        {
            _prodcutContext = context;
        }

        public DummyUnitOfWorks(List<Cart> context)
        {
            _cartContext = context;
        }

        public DummyUnitOfWorks(List<Order> context)
        {
            _orderContext = context;
        }

        public DummyUnitOfWorks(List<User> context)
        {
            _userContext = context;
        }

        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                {
                    this._productRepository = new DummyGenericRepository<Product>(_prodcutContext);
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
                    this._cartRepository = new DummyGenericRepository<Cart>(_cartContext);
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
                    this._orderRepository = new DummyGenericRepository<Order>(_orderContext);
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
                    this._userRepository = new DummyGenericRepository<User>(_userContext);
                }
                return _userRepository;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}