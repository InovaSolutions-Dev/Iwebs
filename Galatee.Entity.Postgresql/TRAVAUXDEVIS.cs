//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Galatee.Entity.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRAVAUXDEVIS
    {
        public string NUMDEM { get; set; }
        public int ORDRE { get; set; }
        public string MATRICULECHEFEQUIPE { get; set; }
        public string NOMCHEFEQUIPE { get; set; }
        public string PROCESVERBAL { get; set; }
        public Nullable<decimal> MONTANTREGLE { get; set; }
        public Nullable<decimal> MONTANTREMBOURSEMENT { get; set; }
        public Nullable<int> FK_IDPRESTATAIRE { get; set; }
        public System.DateTime DATEPREVISIONNELLE { get; set; }
        public Nullable<System.DateTime> DATEDEBUTTRVX { get; set; }
        public Nullable<System.DateTime> DATEFINTRVX { get; set; }
        public string MATRICULEREGLEMENT { get; set; }
        public Nullable<System.DateTime> DATEREGLEMENT { get; set; }
        public bool ISUSEDFORBILAN { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERCREATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int PK_ID { get; set; }
        public int FK_IDDEMANDE { get; set; }
        public Nullable<int> FK_IDTYPEDEPANNE { get; set; }
        public Nullable<int> FK_IDVEHICULE { get; set; }
        public string NBRCABLESECTION { get; set; }
    
        public virtual PRESTATAIRE PRESTATAIRE { get; set; }
        public virtual GRC_PANNE GRC_PANNE { get; set; }
        public virtual GRC_VEHICULE GRC_VEHICULE { get; set; }
        public virtual DEMANDE DEMANDE { get; set; }
    }
}