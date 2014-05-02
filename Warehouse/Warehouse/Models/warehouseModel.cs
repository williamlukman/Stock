using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Warehouse.Models
{
    public class warehouseModel
    {
        public int itemID {get; set;}
        public int totalstock { get; set; }
        public string itemName { get; set; }  // Additional column     

        public static warehouseModel ToWarehouseModel(warehouse w, string itemname)
        {
            return new warehouseModel
            {
                itemID = w.itemID,
                totalstock = w.totalstock,
                itemName = itemname
            };
        }
    }
}
