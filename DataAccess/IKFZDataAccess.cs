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
        List<KFZ> GetKFZList();
    }
}
