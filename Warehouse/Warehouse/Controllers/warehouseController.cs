using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;
using Warehouse.Repository;

namespace Warehouse.Controllers
{
    public class warehouseController : Controller
    {
        private warehouseRepository _warehouseRepository;
     
        public warehouseController()
        {
            _warehouseRepository = new warehouseRepository();
        }

        public ActionResult Index()
        {
            List<warehouseModel> wm = _warehouseRepository.viewWarehouse();

            return View(wm);
        }
        //
        // GET: /warehouse/GetAllWarehouse()
        //
        public ActionResult GetAllWarehouse(string _search, long nd, int rows, int? page, string sidx, string sord, string filters = "")
        {
            IEnumerable<warehouseModel> wm = _warehouseRepository.viewWarehouse();

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = wm.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            wm = wm.Skip(pageIndex * pageSize).Take(pageSize);

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from eachwm in wm
                    select new
                    {
                        id = eachwm.itemName,
                        cell = new object[] {
                            eachwm.itemID,
                            eachwm.itemName,
                            eachwm.totalstock
                      }
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet); 

        }

        public ActionResult filter(string itemName)
        {
            List<warehouseModel> wm = _warehouseRepository.filterWarehouse(itemName);
            
            return Json(new
            {
                wm
            }, JsonRequestBehavior.AllowGet);

        }
    }
}
