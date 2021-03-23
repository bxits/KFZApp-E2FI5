using CommonTypes;
using Mk.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MariaDBDataAccess : IKFZDataAccess
    {
        DBAdapter Adapter;

        public bool Connect(string conString)
        {
            bool success = false;
            //DB-Verbindung aufbauen.


            return success;
        }

        //CRUD
        public List<KFZ> GetKFZList()
        {
            List<KFZ> ltemp = new List<KFZ>();

            //Maria-DB
            string sql = string.Format("SELECT * FROM kfz;");

            DataTable t = Adapter.Adapter.GetDataTable(sql);

            foreach (DataRow r in t.Rows)
            {
                KFZ k = new KFZ();
                k.Idkfz = long.Parse(r[0].ToString());
                k.FahrgestellNr = r[1].ToString();
                k.Kennzeichen = r[2].ToString();
                k.Leistung = Convert.ToInt32(r[3].ToString());
                k.Typ = r[4].ToString();

                ltemp.Add(k);
            }


            return ltemp;
        }
    }
}
