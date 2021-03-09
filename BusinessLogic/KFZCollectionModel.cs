using BusinessLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class KFZCollectionModel
    {

        public List<KFZModel> KFZListe = new List<KFZModel>();
        
        public event KFZDataReadyEventHandler KFZDataReady;


        public KFZCollectionModel()
        {



        }

        public void GetAllKFZFromDB()
        {
            this.KFZListe = Connection.GetKFZList();

            if (KFZDataReady != null)
                KFZDataReady(KFZListe); //Event feuern.
        }
    }
}
