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
            try
            {
                //Maria-DB
                string del_sql = string.Format($"DELETE FROM kfz WHERE idkfz = {kfz.Idkfz};");
                _adapter.Adapter.ExecuteSQL(del_sql);
                _adapter.Adapter.WriteLogFile($"KFZ {kfz.Idkfz}, {kfz.Kennzeichen} erfolgreich gelöscht.");
            }
            catch (Exception ex)
            {
                ErrorMessage?.Invoke($"Fehler in DeleteKFZ, {ex.Message}");
            }
            //Info, wen es interessiert.
            InfoMessage?.Invoke($"KFZ {kfz.Idkfz}, {kfz.Kennzeichen} erfolgreich gelöscht.");
        }

        //CRUD
        public List<KFZ> GetKFZList()
        {
            List<KFZ> ltemp = new List<KFZ>();

            //Maria-DB
            //string sql = string.Format("SELECT * FROM kfz LEFT JOIN ;");
            string sql = Resource.MariaSelectAllKFZs;
            try
            {
                DataTable t = _adapter.Adapter.GetDataTable(sql);

                foreach (DataRow r in t.Rows)
                {
                    KFZ k = new KFZ();
                    k.Idkfz = long.Parse(r["idkfz"]?.ToString());
                    k.FahrgestellNr = r["FahrgestellNr"]?.ToString();
                    k.Kennzeichen = r["kennzeichen"]?.ToString();
                    k.Leistung = Convert.ToInt32(r["leistung"]?.ToString());
                    k.Typ = r["typ"]?.ToString();
                    k.TypID = Convert.ToInt32(r["kfztyp_idkfztyp"]);

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
            try
            {
                _adapter.Adapter.ExecuteParameters(
                    Resource.MariaInsertKFZ,
                    new List<Parameter>() {
                        new Parameter() { Name = "leistung", Value=newKfz.Leistung},
                        new Parameter() {Name = "FahrgestellNr", Value=newKfz.FahrgestellNr},
                        new Parameter() {Name = "kennzeichen", Value = newKfz.Kennzeichen},
                        new Parameter() {Name = "kfztyp_idkfztyp", Value = newKfz.TypID}
                    }
                    );
            }
            catch (Exception ex)
            {
                if (ErrorMessage != null)
                    ErrorMessage("Fehler in InsertNewKFZ: " + ex.Message);
            }
        }

        public void UpdateKFZ(KFZ kfz)
        {
            try
            {
                //_adapter.Adapter.
            }
            catch (Exception ex)
            {
                ErrorMessage?.Invoke($"Fehler in UpdateKFZ: {ex.Message}");
            }
        }
    }
}
