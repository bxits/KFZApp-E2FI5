using BusinessLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using CommonTypes;
using System.ComponentModel;

namespace BusinessLogic
{
    public class KFZCollectionModel
    {
        private BackgroundWorker _bwThread;

        public List<KFZModel> KFZListe = new List<KFZModel>();
        public event KFZDataReadyEventHandler KFZDataReady;
        public event BusinessLogic.Events.InfoMessageEventHandler InfoMessage;
        public event KFZStateChangedEventHandler KFZStateChanged;

        public KFZCollectionModel()
        {
            _bwThread = new BackgroundWorker();
            _bwThread.DoWork += ThreadMethode;
            _bwThread.WorkerSupportsCancellation = true;
            //Starten des Threads.
            _bwThread.RunWorkerAsync();

        }


        /// <summary>
        /// Diese Methode läuft als separater Thread und überprüft, ob
        /// neue KFZs vorhanden sind, KFZs verändert wurden oder gelöscht
        /// wurden. 
        /// Über entsprechende Events werden die Veränderungen bekannt gegeben.
        /// </summary>
        private void ThreadMethode(object sender, DoWorkEventArgs e)
        {
            //Endlosschleife
            while (true)
            {


                //foreach (KFZModel kfzm in KFZListe)
                //{

                //}
                KFZ dummy = new KFZ() { Idkfz = 23, FahrgestellNr = "2342354" };

                if (KFZStateChanged != null)
                {
                    KFZStateChanged(E_KFZSTATE.eKFZNew, dummy);
                }

                System.Threading.Thread.Sleep(5000); //5 Sek. schlafen.
            }

        }

        public void GetAllKFZFromDB()
        {
            //if fehlt noch aus Config-File ODER über using-Direktive...
            IKFZDataAccess dbAccess = new MariaDBDataAccess();
            //alternativ:
            //IKFZDataAccess dbAccess = new PostGreSQLDataAccess();

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
