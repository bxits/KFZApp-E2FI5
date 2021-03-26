using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    ///Design by Contract: Das Interface gibt vor, welche Methoden
    ///die implementierenen Klassen bereitstellen müssen.
    public interface IKFZDataAccess
    {
        bool IsConnected { get; }

        //CRUD-Szenario implementieren
        
        //Read 
        List<KFZ> GetKFZList();

        //Create
        void InsertNewKFZ(KFZ newKfz);

        //Delete
        void DeleteKFZ(KFZ kfz);

        //Update
        void UpdateKFZ(KFZ kfz);
        
    }
}
