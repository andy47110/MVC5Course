using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModel;
using System.Data.Entity.Infrastructure;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //註解ED方法
        //private FabricsEntities db = new FabricsEntities();

         //改為由Repository透過UnitofWork再到EntityFramwork
        // var repo = new ProductRepository();
        //repo.UnitOfWork = new EFUnitOfWork();

        ProductRepository repo = RepositoryHelper.GetProductRepository();
        // GET: Products
        
            [OutputCache(Duration = 300,Location =System.Web.UI.OutputCacheLocation.ServerAndClient)]
        //Model Binding 練習
        public ActionResult Index(bool Active = true)
        {
            //方法一
            //下where條件
            //var data = db.Product.OrderByDescending(x => x.ProductId).Take(10);
            //var  data = db.Product.Where(p => p.Active.HasValue && p.Active.Value == Active);

            //var data = db.All()
            //                .Where(p => p.Active.HasValue && p.Active.Value == Active)
            //                .OrderByDescending(p => p.ProductId).Take(10);

            //方法二
            //將商業邏輯封裝到Repository中
            var data = repo.GetProduct列表頁所有資料(Active, showAll: false);

            //return View(data);

            ViewData.Model = data;
           
           ViewData["ppp"] = data;
            //弱型別的寫法
            ViewBag.qqq = data;
           
           return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //註解ED的東西
            //Product product = db.Product.Find(id);
            //改用Repository
            //Product product = repo.All()
            //    .FirstOrDefault(x=>x.ProductId==id.Value);

            Product product = repo.Get單筆資料ByPrdouctId(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            // 故意註解 產生sql Exception 測試Error ActionFilter
            //if (ModelState.IsValid)
            {
                //註解ED方法
                ////寫入DB動作並儲存
                //db.Product.Add(product);
                //db.SaveChanges();
                ////顯示錯誤訊息可使用tempData ex:SQL exception回傳
                //TempData["Msg"] = "12345";

                //使用Repositroy
                repo.Add(product);
                repo.UnitOfWork.Commit();
                
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByPrdouctId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        //[Bind(Include =  "ProductId,ProductName,Price,Active,Stock")] Product product
        {
            //改用模型繫結延遲驗證，到 Action 裡才執行模型繫結與模型驗證工作 Start
            var product = repo.Get單筆資料ByPrdouctId(id);

            if (TryUpdateModel(product,
                new string[] { "ProductId","ProductName","Price","Active","Stock"}))
            {
                //在這邊就已經被Update了，因此直接Commit
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            //改用模型繫結延遲驗證，到 Action 裡才執行模型繫結與模型驗證工作 End

            //if (ModelState.IsValid)
            //{
            //    //註解ED方法
            //    //db.Entry(product).State = EntityState.Modified;
            //    //db.SaveChanges();

            //    //改使用Repository
            //    repo.Update(product);
            //    repo.UnitOfWork.Commit();

            //    return RedirectToAction("Index");
            //}
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //註解ED方法
            //Product product = db.Product.Find(id);

            //改使用Repository
            Product product = repo.Get單筆資料ByPrdouctId(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //註解ED方法
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();

            //改使用Repository
            Product product = repo.Get單筆資料ByPrdouctId(id);
            //強迫關閉驗證 ValidateOnSaveEnabled = false
            repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            repo.Delete(product);

            //多個Repository 分別為實作兩個不同的Entity
            //故repoOrderLines需要加入到repo的UnitOfWork
            var repoOrderLines = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);
            foreach(var item in product.OrderLine)
            {
                repoOrderLines.Delete(item);
            }
            //repoOrderLines.UnitOfWork.Commit();  已加入repo，故不需要用
            repo.UnitOfWork.Commit();

            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListProducts(ProductSearchVM searchCondition)
        {
            //註解ED方法
            //var data = db.Product
            //    .Where(p => p.Active == true)
            //    .Select(p => new ProductLiteVM()
            //    {
            //        ProductId = p.ProductId,
            //        ProductName = p.ProductName,
            //        Price = p.Price,
            //        Stock = p.Stock
            //    })
            //    .OrderByDescending(p => p.ProductId)
            //    .Take(10);

            //改使用Repository
            //IQueryable<Product> product = repo.GetProduct列表頁所有資料(true,showAll:true);

            if (searchCondition == null)
            {
                ViewBag.searchCondition = new ProductSearchVM()
                {
                    vstockCntStart = 0,
                    vstockCntEnd = 9999
                };
            }

            //強型別做法 Start *********************************************************
            GetProductListBySearch(searchCondition);
            //強型別做法 End *********************************************************

            return View();
        }

        [HttpPost]
        public ActionResult ListProducts(ProductSearchVM searchCondition, ProductBatchUpdate[] items)
        {
            //示範加上 prefix 的 TryUpdateModel 用法
            // TryUpdateModel(searchCondition, "searchCondition")
            if (ModelState.IsValid)
            {
                foreach(var item in items)
                {
                    var prod = db.Product.Find(item.ProductId);
                    prod.Price = item.Price;
                    prod.Stock = item.Stock;
                }

                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();

                return RedirectToAction("ListProducts", searchCondition);
            }

            GetProductListBySearch(searchCondition);

            return View();
        }
        private void GetProductListBySearch(ProductSearchVM searchCondition)
        {
            var data = repo.GetProduct列表頁所有資料(true);

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(searchCondition.vproductName))
                {
                    data = data.Where(x => x.ProductName.Contains(searchCondition.vproductName));
                }

                data = data.Where(x => x.Stock > searchCondition.vstockCntStart && x.Stock < searchCondition.vstockCntEnd);

                ViewData.Model = data
                    .Select(X => new ProductLiteVM()
                    {
                        ProductId = X.ProductId,
                        ProductName = X.ProductName,
                        Price = X.Price,
                        Stock = X.Stock
                    })
                .OrderByDescending(x => x.ProductId);
            }

            ViewBag.searchCondition = searchCondition;
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        //public ActionResult CreateProduct(ProductLiteVM data)
        [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error_DbUpdateException")]
        public ActionResult CreateProduct([Bind(Include ="ProductName,Price,Stock")] ProductLiteVM product)
        {
            if (ModelState.IsValid ==true)
            {
                TempData["CreateProduct_Result"] = "商品新增成功";
                //儲存資料進資料庫
                return RedirectToAction("ListProducts");
            }
            //驗證失敗,繼續顯示原本的表單
            return View();
        }
    }
}
