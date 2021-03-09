using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Mk.DBConnector;

namespace BusinessLogic
{
    public static class Connection
    {
        static DBAdapter Adapter;
        static Connection()
        {
            Connection.Adapter = new DBAdapter(DatabaseType.MySql,
                Instance.NewInstance, "localhost", 3306, "kfzka", "ukfz", "ukfz", "logdatei.log");
        }

        public static List<KFZModel> GetKFZList()
        {
            List<KFZModel> ltemp = new List<KFZModel>();

            string sql = string.Format("SELECT * FROM kfz;");

            DataTable t = Adapter.Adapter.GetDataTable(sql);

            foreach(DataRow r in t.Rows)
            {
                KFZModel k = new KFZModel();
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
