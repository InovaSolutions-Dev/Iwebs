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
    
    public partial class COMPTA_TYPE_COMPTE
    {
        public COMPTA_TYPE_COMPTE()
        {
            this.COMPTA_COMPTE_SPECIFIQUE = new HashSet<COMPTA_COMPTE_SPECIFIQUE>();
        }
    
        public int PK_ID { get; set; }
        public string CODE { get; set; }
        public string LIBELLE { get; set; }
        public string USERCREATION { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public Nullable<bool> AVECFILTRE { get; set; }
        public string TABLEFILTRE { get; set; }
        public string TABLEFILTRE1 { get; set; }
        public string TABLEFILTRE2 { get; set; }
        public string VALEURMONTANT { get; set; }
        public Nullable<bool> SIGNE { get; set; }
        public string SOUSCOMPTE { get; set; }
    
        public virtual ICollection<COMPTA_COMPTE_SPECIFIQUE> COMPTA_COMPTE_SPECIFIQUE { get; set; }
    }
}
