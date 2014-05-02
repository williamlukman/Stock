using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;

namespace Warehouse.Repository
{
    public class logRepository : EfRepository<log>, IRepository<log>
    {

        public List<logModel.logString> getLogs()
        {
            logModel lm = new logModel();

            using (var db = new WarehouseEntities())
            {
                
                List<logModel.logDate> logs = (from l in db.logs
                             join i in db.items on l.itemID equals i.itemID
                             select new logModel.logDate
                             {
                                historyID = l.historyID,
                                itemID = l.itemID,
                                itemName = i.itemName,
                                total = l.total,
                                description = l.description,
                                date = l.date,
                                invoicenumber = l.invoicenumber
                             }).ToList();
                
                List<logModel.logString> lms = (from l in logs
                                      select new logModel.logString
                                        {
                                        historyID = l.historyID,
                                        itemID = l.itemID,
                                        itemName = l.itemName,
                                        total = l.total,
                                        description = l.description,
                                        date = l.date.ToString("yyyyMMdd"),
                                        invoicenumber = l.invoicenumber
                                        }).ToList();
                return lms;
            }
        }

        public List<logModel.logString> getLogsByItem(int itemID)
        {
            using (var db = new WarehouseEntities())
            {
                List<logModel.logDate> logs =
                    (from l in db.logs
                           where l.itemID == itemID
                           join i in db.items on l.itemID equals i.itemID
                           select new logModel.logDate
                           {
                               historyID = l.historyID,
                               itemID = l.itemID,
                               itemName = i.itemName,
                               total = l.total,
                               description = l.description,
                               date = l.date,
                               invoicenumber = l.invoicenumber
                           }).ToList();

                List<logModel.logString> lms = (from l in logs
                                      select new logModel.logString
                                      {
                                        historyID = l.historyID,
                                        itemID = l.itemID,
                                        itemName = l.itemName,
                                        total = l.total,
                                        description = l.description,
                                        date = l.date.ToString("yyyyMMdd"),
                                        invoicenumber = l.invoicenumber
                                      }).ToList();
                return lms;
            }
        }

        public List<logModel.logString> getLogsByName(string itemName)
        {
            
            using (var db = new WarehouseEntities())
            {
                List<logModel.logDate> logs = (from i in db.items
                                               where i.itemName == itemName
                                               join l in db.logs on i.itemID equals l.itemID
                                               select new logModel.logDate
                                               {
                                                   historyID = l.historyID,
                                                   itemID = l.itemID,
                                                   itemName = itemName,
                                                   total = l.total,
                                                   description = l.description,
                                                   date = l.date,
                                                   invoicenumber = l.invoicenumber
                                               }).ToList();

                List<logModel.logString> lms = (from l in logs.AsEnumerable()
                                      select new logModel.logString
                                      {
                                          historyID = l.historyID,
                                          itemID = l.itemID,
                                          itemName = l.itemName,
                                          total = l.total,
                                          description = l.description,
                                          date = l.date.ToString("yyyyMMdd"),
                                          invoicenumber = l.invoicenumber
                                      }).ToList();
                return lms;
            }
        }
        public logModel.logString getLastLog()
        {
            using (var db = new WarehouseEntities())
            {
                List<logModel.logDate> lastlog = (from l in db.logs
                                     orderby l.historyID descending
                                     join i in db.items on l.itemID equals i.itemID
                                     select new logModel.logDate
                                     {
                                         historyID = l.historyID,
                                         itemID = l.itemID,
                                         itemName = i.itemName,
                                         total = l.total,
                                         description = l.description,
                                         date = l.date,
                                         invoicenumber = l.invoicenumber
                                     }).ToList();

                logModel.logString ll = (from l in lastlog
                                      select new logModel.logString
                                      {
                                          historyID = l.historyID,
                                          itemID = l.itemID,
                                          itemName = l.itemName,
                                          total = l.total,
                                          description = l.description,
                                          date = l.date.ToString("yyyyMMdd"),
                                          invoicenumber = l.invoicenumber
                                      }).First();
                return ll;
            }
        }

        public log insertLog (int itemID, int totalnumber, string description)
        {
            log newlog = new log();
            newlog.itemID = itemID;
            newlog.total = totalnumber;
            newlog.description = description;
            newlog.date = DateTime.Today;
            newlog.invoicenumber = DateTime.Today.ToString("yyyyMMdd") + todigit(itemID, 5) + todigit(totalnumber, 5);
            return Create(newlog);
        }

        public string todigit (int input, int numberofdigit)
        {
            string digit = input.ToString();
            string zeroes = "";
          
            int inputdigit;
            // ignore and just print value if inputdigit > 5
            if (input >= 10000) inputdigit = 5;
            else if (input >= 1000) inputdigit = 4;
            else if (input >= 100) inputdigit = 3;
            else if (input >= 10) inputdigit = 2;
            else if (input >= 1) inputdigit = 1;
            else inputdigit = 0;

            for (int i = 0; i < numberofdigit - inputdigit; i++)
            {
                zeroes = String.Concat(zeroes, "0");
            }
            return String.Concat(zeroes, input);
        }
    }
}
