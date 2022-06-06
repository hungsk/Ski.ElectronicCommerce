using Ski.Demo1.Domain;

namespace Ski.Demo1.Business
{
    public class CartLogic : ICartLogic
    {
        private readonly IUnitOfWorks _unitOfWork;

        public CartLogic(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CartResponse Create(CartRequest request, string username)
        {
            var req = request.data;
            var productEntity = _unitOfWork.ProductRepository.Get(filter: item => item.id == req.product_id).FirstOrDefault();
            var cartEntity = _unitOfWork.CartRepository.Get(filter: item => item.productid == req.product_id && item.username == username).FirstOrDefault();

            var result = new CartResponse();
            if (cartEntity == null)
            {
                var cartNewEntity = new Cart
                {
                    id = Guid.NewGuid().ToString("N"),
                    username = username,
                    productid = productEntity.id,
                    qty = req.qty,
                    final_total = productEntity.price * req.qty,
                    total = productEntity.price * req.qty,
                    is_enabled = 1,
                    coupon_code = ""
                };
                _unitOfWork.CartRepository.Create(cartNewEntity);

                result = new CartResponse()
                {
                    success = true,
                    message = "已加入購物車",
                    data = new CartResponse.CartResponseData()
                    {
                        product_id = cartNewEntity.productid,
                        qty = cartNewEntity.qty,
                        coupon_code = cartNewEntity.coupon_code,
                        id = cartNewEntity.id,
                        total = cartNewEntity.total,
                        final_total = cartNewEntity.final_total,
                        product = productEntity
                    }
                };
            }
            else
            {
                result = Update(new CartRequest()
                {
                    data = new CartRequest.CartRequestData()
                    {
                        product_id = req.product_id,
                        qty = cartEntity.qty += req.qty
                    }
                }, cartEntity.id, username);
            }

            return result;
        }

        public CartResponse Update(CartRequest request, string id, string username)
        {
            var req = request.data;
            var cartEntity = _unitOfWork.CartRepository.Get(filter: item => item.productid == req.product_id && item.username == username).FirstOrDefault();

            var result = new CartResponse();
            if (cartEntity == null)
            {
                result.success = false;
                result.message = "查無購物車";
            }
            else
            {
                var productEntity = _unitOfWork.ProductRepository.Get(filter: item => item.id == req.product_id).FirstOrDefault();

                cartEntity.qty = req.qty;
                cartEntity.total = productEntity.price * cartEntity.qty;
                cartEntity.final_total = productEntity.price * cartEntity.qty;
                _unitOfWork.CartRepository.Update(cartEntity);

                result.success = true;
                result.message = "已更新購物車";
                result.data = new CartResponse.CartResponseData()
                {
                    product_id = cartEntity.productid,
                    qty = cartEntity.qty
                };
            }
            return result;
        }

        public EditResponse Delete(string username)
        {
            var cartEntity = _unitOfWork.CartRepository.Get(filter: item => item.username == username).FirstOrDefault();
            _unitOfWork.CartRepository.Delete(cartEntity);

            var result = new EditResponse();
            result.success = true;
            result.message = "已刪除";

            return result;
        }

        public EditResponse Delete(string id, string username)
        {
            var cartEntity = _unitOfWork.CartRepository.Get(filter: item => item.id == id && item.username == username).FirstOrDefault();
            _unitOfWork.CartRepository.Delete(cartEntity);

            var result = new EditResponse();
            if (cartEntity == null)
            {
                result.success = false;
                result.message = "查無購物車";
            }
            else
            {
                _unitOfWork.CartRepository.Delete(cartEntity);

                result.success = true;
                result.message = "已刪除";
            }
            return result;
        }

        public CartListResponse GetList(string username)
        {
            var cartEntity = _unitOfWork.CartRepository.Get(filter: item => item.username == username).ToList();

            var result = new CartListResponse();
            if (cartEntity == null || cartEntity.Count == 0)
            {
                result.success = true;
                result.data = new CartListResponse.CartListResponseData()
                {
                    carts = new List<CartDTO>(),
                    total = 0,
                    final_total = 0
                };
                result.messages = new List<string> { };
            }
            else
            {
                result = cartEntity.Select(c => new CartListResponse()
                {
                    success = true,
                    data = new CartListResponse.CartListResponseData()
                    {
                        carts = cartEntity.Select(x => new CartDTO()
                        {
                            coupon = null,
                            id = x.id,
                            qty = x.qty,
                            total = x.total,
                            final_total = x.final_total,
                            product_id = x.productid,
                            product = _unitOfWork.ProductRepository.Get(filter: item => item.id == x.productid).FirstOrDefault()
                        }).ToList(),
                        total = cartEntity.Select(c => c.total).Sum(),
                        final_total = cartEntity.Select(c => c.final_total).Sum()
                    }
                }).FirstOrDefault();
            }

            return result;
        }

        public void Dispose()
        {
            _unitOfWork.SaveChanges();//Saves all unsaved result before disposing
            _unitOfWork.Dispose();
        }
    }
}