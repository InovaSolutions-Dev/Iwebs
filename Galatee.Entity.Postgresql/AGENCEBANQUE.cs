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
    
    public partial class AGENCEBANQUE
    {
        public int PK_ID { get; set; }
        public string CODE { get; set; }
        public string CODEBANQUE { get; set; }
        public string LIBELLE { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public string USERCREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int FK_IDBANQUE { get; set; }
    
        public virtual BANQUE BANQUE { get; set; }
    }
}
