using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PostGreSQLDataAccess : IKFZDataAccess
    {
        public bool IsConnected => throw new NotImplementedException();

        public event ErrorMessageEventHandler ErrorMessage;
        public event InfoMessageEventHandler InfoMessage;

        public bool Connect(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void DeleteKFZ(KFZ kfz)
        {
            throw new NotImplementedException();
        }

        public List<KFZ> GetKFZList()
        {
            List<KFZ> kfzListeMock = new List<KFZ>();

            KFZ k1 = new KFZ()
            {
                Idkfz = 1,
                Kennzeichen = "S-GH 65",
                Typ = "Limousine",
                Leistung = 123,
                FahrgestellNr = "FG 4245"
            };

            kfzListeMock.Add(k1);

            KFZ k2 = new KFZ()
            {
                Idkfz = 1,
                Kennzeichen = "BB-RB 4711",
                Typ = "SUV",
                Leistung = 204,
                FahrgestellNr = "4828345"
            };
            kfzListeMock.Add(k2);

            return kfzListeMock;
        }

        public void InsertNewKFZ(KFZ newKfz)
        {
            throw new NotImplementedException();
        }

        public void UpdateKFZ(KFZ kfz)
        {
            throw new NotImplementedException();
        }
    }
}
