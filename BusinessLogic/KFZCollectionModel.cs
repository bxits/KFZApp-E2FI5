using BusinessLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using CommonTypes;

namespace BusinessLogic
{
    public class KFZCollectionModel
    {

        public List<KFZModel> KFZListe = new List<KFZModel>();

        public event KFZDataReadyEventHandler KFZDataReady;
        public event BusinessLogic.Events.InfoMessageEventHandler InfoMessage;


        public KFZCollectionModel()
        {



        }

        public void GetAllKFZFromDB()
        {
            //if fehlt noch aus Config-File ODER über using-Direktive...
            //IKFZDataAccess dbAccess = new MariaDBDataAccess();
            //alternativ:
            IKFZDataAccess dbAccess = new PostGreSQLDataAccess();

            if (dbAccess.Connect(""))
            {
                List<KFZ> kfzListe = dbAccess.GetKFZList();

                foreach (var item in kfzListe)
                {
                    //Klassisch, Properties setzen
                    //KFZModel km = new KFZModel();
                    //km.FahrgestellNr = item.FahrgestellNr;

                    //C#-Style Kurzschreibweise Prop setzen
                    KFZModel km2 = new KFZModel()
                    {
                        FahrgestellNr = item.FahrgestellNr,
                        Idkfz = item.Idkfz,
                        IsSelected = false,
                        Kennzeichen = item.Kennzeichen,
                        Leistung = item.Leistung,
                        Typ = item.Typ
                    };
                    this.KFZListe.Add(km2);
                }

                if (KFZDataReady != null)
                    KFZDataReady(KFZListe); //Event feuern.
            }
        }

        public void DeleteKfz(KFZModel kfzm)
        {
            IKFZDataAccess dbAccess = new MariaDBDataAccess();
            if (dbAccess.Connect(""))
            {
                try
                {
                    dbAccess.DeleteKFZ(new KFZ() { Idkfz = kfzm.Idkfz });

                    if (InfoMessage != null)
                    {
                        //KFZ auch noch aus der lokalen Property rauswerfen.
                        if (KFZListe.Remove(kfzm))
                        {
                            //Wenn auch das geklappt hat, Info-Event werfen.
                            InfoMessage($"Das Kfz {kfzm.Kennzeichen} wurde erfolgreich aus der Datenbank gelöscht.");
                        }
                    }
                }
                catch (Exception)
                {
                    if (InfoMessage != null)
                    {
                        InfoMessage($"Das Kfz {kfzm.Kennzeichen} konnte nicht aus der Datenbank gelöscht werden.");
                    }
                }
            }
        }
    }
}
