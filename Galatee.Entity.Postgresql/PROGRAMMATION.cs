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
    
    public partial class PROGRAMMATION
    {
        public int PK_ID { get; set; }
        public Nullable<System.DateTime> DATEPROGRAMME { get; set; }
        public Nullable<System.Guid> FK_IDEQUIPE { get; set; }
        public Nullable<int> FK_IDDEMANDE { get; set; }
        public Nullable<bool> ESTACTIF { get; set; }
        public Nullable<bool> ISCOMPTEURLIVRE { get; set; }
        public Nullable<bool> ISMATERIELLIVRE { get; set; }
        public Nullable<bool> ISMATERIELEXTLIVRE { get; set; }
        public Nullable<System.DateTime> DATECREATION { get; set; }
        public string NUMPROGRAMME { get; set; }
        public string RECPETEURCOMPTEUR { get; set; }
        public string RECPETEURMATERIEL { get; set; }
    
        public virtual DEMANDE DEMANDE { get; set; }
    }
}
