using MVC5Homework1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Homework1.Controllers
{
    public class CustDataManageController : Controller
    {
        客戶資料Entities db = new 客戶資料Entities();
        // GET: CustDataManage
        public ActionResult Index()
        {
            var all = db.客戶資料.AsQueryable();
            
            return View();
        }
    }
}