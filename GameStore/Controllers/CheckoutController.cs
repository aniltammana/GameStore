using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.DB;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly GameContext _context;

        public CheckoutController(GameContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //Check if user is logged in. 
            string userSession = Request.Cookies["CartSessionId"];
            Session session = _context.Sessions.Find(userSession);

            //If user is not logged in, redirect to login 
            if (session.Username == null)
            {
                return RedirectToAction("Index", "Login");
            }

            CreateOrder(userSession, session.Username);

            GenerateActivationCode();

            ClearCartSession();

            return RedirectToAction("Index", "MyPurchases");
        }

        public List<Cart> GetCart()
        {
            string CartSessionId = Request.Cookies["CartSessionId"];
            List<Cart> cartitems = _context.Carts.Where(x => x.SessionId == CartSessionId).ToList();
            return cartitems;
        }

        public void CreateOrder(string userSession, string username)
        {
            List<Cart> cartList = GetCart();

            decimal? total = (from cartItems in _context.Carts
                              where cartItems.SessionId == userSession
                              select (int?)cartItems.Quantity * cartItems.Game.Price).Sum();

            Order order = new Order()
            {

                OrderId = userSession,
                Userusername = username,
                Total = (decimal)total,
                OrderDate = DateTime.Now
            };

            _context.Add(order);
            _context.SaveChanges();

            foreach (var cart in cartList)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    GameId = cart.GameId,
                    Quantity = cart.Quantity,
                    Price = cart.Game.Price

                };

                _context.Add(orderDetail);
                _context.SaveChanges();

            }

        }

        public void ClearCartSession()
        {
            //clear the session cookie and direct to login page at /Login/Index
            EmptyCart();
            HttpContext.Session.Clear();
        }

        public void EmptyCart()
        {
            string sessionId = Request.Cookies["CartSessionId"];
            var cartItems = _context.Carts.Where(cart => cart.SessionId == sessionId);

            foreach (var cartItem in cartItems)
            {
                _context.Carts.Remove(cartItem);
            }

            // Save changes
            _context.SaveChanges();
        }

        public List<OrderDetail> GetOrderDetails(string sessionId)
        {
            Order order = _context.Orders.Find(sessionId);
            List<OrderDetail> orderdetails = _context.OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();

            return orderdetails;
        }

        public void GenerateActivationCode()
        {
            //get order details
            string sessionId = Request.Cookies["CartSessionId"];

            List<OrderDetail> OrderDetailList = GetOrderDetails(sessionId);

            foreach (var item in OrderDetailList)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    ActivationCode active = new ActivationCode()
                    {
                        OrderDetailId = item.Id,
                        Activationcode = Guid.NewGuid()
                    };
                    _context.Add(active);
                }

                // Save changes
                _context.SaveChanges();
            }
        }

    }
}

    

