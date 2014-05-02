using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Warehouse.Models
{
    public class itemModel
    {
        public int itemID { get; set; }
        public string itemName { get; set; }

        public static itemModel ToModel(item i)
        {
            return new itemModel
            {
                itemID = i.itemID,
                itemName = i.itemName
            };
        }
    }
}
