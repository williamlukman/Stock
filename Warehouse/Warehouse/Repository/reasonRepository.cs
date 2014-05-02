using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;

namespace Warehouse.Repository
{
    public class reasonRepository : EfRepository<reason>, IRepository<reason>
    {
        //
        // GET: /reasonRepository/

        /// <summary>
        /// Get All Descriptions in the Reason table
        /// </summary>
        /// <returns>List<Strings> Reason</returns>
        public List<reasonModel> getAllReasons()
        {
            using (var db = new WarehouseEntities())
            {
                var reasons = (from r in db.reasons select new reasonModel { reasonID = r.reasonID, description = r.description}).ToList();
                return reasons;
            }
        }

        /// <summary>
        /// Get reasonID from Reason table given the string Reason
        /// </summary>
        /// <param name="Reason"></param>
        /// <returns>int reasonID</returns>
        /// 

        public int getID(string Reason)
        {
            using (var db = new WarehouseEntities())
            {
                var reasonID = (from r in db.reasons where r.description == Reason select r.reasonID).FirstOrDefault();
                return reasonID;
            }
        }

        public string getDescription(int id)
        {
            using (var db = new WarehouseEntities())
            {
                var description = (from r in db.reasons where r.reasonID == id select r.description).FirstOrDefault();
                return description;
            }
        }
        
        /// <summary>
        /// This function allows users to know whether or not certain reasons has positive or negative influence
        /// on the total stock of the warehouse
        /// </summary>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public Int32 calculate(string Reason)
        {
            Int32 sign = 0;
            switch(Reason) {
                case ("Sales"): 
                    sign = -1;
                    break;
                case ("Retur Rusak"):
                    sign = -1;
                    break;
                case ("Return to Supplier"):
                    sign = -1;
                    break;
                case ("Receive from Supplier"):
                    sign = 1;
                    break;
                case ("Retur Baik"):
                    sign = 1;
                    break;
                case ("Purchase"):
                    sign = 1;
                    break;
            }
            return sign;
        }
    }
}
