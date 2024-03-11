using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TestTask.Entities;
using TestTask.Entities.Models;
using TestTask.Services.Authorization;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("/Service")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;

        private IProductService _productService;

        private IOrderService _orderService;

        private IProductAndOrderService _productAndOrderService;

        private IUserService _userService;

        public ServiceController(ILogger<ServiceController> logger,
                                 IProductService productService,
                                 IOrderService orderService,
                                 IProductAndOrderService productAndOrderService,
                                 IUserService userService)
        {
            _logger = logger;

            _productService = productService;

            _orderService = orderService;

            _productAndOrderService = productAndOrderService;

            _userService = userService;
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<ProductInfo>>> GetProducts()
        {
            var availableProducts = _productService.GetAllProducts().Where(p => p.IsAvailaleToOrder);

            var productsInfo = from product in availableProducts
                               select new ProductInfo
                               {
                                   Name = product.Name,

                                   Price = product.Price,

                                   Description = product.Description
                               };

            return Ok(productsInfo);
        }


        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<OrderInfo>>> GetOrders()
        {
            var orders = _orderService.GetAllOrders();

            var products = _productService.GetAllProducts();

            var productsAndOrder = _productAndOrderService.GetProductAndOrder();

            IEnumerable<OrderInfo?> ordersInfo = productsAndOrder.GroupBy(po => po.OrderId).Select(group =>
            {
                var order = orders.FirstOrDefault(o => o.Id == group.Key);

                if (order != null)
                {
                    return new OrderInfo
                    {
                        CreateDate = order.CreatedDate,

                        ProductsInOrder = group.Select(po =>
                        {
                            var product = products.FirstOrDefault(p => p.Id == po.ProductId);

                            return new ProductInfo
                            {
                                Name = product.Name,

                                Description = product.Description,

                                Price = product?.Price ?? 0
                            };
                        }).ToList()
                    };
                }

                return null;

            }).Where(orderInfo => orderInfo != null);

            return Ok(ordersInfo);
        }


        [JwtAuthorize]
        [HttpPost("add_new_product")]
        public async Task<ActionResult<string>> AddNewProduct([FromBody] ProductInfo product)
        {
            if (product == null)
                return BadRequest(ModelState);

            if (product.Price < 0)
                return BadRequest("Цена должна быть неотрицательной ");


            var newProduct = new Product
            {
                Description = product.Description,

                Name = product.Name,

                Price = product.Price,
            };

            await _productService.AddNewProduct(newProduct);

            return Ok($"Товар {product.Name} добавлен в доступные для заказа");
        }

        [JwtAuthorize]
        [HttpDelete("remove_product")]
        public async Task<ActionResult<string>> RemoveProduct([FromBody] ProductInfo product)
        {
            if (!_productService.DisableProduct(product))
                BadRequest($"Товар не найден");

            return Ok($"Товар успешно удалён из доступных для заказа");
        }

        [JwtAuthorize]
        [HttpPost("create_order")]
        public async Task<ActionResult<string>> CreateOrder([FromBody] List<string> productsNames)
        {
            var newOrder = new Order();

            await _orderService.SaveOrder(newOrder);

            var products = _productService.GetAllProducts();

            foreach (string productName in productsNames)
            {
                var productInOrder = products.FirstOrDefault(p => p.Name == productName);

                if (productInOrder == null)
                    return BadRequest($"Товар с именем '{productName}' не найден.");

                await _productAndOrderService.SetProductAndOrder(productInOrder.Id, newOrder.Id);

            }

            return Ok($"Заказ создан успешно");
        }

        [JwtAuthorize]
        [HttpPut("get_user_orders")]
        public async Task<ActionResult<IEnumerable<OrderInfo>>> GetUserOrders([FromBody]string userName)
        {
            var user = _userService.GetAllUsers().Where(p => p.UserName == userName).FirstOrDefault();

            if (user == null)
                return NotFound();

            var response = _orderService.GetUserOrdersByUserId(user.Id);

            return Ok(response);
        }

        [JwtAuthorize]
        [HttpDelete("remove_order")]
        public async Task<ActionResult<string>> RemoveOrder([FromBody] OrderInfo orderInfo)
        {
            var products = _productService.GetAllProducts().OrderBy(p => p.Name);

            var orders = _orderService.GetAllOrders();

            var productsAndOrders = _productAndOrderService.GetProductAndOrder().GroupBy(po => po.OrderId);

            List<Guid> productsInOrderIds = new List<Guid>();

            foreach (var product in orderInfo.ProductsInOrder)
            {
                var productId = products.FirstOrDefault(p => p.Name == product.Name)?.Id;

                if (productId.HasValue)
                {
                    productsInOrderIds.Add(productId.Value);
                }
            }

            var orderIdToRemove = (from poGroup in productsAndOrders
                                   where poGroup.Any(po => productsInOrderIds.Contains(po.ProductId))
                                   let order = orders.FirstOrDefault(o => o.Id == poGroup.Key)
                                   where order != null && order.CreatedDate == orderInfo.CreateDate
                                   select poGroup.Key).FirstOrDefault();

            var orderToRemove = orders.FirstOrDefault(o => o.Id == orderIdToRemove);

            if (orderToRemove == null)
                return NotFound($"Заказ не найден");

            await _orderService.DeleteOrder(orderToRemove);

            return Ok($"Заказ успешно отменен");

        }

        [JwtAuthorize]
        [HttpPut("find_products_by_name")]
        public async Task<ActionResult<List<ProductInfo>>> FindProductsByName([FromBody] string productName)
        {
            var products =  _productService.FindProducts(productName);

            if (products == null) return NotFound("Товары не найдены");

            IEnumerable<ProductInfo> result = from product in products
                                              select new ProductInfo
                                              {
                                                  Description = product.Description,

                                                  Name = product.Name,

                                                  Price = product.Price,
                                              };

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserAuthorizeModel>> Register([FromBody] UserRegisterModel user)
        {
            var response = await _userService.Register(user);

            if (response == null) 
                return NotFound("Пользователь с таким именем уже существует");

            var newModel = new UserAuthorizeModel()
            {
                UserName = user.UserName,

                Password = user.Password,

                Token = response
            };

            return Ok(newModel);
        }

        //[HttpPost("authorize")]
        //public async Task<ActionResult<UserAuthorizeModel>> Authorize([FromBody] UserAuthorizeModel user)
        //{
        //}

    }
}

