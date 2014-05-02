using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;
using Warehouse.Repository;

namespace Warehouse.Controllers
{
    public class reasonController : Controller
    {
        private reasonRepository _reasonRepository;
        
        public reasonController()
        {
            _reasonRepository = new reasonRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /reason/GetAllReasons
        //
        public ActionResult GetAllReasons(string _search, long nd, int rows, int? page, string sidx, string sord, string filters = "")
        {
            IEnumerable<reasonModel> rm = _reasonRepository.getAllReasons();

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = rm.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            // default last page
            if (totalPages > 0)
            {
                pageIndex = totalPages - 1;
                page = totalPages;
            }

            rm = rm.Skip(pageIndex * pageSize).Take(pageSize);

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from eachrm in rm
                    select new
                    {
                        id = eachrm.reasonID,
                        cell = new object[] {
                            eachrm.reasonID,
                            eachrm.description
                      }
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /reason/reasonComboBoxSelection
        //
        public ActionResult reasonComboBoxSelection()
        {
            IEnumerable<reasonModel> rm = _reasonRepository.getAllReasons();

            var reasonmodel = (from eachrm in rm
                    select new
                    {
                        reasonID = eachrm.reasonID,
                        description = eachrm.description
                    }).ToList();

        return Json(reasonmodel, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /reason/getid?description={@description}
        //
        public ActionResult getid(String description)
        {
            int id = _reasonRepository.getID(description);

            return Json(new
            {
                id
            }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /reason/getsign?description={@description}
        //
        public ActionResult getsign(String description)
        {
            Int32 sign = _reasonRepository.calculate(description);

            string signstring = (sign == 1) ? "++" : (sign == -1) ? "--" : "Unknown reason: -- " + description + " -- is not registered in the database";

            return Json(new
            {
                signstring
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
