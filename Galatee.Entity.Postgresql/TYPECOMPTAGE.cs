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
    
    public partial class TYPECOMPTAGE
    {
        public TYPECOMPTAGE()
        {
            this.ABON = new HashSet<ABON>();
            this.DABON = new HashSet<DABON>();
            this.PROFAC = new HashSet<PROFAC>();
            this.TYPECOMPTEURCOMPTAGE = new HashSet<TYPECOMPTEURCOMPTAGE>();
            this.DEMANDE = new HashSet<DEMANDE>();
            this.FAC_PROFAC = new HashSet<FAC_PROFAC>();
        }
    
        public string CODE { get; set; }
        public string LIBELLE { get; set; }
        public bool TRANSFO_UNIQUE { get; set; }
        public Nullable<int> PUISSANCESOUSCRITE_MINI { get; set; }
        public Nullable<int> PUISSANCESOUSCRITE_MAXI { get; set; }
        public Nullable<int> PUISSANCEINSTALLEE_MINI { get; set; }
        public Nullable<int> PUISSANCEINSTALLEE_MAXI { get; set; }
        public bool AVEC_PERTE { get; set; }
        public bool AVEC_PRIMEFIXE { get; set; }
        public string USERCREATION { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int PK_ID { get; set; }
    
        public virtual ICollection<ABON> ABON { get; set; }
        public virtual ICollection<DABON> DABON { get; set; }
        public virtual ICollection<PROFAC> PROFAC { get; set; }
        public virtual ICollection<TYPECOMPTEURCOMPTAGE> TYPECOMPTEURCOMPTAGE { get; set; }
        public virtual ICollection<DEMANDE> DEMANDE { get; set; }
        public virtual ICollection<FAC_PROFAC> FAC_PROFAC { get; set; }
    }
}