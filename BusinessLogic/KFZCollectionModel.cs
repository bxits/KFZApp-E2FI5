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

        public KFZCollectionModel()
        {
            this.KFZListe = Connection.GetKFZList();


        }
    }
}
