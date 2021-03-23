using Mk.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using CommonTypes;

namespace DataAccess
{
    public static class Connection
    {
        static DBAdapter Adapter;
        static Connection()
        {
            Connection.Adapter = new DBAdapter(DatabaseType.MySql,
                Instance.NewInstance, "localhost", 3306, "kfzka", "ukfz", "ukfz", "logdatei.log");
        }

        public static List<KFZ> GetKFZList()
        {
            List<KFZ> ltemp = new List<KFZ>();

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

        public static bool DeleteKfzById(long kfzId)
        {
            bool success = false;
            string sql = string.Format($"DELETE FROM kfz WHERE idkfz={kfzId};");

            Adapter.Adapter.ExecuteSQL(sql);
            


            success = Convert.ToBoolean(Adapter.Adapter.ExecuteSQL(sql));
            return success;
        }

    }
}
