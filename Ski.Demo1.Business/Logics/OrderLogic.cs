using Ski.Demo1.Domain;

namespace Ski.Demo1.Business
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IUnitOfWorks _unitOfWork;

        public OrderLogic(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public OrderResponse Create(OrderRequest request, string username)
        {
            var req = request.data;
            var cartEntity = _unitOfWork.CartRepository.Get(filter: item => item.username == username && item.is_enabled == 1).ToList();

            var orderEntity = new Order
            {
                id = Guid.NewGuid().ToString("N"),
                create_at = DateTime.Now,
                is_paid = false ? 1 : 0,
                payment_method = "credit_card",
                message = req.message,
                total = cartEntity.Select(c => c.total).Sum(),
                user = new User()
                {
                    id = username,
                    name = req.user.name,
                    email = req.user.email,
                    tel = req.user.tel,
                    address = req.user.address
                }
            };

            List<OrderItem> orderItem = new List<OrderItem>();
            foreach (var c in cartEntity)
            {
                //_context.OrderItem.Add(
                //    new OrderItem()
                //    {
                //        id = Guid.NewGuid().ToString("N"),
                //        orderid = c.id,
                //        productid = c.productid,
                //        qty = c.qty,
                //    });
                //order.product = _context.Product.Where(p => p.id == c.productid).ToList();

                orderItem.Add(
                    new OrderItem()
                    {
                        id = Guid.NewGuid().ToString("N"),
                        orderid = c.id,
                        productid = c.productid,
                        qty = c.qty,
                        total = c.total,
                        final_total = c.final_total
                    });

                c.is_enabled = 0;
            }
            orderEntity.orderItem = orderItem;

            var userEntity = _unitOfWork.UserRepository.Get(filter: item => item.id == username).FirstOrDefault();
            if (userEntity == null)
            {
                orderEntity.user = new User()
                {
                    id = username,
                    name = req.user.name,
                    email = req.user.email,
                    tel = req.user.tel,
                    address = req.user.address
                };
            }
            else
            {
                orderEntity.user = userEntity;
            }
            _unitOfWork.OrderRepository.Create(orderEntity);

            var result = new OrderResponse()
            {
                success = true,
                message = "已建立訂單",
                total = orderEntity.total,
                create_at = orderEntity.create_at.GetHashCode(),
                orderId = orderEntity.id
            };

            return result;
        }

        public EditResponse Update(OrderUpdateRequest request, string id)
        {
            var req = request.data;
            var orderEntity = _unitOfWork.OrderRepository.Get(
                    filter: item => item.id == req.id && item.id == id,
                    null,
                    order => order.orderItem, order => order.user
                ).FirstOrDefault();

            var result = new EditResponse();
            if (orderEntity == null)
            {
                result.success = false;
                result.message = "查無訂單";
            }
            else
            {
                orderEntity.is_paid = request.data.is_paid ? 1 : 0;
                orderEntity.user.name = request.data.user.name;
                orderEntity.user.tel = request.data.user.tel;
                orderEntity.user.email = request.data.user.email;
                orderEntity.user.address = request.data.user.address;

                foreach (var x in request.data.products)
                {
                    foreach (var i in orderEntity.orderItem)
                    {
                        if (x.product_id == i.productid)
                        {
                            i.qty = x.qty;
                            i.total = x.total;
                            i.final_total = x.final_total;
                        }
                    }
                }
                orderEntity.total = req.total;
                _unitOfWork.OrderRepository.Update(orderEntity);

                result.success = true;
                result.message = "已更新訂單";
            }
            return result;
        }

        public EditResponse Delete(string id)
        {
            var orderEntity = _unitOfWork.OrderRepository.Get(item => item.id == id).FirstOrDefault();
            _unitOfWork.OrderRepository.Delete(orderEntity);

            var result = new EditResponse();
            if (orderEntity == null)
            {
                result.success = false;
                result.message = "查無此訂單";
            }
            else
            {
                result.success = true;
                result.message = "刪除成功";
            }
            return result;
        }

        public OrderGetResponse Get(string id)
        {
            var orderEntity = _unitOfWork.OrderRepository.Get(
                filter: item => item.id == id,
                null,
                x => x.orderItem, x => x.user
                ).FirstOrDefault();

            var result = new OrderGetResponse();
            if (orderEntity == null)
            {
                result.success = false;
            }
            else
            {
                result.success = true;
                result.order = new OrderDTO()
                {
                    create_at = orderEntity.create_at.GetHashCode(),
                    id = orderEntity.id,
                    is_paid = orderEntity.is_paid == 1 ? true : false,
                    message = orderEntity.message,
                    payment_method = orderEntity.payment_method,
                    total = orderEntity.total,
                    user = Mapper(orderEntity.user)
                };

                List<OrderDTO.ProductInfo> productInfo = new List<OrderDTO.ProductInfo>();
                foreach (var i in orderEntity.orderItem)
                {
                    var productEntity = _unitOfWork.ProductRepository.Get(
                        filter: item => item.id == i.productid,
                        includeProperties: product => product.imagesUrl
                        ).FirstOrDefault();
                    productInfo.Add(
                        new OrderDTO.ProductInfo()
                        {
                            final_total = i.final_total,
                            id = i.id,
                            product_id = i.productid,
                            qty = i.qty,
                            product = ProductLogic.Mapper(productEntity),
                            total = i.total
                        });
                }
                result.order.products = productInfo;
            }

            return result;
        }

        public OrderListResponse GetList(int page)
        {
            var orderEntity = _unitOfWork.OrderRepository.Get(
                null,
                null,
                order => order.orderItem, order => order.user
                ).ToList();

            var result = new OrderListResponse();
            result.success = true;
            result.orders = orderEntity.Select((x, index) => Mapper(x, index)).ToList();

            var count = orderEntity.Count;
            var pagination = new Pagination()
            {
                total_pages = (count / 10) <= 0 ? 1 : (count / 10),
                current_page = page,
                has_pre = (page == 1) ? false : true,
                has_next = (page == count) ? false : true,
                category = ""
            };
            result.pagination = pagination;
            result.messages = new List<string> { };

            return result;
        }

        public void Dispose()
        {
            _unitOfWork.SaveChanges();//Saves all unsaved result before disposing
            _unitOfWork.Dispose();
        }

        private UserDTO Mapper(User x)
        {
            var result = new UserDTO()
            {
                name = x.name,
                email = x.email,
                tel = x.tel,
                address = x.address
            };

            return result;
        }

        private OrderDTO Mapper(Order x, int index)
        {
            var result = new OrderDTO()
            {
                create_at = x.create_at.GetHashCode(),
                id = x.id,
                is_paid = x.is_paid == 1 ? true : false,
                message = x.message,
                payment_method = x.payment_method,
                total = x.total,
                user = new UserDTO()
                {
                    name = x.user.name,
                    email = x.user.email,
                    tel = x.user.tel,
                    address = x.user.address
                }
            };

            List<OrderDTO.ProductInfo> productInfo = new List<OrderDTO.ProductInfo>();
            foreach (var i in x.orderItem)
            {
                var productEntity = _unitOfWork.ProductRepository.Get(
                    filter: item => item.id == i.productid,
                    includeProperties: product => product.imagesUrl
                    ).FirstOrDefault();

                productInfo.Add(
                    new OrderDTO.ProductInfo()
                    {
                        coupon = new CouponDTO()
                        {
                            code = "testCode",
                            due_date = "",
                            id = "-1",
                            is_enabled = 1,
                            percent = 0,
                            title = "無特惠價格"
                        },
                        final_total = i.final_total,
                        id = i.id,
                        product_id = i.productid,
                        qty = i.qty,
                        product = ProductLogic.Mapper(productEntity),
                        total = i.total
                    });
            }
            result.products = productInfo;
            result.num = index + 1;

            return result;
        }
    }
}