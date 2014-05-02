using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;
using Warehouse.Repository;

/*
 *  implement Model Warehouse
 *            public int itemID
 *            public int totalstock
 *            public virtual item item
 */

namespace Warehouse.Controllers
{
    public class itemController : Controller
    {

        private itemRepository _itemRepository;

        public itemController()
        {
            _itemRepository = new itemRepository(); 
        }

        //
        // GET: /item/

        public ViewResult Index()
        {
            return View();
        }

        //
        // POST: /item/Create

        [HttpPost]
        public ActionResult Create(itemModel item)
        {
            if (ModelState.IsValid)
            {
                _itemRepository.createItem(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        //
        // POST: /items/Edit/5

        [HttpPost]
        public ActionResult Edit(itemModel im)
        {
            if (ModelState.IsValid)
            {
                _itemRepository.editItem(im);
                return RedirectToAction("Index");
            }
            return View(im);
        }

        //
        // GET: /items/Delete/5

        public ActionResult Delete(int id)
        {
            _itemRepository.deleteItem(id);
            return RedirectToAction("Index");
        }

        //
        // POST: /items/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _itemRepository.deleteItem(id);
            return RedirectToAction("Index");
        }

        //
        // GET: /item/AllItems
        //
        public ActionResult AllItems(string _search, long nd, int rows, int? page, string sidx, string sord, string filters = "")
        {
            IEnumerable<itemModel> im = _itemRepository.GetAllItems();

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = im.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            im = im.Skip(pageIndex * pageSize).Take(pageSize);

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from eachim in im
                    select new
                    {
                        id = eachim.itemID,
                        cell = new object[] {
                            eachim.itemID,
                            eachim.itemName
                      }
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult itemComboBoxSelection()
        {
            IEnumerable<itemModel> im = _itemRepository.GetAllItems();
            
            var itemmodel = (from eachim in im
                             orderby eachim.itemName ascending
                             select new
                             {
                                itemID = eachim.itemID,
                                itemName = eachim.itemName
                             }).ToList();
            return Json(itemmodel, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /item/details?itemid={itemid}
        //
        public ActionResult getItem(int itemid)
        {
            itemModel im = _itemRepository.GetItem(itemid);

            return Json(new
            {
                im
            }, JsonRequestBehavior.AllowGet);
        }

//
        // POST: /item/createItem?itemName={@itemName}
        //
        [HttpPost]
        public ActionResult createItem(string itemName)
        {
            item i = _itemRepository.createItemByName(itemName);

            itemModel im = itemModel.ToModel(i);
            return Json(new
            {
                im
            });
        }
    }
}
