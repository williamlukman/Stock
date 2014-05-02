using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;
using Warehouse.Repository;

namespace Warehouse.Controllers
{
    public class logController : Controller
    {

        private logRepository _logRepository;
        private warehouseRepository _warehouseRepository;
        private reasonRepository _reasonRepository;
        private itemRepository _itemRepository;

        public logController()
        {
            _logRepository = new logRepository();
            _warehouseRepository = new warehouseRepository();
            _reasonRepository = new reasonRepository();
            _itemRepository = new itemRepository();
        }

        //
        // GET: /Log
        //
        public ActionResult Index()
        {
            List<logModel.logString> lm = _logRepository.getLogs();

            return View(lm);

        }

        //
        // GET: /Log/getAllLogs
        //
        public ActionResult getAllLogs(string _search, long nd, int rows, int? page, string sidx, string sord, string filters = "")
        {
            IEnumerable<logModel.logString> lm = _logRepository.getLogs();

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = lm.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
 
            lm = lm.Skip(pageIndex * pageSize).Take(pageSize);

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from eachlm in lm
                    select new
                    {
                        id = eachlm.historyID,
                        cell = new object[] {
                            eachlm.historyID,
                            eachlm.itemID,
                            eachlm.itemName,
                            eachlm.total,
                            eachlm.description,
                            eachlm.date,
                            eachlm.invoicenumber
                      }
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet); 
            
        }

        //
        // GET: /log/getlogsbyitem?itemid={@itemid}
        //
        public ActionResult getlogsbyitem (string _search, long nd, int rows, int? page, string sidx, string sord, string filters = "", int itemid = 0)
        {

            IEnumerable<logModel.logString> lm = _logRepository.getLogsByItem(itemid);

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = lm.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            
            lm = lm.Skip(pageIndex * pageSize).Take(pageSize);

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from eachlm in lm
                    select new
                    {
                        id = eachlm.historyID,
                        cell = new object[] {
                            eachlm.invoicenumber,
                            eachlm.date,
                            eachlm.total,
                            eachlm.description                            
                      }
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);             
        }

        //
        // GET: /log/getlogsbyname?itemname={@itemname}
        //
        public ActionResult getlogsbyname(string _search, long nd, int rows, int? page, string sidx, string sord, string filters = "", string itemname = "")
        {
            IEnumerable<logModel.logString> lm = _logRepository.getLogsByName(itemname);

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = lm.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            // default last page
            if (totalPages > 0)
            {
                pageIndex = totalPages - 1;
                page = totalPages;
            }

            lm = lm.Skip(pageIndex * pageSize).Take(pageSize);

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from eachlm in lm
                    select new
                    {
                        id = eachlm.historyID,
                        cell = new object[] {
                            eachlm.invoicenumber,
                            eachlm.date,
                            eachlm.total,
                            eachlm.description                            
                      }
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /log/getlastlog
        //
        public ActionResult getlastlog()
        {
            logModel.logString lm = _logRepository.getLastLog();

            return Json(new
            {
                lm
            }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Log/insertlog?itemID={@itemID}&itemName={@itemName}&totalnumber={@totalnumber}&description={@reason.description}
        //

        [HttpPost]
        public ActionResult insertLog(int itemID, int totalnumber, int reasonID)
        {
            String description = _reasonRepository.getDescription(reasonID);
            String itemName = _itemRepository.GetItemName(itemID);

            log newlog = _logRepository.insertLog(itemID, totalnumber, description);
            warehouse newwarehouse = _warehouseRepository.UpdateWarehouse(itemID, totalnumber, description);
            logModel.logString lm = logModel.ToLogString(newlog, itemName);
            warehouseModel wm = warehouseModel.ToWarehouseModel(newwarehouse, itemName);

            return Json(new
            {
                lm
            });
        }
    }
}
