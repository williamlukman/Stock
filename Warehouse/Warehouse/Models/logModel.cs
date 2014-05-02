using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Warehouse.Models
{
    public class logModel
    {
        public class logString
        {
            public int historyID { get; set; }
            public int itemID { get; set; }
            public string itemName { get; set; } // Additional column 
            public int total { get; set; }
            public string description { get; set; }
            public string invoicenumber { get; set; }
            public string date { get; set; }
        }
        public class logDate
        {
            public int historyID { get; set; }
            public int itemID { get; set; }
            public string itemName { get; set; } // Additional column 
            public int total { get; set; }
            public string description { get; set; }
            public string invoicenumber { get; set; }
            public DateTime date { get; set; }
        }
        public static logString ToLogString(log l, string itemname)
        {
            return new logString
            {
                historyID = l.historyID,
                itemID = l.itemID,
                itemName = itemname,
                total = l.total,
                description = l.description,
                date = l.date.ToString("yyyyMMdd"),
                invoicenumber = l.invoicenumber
            };
        }

        public static logDate ToLogDate(log l, string itemname)
        {
            return new logDate
            {
                historyID = l.historyID,
                itemID = l.itemID,
                itemName = itemname,
                total = l.total,
                description = l.description,
                date = l.date,
                invoicenumber = l.invoicenumber
            };
        }
    }
}
