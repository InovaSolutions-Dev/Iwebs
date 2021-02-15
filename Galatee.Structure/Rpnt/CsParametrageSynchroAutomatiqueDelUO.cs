using System;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace  Galatee.Structure

{
     [DataContract]
    public class CsParametrageSynchroAutomatiqueDelUO
    {
		
		
[DataMember]
		public String CodeExploitation { get; set; }
[DataMember]
		public String CodeUO { get; set; }
[DataMember]
		public DateTime? DateDebutValidite { get; set; }
[DataMember]
		public Boolean EstActif { get; set; }
[DataMember]
		public Int32? Periodicite { get; set; }
[DataMember]
		public Int32? PartieHeureLancement_Heure { get; set; }
[DataMember]
		public Int32? PartieHeureLancement_Minute { get; set; }
[DataMember]
		public DateTime? DateProchaineExecution { get; set; }


    }
}
