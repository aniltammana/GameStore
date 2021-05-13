using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.DB;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreCA.Controllers
{
    public class MyPurchasesController : Controller
    {
        private readonly GameContext _context;

        public MyPurchasesController(GameContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string sessionId = Request.Cookies["CartSessionId"];

           //get order 
            Order order = GetOrder(sessionId);

            //get OrderDetailList
            List<OrderDetail> orderdetails = _context.OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();


            return View(orderdetails);
        }

        public IActionResult GetActivationCode(OrderDetail orderdetail)
        {
            List<ActivationCode> activeList = _context.ActivationCodes.Where(x => x.OrderDetailId ==orderdetail.Id).ToList();

            return RedirectToAction("Index");
        }

        public Order GetOrder(string sessionId)
        {
            Order order = _context.Orders.Find(sessionId);
            return order;
        }



    }  
}