
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokStart.DAL;
using PustokStart.Models;
using PustokStart.ViewModels;

namespace PustokStart.Controllers
{
    public class BookController:Controller
    {
        public readonly PustokContext _context;
        public BookController(PustokContext context)
        {
            _context= context;
        }
        public IActionResult GetBookDetail(int id)
        {
            Book book =_context.Books.Include(x=>x.Author).Include(x=>x.BookImages).Include(x=>x.Tags).ThenInclude(x=>x.Tag).FirstOrDefault(x=>x.Id==id);
            if (book == null) StatusCode(404);

          return PartialView("_BookModalPartial",book);
        }
        public IActionResult AddToBasket(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("loadpage");
            };
           
            List<BasketItemViewModel> cookieItems = new List<BasketItemViewModel>();
            BasketItemViewModel cookieitem;
            var basketStr = Request.Cookies["Basket"];

            if (basketStr != null)
            {
                cookieItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketStr);
                 cookieitem = cookieItems.FirstOrDefault(x=>x.BookId==id);
                if(cookieitem != null) 
                {
                    cookieitem.Count++;
                }
                else
                {
                    cookieitem = new BasketItemViewModel { BookId = id, Count = 1 };
                    cookieItems.Add(cookieitem);
                    HttpContext.Response.Cookies.Append("Basket",JsonConvert.SerializeObject(cookieItems));

                    return Json(new
                    {
                        length=cookieItems.Count,
                    });
                }
            }
            else
            {
                cookieitem = new BasketItemViewModel {BookId=id,Count=1};
                cookieItems.Add(cookieitem);
                
            }

            
            HttpContext.Response.Cookies.Append("Basket",JsonConvert.SerializeObject(cookieItems) );

            return Json(new
            {
                
                length = cookieItems.Count
            });
            //return RedirectToAction("index","home");
        }
        public IActionResult ShowBasket()
        {
            var basket = new List<BasketItemViewModel>();
            var basketStr = HttpContext.Request.Cookies["Basket"];
            if (basket!=null)
            {

                basket = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketStr);
            }

           
            return Json(new { basket });
        }
        public IActionResult LoadPage()
        {
            var basket = new List<BasketItemViewModel>();
            var basketStr = HttpContext.Request.Cookies["Basket"];
            basket = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketStr);
            return Json(new
            {
                length = basket.Count,
            });


        }
        public IActionResult RemoveBasket(int id)
        {
            var basket = new List<BasketItemViewModel>();
            var basketStr = HttpContext.Request.Cookies["Basket"];
            basket =JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketStr);
            basket.Remove(basket.FirstOrDefault(x => x.BookId == id));

            HttpContext.Response.Cookies.Append("Basket", JsonConvert.SerializeObject(basket));

            return RedirectToAction("showbasket");

         
        }
    }
}
