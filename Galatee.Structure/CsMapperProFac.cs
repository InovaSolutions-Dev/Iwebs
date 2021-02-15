﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Galatee.Structure
{
    [DataContract]
    public class CsMapperProFac  
    {
        public string LOTRI { get; set; }
        public string JET { get; set; }
        public string TOURNEE { get; set; }
        public string ORDTOUR { get; set; }
        public string CENTRE { get; set; }
        public string CLIENT { get; set; }
        public string ORDRE { get; set; }
        public string FACTURE { get; set; }
        public string TYPECOMPTAGE { get; set; }
        public string PRODUIT { get; set; }
        public string COMPTEUR { get; set; }
        public string REGLAGECOMPTEUR { get; set; }
        public Nullable<decimal> COEFLECT { get; set; }
        public Nullable<int> POINT { get; set; }
        public Nullable<decimal> PUISSANCE { get; set; }
        public string DERPERF { get; set; }
        public string DERPERFN { get; set; }
        public Nullable<int> REGCONSO { get; set; }
        public Nullable<int> REGFAC { get; set; }
        public string TFAC { get; set; }
        public Nullable<int> LIENRED { get; set; }
        public Nullable<int> CONSOFAC { get; set; }
        public Nullable<System.DateTime> DATEEVT { get; set; }
        public string PERIODE { get; set; }
        public Nullable<int> AINDEX { get; set; }
        public Nullable<int> NINDEX { get; set; }
        public string CAS { get; set; }
        public Nullable<int> CONSO { get; set; }
        public Nullable<decimal> TOTPROHT { get; set; }
        public Nullable<decimal> TOTPROTAX { get; set; }
        public Nullable<decimal> TOTPROTTC { get; set; }
        public string ADERPERF { get; set; }
        public string ADERPERFN { get; set; }
        public Nullable<int> REGIMPUTE { get; set; }
        public string TYPECOMPTEUR { get; set; }
        public string REGROU { get; set; }
        public Nullable<System.DateTime> DEVPRE { get; set; }
        public Nullable<int> NBREDTOT { get; set; }
        public Nullable<int> STATUS { get; set; }
        public string LIENFAC { get; set; }
        public Nullable<int> EVENEMENT { get; set; }
        public string TOPMAJ { get; set; }
        public Nullable<int> TOPANNUL { get; set; }
        public Nullable<decimal> PUISSANCEINSTALLEE { get; set; }
        public Nullable<int> COEFCOMPTAGE { get; set; }
        public string BRANCHEMENT { get; set; }
        public Nullable<decimal> COEFK1 { get; set; }
        public Nullable<decimal> COEFK2 { get; set; }
        public Nullable<decimal> PERTESACTIVES { get; set; }
        public Nullable<decimal> PERTESREACTIVES { get; set; }
        public Nullable<int> COEFFAC { get; set; }
        public int PK_ID { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERCREATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public Nullable<int> FK_IDENTFAC { get; set; }
        public Nullable<int> FK_IDEVENEMENT { get; set; }
        public Nullable<int> FK_IDTYPECOMPTAGE { get; set; }
        public int FK_IDPRODUIT { get; set; }
        public int FK_IDCAS { get; set; }
        public int FK_IDCENTRE { get; set; }
        public int FK_IDABON { get; set; }
        public string DEBUTEXONERATIONTVA { get; set; }
        public string FINEXONERATIONTVA { get; set; }


    }
}
