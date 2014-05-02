using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;
using Warehouse.Repository;

namespace Warehouse.Repository
{
    public class warehouseRepository : EfRepository<warehouse>, IRepository<warehouse>
    {

        private logRepository _logRepository;
        private reasonRepository _reasonRepository;
        private itemRepository _itemRepository;

        public warehouseRepository()
        {
            _logRepository = new logRepository();
            _reasonRepository = new reasonRepository();
            _itemRepository = new itemRepository();
        }

        public List<warehouseModel> filterWarehouse(string itemName)
        {
            using (var db = new WarehouseEntities())
            {
                int itemid = (from i in db.items where i.itemName == itemName select i.itemID).FirstOrDefault();
                List<warehouseModel> list = (from w in db.warehouses
                                        where w.itemID == itemid
                                        join i in db.items on w.itemID equals i.itemID
                                        select new warehouseModel { itemID = w.itemID, itemName = i.itemName, totalstock = w.totalstock}).ToList();

                return list;
            }
        }


        /// <summary>
        /// The main function that runs the update of log, item, and warehouse.
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="totalnumber"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public warehouse UpdateWarehouse(int itemID, int totalnumber, string description)
        {
            using (var db = new WarehouseEntities())
            {
                item existingitem = (from i in db.items where i.itemID == itemID select i).FirstOrDefault();
                
                if (existingitem != null)
                {   // if the item is already registered
                    
                    warehouse oldwarehouse = Find(w => w.itemID == itemID);
                    if (oldwarehouse != null)
                    {
                        int totalstock = oldwarehouse.totalstock;
                        totalstock = _reasonRepository.calculate(description) > 0 ? totalstock + totalnumber : totalstock - totalnumber;
                        oldwarehouse.totalstock = totalstock;
                        Update(oldwarehouse);
                        return oldwarehouse;
                    }
                    else
                    {

                        warehouse newWarehouse = new warehouse();
                        newWarehouse.itemID = existingitem.itemID;
                        newWarehouse.totalstock = _reasonRepository.calculate(description) > 0 ? totalnumber : (-1)*totalnumber;

                        return Create(newWarehouse);
                    }
                }

                return null;
            }
        }
        
        public List<warehouseModel> viewWarehouse()
        {
            using (var db = new WarehouseEntities())
            {
                List<warehouseModel> list = (from w in db.warehouses
                                              join i in db.items on w.itemID equals i.itemID
                                              select new warehouseModel {
                                               itemID = w.itemID,
                                               itemName = i.itemName,
                                               totalstock = w.totalstock
                                              }).ToList();
                return list;
            }
        }
    }
}
