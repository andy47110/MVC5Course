using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    //[Authorize] //ActionBuilder??? 下禮拜會講 20170507
    public class EFController : Controller
    {

        FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index()
        {
            //延遲載入:
            //AsQueryable:最後執行時才查詢(延遲載入)
            //AsEnumerable: 直接查詢，再WHERE...等動作
            //下方代表只是一個查詢物件
            var all = db.Product.AsQueryable();
            //也可轉成AsEnumerable 但會在這邊就把DB物件全部取回來
            //var allEnum = db.Product.AsEnumerable();

            //不同的API傳入的資料，會依據API不同而產生不同的型別
            var one = db.Product.Find(1);
            var two = db.Product.FirstOrDefault(x => x.ProductId == 1);
            var data = all.Where(x => 
                    x.Active == true && x.ProductName.Contains("Black")
                    );

            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id,Product product)
        {
            if(ModelState.IsValid)
            {
                var item = db.Product.Find(product);
                item.ProductId = product.ProductId;
                item.ProductName = product.ProductName;
                item.Stock = product.Stock;
                item.Active = product.Active;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        
        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);

            //edmx中的導覽屬性:取出Product關聯的所以資料

            ////方法一
            //foreach(var item in product.OrderLine.ToList())
            //{
            //    //使用導覽屬性將所有關聯資料刪除
            //    db.OrderLine.Remove(item);
            //    //*不建議在此進行saveChanges
            //    //建議在所有動作結束後再進行saveChanges
            //}

            //方法二
            db.OrderLine.RemoveRange(product.OrderLine);

            //如資料為關聯式資料時，下行方法將無法只刪除單一table中的資料
            db.Product.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}