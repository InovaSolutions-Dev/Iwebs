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
    
    public partial class CASIND
    {
        public CASIND()
        {
            this.DEVENEMENT = new HashSet<DEVENEMENT>();
            this.PROFAC = new HashSet<PROFAC>();
            this.FAC_PROFAC = new HashSet<FAC_PROFAC>();
        }
    
        public string CODE { get; set; }
        public string LIBELLE { get; set; }
        public string CASGEN1 { get; set; }
        public string CASGEN2 { get; set; }
        public string CASGEN3 { get; set; }
        public string CASGEN4 { get; set; }
        public string CASGEN5 { get; set; }
        public string CASGEN6 { get; set; }
        public string CASGEN7 { get; set; }
        public string CASGEN8 { get; set; }
        public string CASGEN9 { get; set; }
        public string CASGEN10 { get; set; }
        public string SAISIEINDEX { get; set; }
        public string SAISIECOMPTEUR { get; set; }
        public string SAISIECONSO { get; set; }
        public Nullable<bool> ENQUETABLE { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERCREATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int PK_ID { get; set; }
        public Nullable<int> FK_IDTYPEFACTURATIONSANSENQUETE { get; set; }
        public Nullable<int> FK_IDTYPEFACTURATIONAPRESENQUETE { get; set; }
    
        public virtual TYPEFACTURATION TYPEFACTURATION { get; set; }
        public virtual TYPEFACTURATION TYPEFACTURATION1 { get; set; }
        public virtual ICollection<DEVENEMENT> DEVENEMENT { get; set; }
        public virtual ICollection<PROFAC> PROFAC { get; set; }
        public virtual ICollection<FAC_PROFAC> FAC_PROFAC { get; set; }
    }
}
