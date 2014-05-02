using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;

namespace Warehouse.Repository
{
    public class itemRepository : EfRepository<item>, IRepository<item>
    {

        //
        // GET: /baseItem/GetItem/{itemID}
        //
        public itemModel GetItem(int itemID)
        {
            using (var db = new WarehouseEntities())
            {
                itemModel item = (from i in db.items where i.itemID == itemID select new itemModel { itemID = i.itemID, itemName = i.itemName }).FirstOrDefault();
                return item;
            }
        }

        //
        // GET: /baseItem/GetItemName/{itemID}
        //
        public string GetItemName(int itemID)
        {
            using (var db = new WarehouseEntities())
            {
                string itemName = (from i in db.items where i.itemID == itemID select i.itemName).FirstOrDefault();
                return itemName;
            }
        }

        public List<itemModel> GetAllItems()
        {
            using (var db = new WarehouseEntities())
            {
                List<itemModel> items = (from i in db.items select new itemModel { itemID = i.itemID, itemName = i.itemName }).ToList();
                return items;
            }
        }

        public item createItem(itemModel im)
        {
            item newitem = new item();
            newitem.itemName = im.itemName;
            return Create(newitem);
        }

        public item createItemByName (string itemName)
        {
            item newitem = new item();
            newitem.itemName = itemName;
            return Create(newitem);
        }

        public item editItem(itemModel im)
        {
            item item = Find(i => i.itemID == im.itemID);
            if (item != null)
            {
                item.itemName = im.itemName;
                Update(item);
            }
            return item;
        }

        public void deleteItem(int itemid)
        {
            item item = Find(i => i.itemID == itemid);
            if (item != null)
            {
                Delete(item);
            }
        }
    }
}
