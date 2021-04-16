using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class KFZModel //KFZPresenter
    {
        public bool IsSelected { get; set; }
        private KFZ _kfzData;

        public int Leistung{ get; set; }
        public string Kennzeichen { 
            get {
                return _kfzData.Kennzeichen;
            }
            set {
                //Validierung hier sinnoll
                _kfzData.Kennzeichen = value;

            }
        }




































        public long Idkfz { get; set; }

        private string _kennzeichen;
        public string Kennzeichen
        {
            get
            {
                return _kennzeichen;
            }

            set
            {
                _kennzeichen = value;
            }
        }

        public string FahrgestellNr { get; set; }
        public int Leistung { get; set; }
        public string Typ { get; set; }

        public KFZModel()
        {

        }

        public override string ToString()
        {
            //string info = string.Format("Kennzeichen: {0} und ID: {1}", Kennzeichen, Idkfz);
            return $"Infos zum Fahrzeug: Kennzeichen {Kennzeichen}, Typ: {Typ}." ;
        }
    }
}
