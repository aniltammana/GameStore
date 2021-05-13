using GameStore.DB;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace NetCoreCA.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly GameContext _context;

        public ShoppingCartController(GameContext context)
        {
            _context = context;
        }

        // GET: /ShoppingCart/
        public IActionResult Index()
        {
            List<Cart> viewcarts = GetCart();
            decimal gettotal = GetTotal();
            ViewData["Total"] = gettotal.ToString("0.00");
            ViewData["Count"] = GetCount();
            return View(viewcarts);
        }

        //To display cart items
        public List<Cart> GetCart()
        {
            string CartSessionId = Request.Cookies["CartSessionId"];
            List<Cart> cartitems = _context.Carts.Where(x => x.SessionId == CartSessionId).ToList();
            return cartitems;
        }


        public IActionResult AddToCart(string Id)
        {
            //Retrieve Cart
            string sessionId = Request.Cookies["CartSessionId"];
            Session session = _context.Sessions.Find(sessionId);
            Game game = _context.Games.Find(Id);

            //check if product exists in cart 
            Cart cart_check = _context.Carts.Where(x => x.GameId == Id && x.SessionId == sessionId).FirstOrDefault();

            
            //If cart does not exist
            if (cart_check == null)
            {
                Cart cart = new Cart()
                {
                    SessionId = sessionId,
                    GameId = Id,
                    Quantity = 1,
                };

                _context.Add(cart);
                _context.SaveChanges();
            }

            else
            {
                cart_check.Quantity++;
                _context.Update(cart_check);
                _context.SaveChanges();
            }

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(string id)
        {
            string sessionId = Request.Cookies["CartSessionId"];
            var cartItem = _context.Carts.Single(cart => cart.SessionId == sessionId && cart.GameId == id);
       
            if (cartItem != null)
            { 
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();

            }

            return RedirectToAction("Index");
        }

        public IActionResult AddProduct(string id)
        {
            string sessionId = Request.Cookies["CartSessionId"];
            var cartItem = _context.Carts.Single(cart => cart.SessionId == sessionId && cart.GameId == id);

            if (cartItem != null)
            {
                cartItem.Quantity++;

            }
            _context.Update(cartItem);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ReduceProduct(string id)
        {
            string sessionId = Request.Cookies["CartSessionId"];
            var cartItem = _context.Carts.Single(cart => cart.SessionId == sessionId && cart.GameId == id);

            if (cartItem != null && cartItem.Quantity >1)
            {
                cartItem.Quantity--;

            }
            _context.Update(cartItem);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult EmptyCart()
        {
            string sessionId = Request.Cookies["CartSessionId"];
            var cartItems = _context.Carts.Where(cart => cart.SessionId == sessionId);

            foreach (var cartItem in cartItems)
            {
                _context.Carts.Remove(cartItem);
            }

            // Save changes
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult CartSummary()
        {
           return RedirectToAction("Index");
        }

        public int GetCount()
        {
            string sessionId = Request.Cookies["CartSessionId"];
            int? count = (from cartItems in _context.Carts
                          where cartItems.SessionId == sessionId
                          select (int?)cartItems.Quantity).Sum();

            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            string sessionId = Request.Cookies["CartSessionId"];
            decimal? total = (from cartItems in _context.Carts
                              where cartItems.SessionId == sessionId
                              select (int?)cartItems.Quantity * cartItems.Game.Price).Sum();

            //return total ?? decimal.Zero;
            return total ?? decimal.Zero;
        }

        //public IActionResult RemoveFromCart(string id)
        //{
        //    string sessionId = Request.Cookies["CartSessionId"];
        //    var cartItem = _context.Carts.Single(cart => cart.SessionId == sessionId && cart.GameId == id);

        //    if (cartItem != null)
        //    {
        //        if (cartItem.Quantity > 1)
        //        {
        //            cartItem.Quantity--;

        //        }
        //        else
        //        {
        //            _context.Carts.Remove(cartItem);
        //        }

        //        _context.Update(cartItem);
        //        _context.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}

    }
}