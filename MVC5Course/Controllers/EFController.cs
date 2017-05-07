using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

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
                    //方法三
                    x.Is刪除 == false &&
                    x.Active == true && x.ProductName.Contains("Black") 
                    )
                .OrderByDescending(x => x.ProductId);
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
                var item = db.Product.Find(id);
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
            //db.OrderLine.RemoveRange(product.OrderLine);

            //方法三
            try
            {       
                product.Is刪除 = true;
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            //如資料為關聯式資料時，下行方法將無法只刪除單一table中的資料
            //db.Product.Remove(product);
           
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = db.Database.SqlQuery<Product>(
                "SELECT * FROM dbo.Product WHERE ProductId=@p0", id).FirstOrDefault();
            //p0 p1 p2 p3 p4........


            return View(data);
        }

        public ActionResult DeletedDetail()
        {
            var all = db.Product.AsQueryable();
            //延遲載入:
            //AsQueryable:最後執行時才查詢(延遲載入)
            //AsEnumerable: 直接查詢，再WHERE...等動作
            //下方代表只是一個查詢物件
            //也可轉成AsEnumerable 但會在這邊就把DB物件全部取回來
            //var allEnum = db.Product.AsEnumerable();

            //不同的API傳入的資料，會依據API不同而產生不同的型別
            var data = all.Where(x => x.Is刪除 == true && x.ProductName.Contains("Black"))
                .OrderByDescending(x => x.ProductId);

            return View(data);
        }

        public ActionResult RestoreData(int id )
        {
            var product = db.Product.Find(id);

            try
            {
                product.Is刪除 = false;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}