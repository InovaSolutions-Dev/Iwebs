using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Galatee.Structure
{
    [DataContract]
    public class CsMapperDetailMandatement
    {
        public int FK_IDMANDATEMENT { get; set; }
        public string CENTRE { get; set; }
        public string CLIENT { get; set; }
        public string ORDRE { get; set; }
        public string PERIODE { get; set; }
        public string NDOC { get; set; }
        public string STATUS { get; set; }
        public Nullable<decimal> MONTANT { get; set; }
        public int PK_ID { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERCREATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public Nullable<decimal> MONTANTTVA { get; set; }
        public Nullable<int> FK_IDCENTRE { get; set; }
        public Nullable<int> FK_IDLCLIENT { get; set; }
        public Nullable<int> FK_IDCLIENT { get; set; }
    }
}
