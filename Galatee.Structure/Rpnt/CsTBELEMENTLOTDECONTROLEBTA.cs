using System;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace  Galatee.Structure

{
     [DataContract]
    public class CsTBELEMENTLOTDECONTROLEBTA:CsPrint
    {
		
		
[DataMember]
public Guid? LOT_ID { get; set; }
[DataMember]
public string CENTRE { get; set; }
[DataMember]
public string CLIENT { get; set; }
[DataMember]
public string LIBELLE_LOT { get; set; }
[DataMember]
public string NOMABON { get; set; }
[DataMember]
		public Guid CONTRAT_ID { get; set; }
[DataMember]
public int BRT_ID { get; set; }
[DataMember]
public DateTime DATE_EDITION{ get; set; }
[DataMember]
		public DateTime? DATESELECTION { get; set; }
[DataMember]
		public DateTime? DATEAFFECTATIONLOT { get; set; }
[DataMember]
		public Guid? RESULTATCONTROLE_ID { get; set; }
[DataMember]
		public Int32 METHODE_ID { get; set; }
[DataMember]
		public Int32? DEBUT_PERAA { get; set; }
[DataMember]
		public Int32? DEBUT_PERMM { get; set; }
[DataMember]
		public Int32? FIN_PERAA { get; set; }
[DataMember]
		public Int32? FIN_PERMM { get; set; }
[DataMember]
		public Double DIFFERENCE { get; set; }
[DataMember]
		public Double? _MCHUTECONSO_CONSOENCHUTE_VALEUR { get; set; }
[DataMember]
		public Int32? _MCHUTECONSO_CONSOENCHUTE_PERAA { get; set; }
[DataMember]
		public Int32? _MCHUTECONSO_CONSOENCHUTE_PERMM { get; set; }
[DataMember]
		public Int32? _MCHUTECONSO_CONSOPRECEDENTE_PERAA { get; set; }
[DataMember]
		public Int32? _MCHUTECONSO_CONSOPRECEDENTE_PERMM { get; set; }
[DataMember]
		public Double? _MCONSOFAIBLE_CONSOJOURNALIEREATTENDUE { get; set; }
[DataMember]
		public Double? _MCONSOFAIBLE_CONSOMMATIONFACTUREE { get; set; }
[DataMember]
		public Int32? _MCONSOFAIBLE_NBREJOURSDECONSO { get; set; }

[DataMember]
public List<CsREFFAMILLEANOMALIEBTA> FAMILLEANOMALIEBTA { get; set; }
    }
}
