using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using log4net;

using Model;
using BLL;
using Utls;

namespace Portal.Controllers
{
    public class memberController : Controller
    {
        //
        // GET: /login/
        public ActionResult login()
        {
            return View();
        }

        
    }
}
