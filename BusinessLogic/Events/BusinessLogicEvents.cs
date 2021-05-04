using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Events
{
    public enum E_KFZSTATE
    {
        eKFZNew,
        eKFZChanged,
        eKFZDeleted
    }

    //Deklaration des EventHandlers
    public delegate void KFZDataReadyEventHandler(List<KFZModel> list);
    public delegate void InfoMessageEventHandler(string msg);
    public delegate void KFZStateChangedEventHandler(E_KFZSTATE kfzs, KFZ kfz);
}
