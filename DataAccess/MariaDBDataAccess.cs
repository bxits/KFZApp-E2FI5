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
        private DBAdapter _adapter;

        public bool IsConnected { get; private set; }

        public event ErrorMessageEventHandler ErrorMessage;
        public event InfoMessageEventHandler InfoMessage;

        public bool Connect(string conString) //Connection-String noch verwenden.
        {
            bool success = false;
            //DB-Verbindung aufbauen.
            try
            {
                _adapter = new DBAdapter(DatabaseType.MySql,
                    Instance.NewInstance, "localhost", 3306, "kfzka", "ukfz", "ukfz", "logdatei.log");
                _adapter.Adapter.LogFile = true;
                IsConnected = true;
            }
            catch (Exception)
            {

                IsConnected = false;
            }

            return success;
        }

        public void DeleteKFZ(KFZ kfz)
        {
            //Maria-DB
            string del_sql = string.Format($"DELETE FROM kfz WHERE idkfz = {kfz.Idkfz};");
            _adapter.Adapter.ExecuteSQL(del_sql);
            _adapter.Adapter.WriteLogFile($"KFZ {kfz.Idkfz}, {kfz.Kennzeichen} erfolgreich gelöscht.");

            //Info, wen es interessiert.
            InfoMessage?.Invoke($"KFZ {kfz.Idkfz}, {kfz.Kennzeichen} erfolgreich gelöscht.");
        }

        //CRUD
        public List<KFZ> GetKFZList()
        {
            List<KFZ> ltemp = new List<KFZ>();

            //Maria-DB
            string sql = string.Format("SELECT * FROM kfz;");
            try
            {
                DataTable t = _adapter.Adapter.GetDataTable(sql);

                foreach (DataRow r in t.Rows)
                {
                    KFZ k = new KFZ();
                    k.Idkfz = long.Parse(r["idkfz"]?.ToString());
                    k.FahrgestellNr = r[1]?.ToString();
                    k.Kennzeichen = r[2]?.ToString();
                    k.Leistung = Convert.ToInt32(r[3]?.ToString());
                    k.Typ = r[4]?.ToString();

                    ltemp.Add(k);
                }
            }
            catch (Exception ex)
            {
                if (ErrorMessage != null)
                    ErrorMessage("Fehler in GetKFZList: " + ex.Message);
            }

            return ltemp;
        }

        public void InsertNewKFZ(KFZ newKfz)
        {
            throw new NotImplementedException();
        }

        public void UpdateKFZ(KFZ kfz)
        {
            throw new NotImplementedException();
        }
    }
}
