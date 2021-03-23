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
        public event InfoMessageEventHandler InfoMessage; 


        public KFZCollectionModel()
        {



        }

        public void GetAllKFZFromDB()
        {
            //if fehlt noch aus Config-File
            IKFZDataAccess dbAccess = new MariaDBDataAccess();
            //IKFZDataAccess dbAccess = new PostGreSQLDataAccess();


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

        public void DeleteKfz(KFZModel kfzm)
        {
            bool kfzDeleted = false;

            kfzDeleted = Connection.DeleteKfzById(kfzm.Idkfz);

            if(kfzDeleted == true && InfoMessage != null)
            {
                if (KFZListe.Remove(kfzm))
                {
                    InfoMessage($"Das Kfz {kfzm.Kennzeichen} wurde erfolgreich aus der Datenbank gelöscht.");
                }
            }


        }
    }
}
