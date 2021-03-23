using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Events
{
    //Deklaration des EventHandlers
    public delegate void KFZDataReadyEventHandler(List<KFZModel> list);
    public delegate void InfoMessageEventHandler(string msg);
}
