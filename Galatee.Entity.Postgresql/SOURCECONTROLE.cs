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
    
    public partial class SOURCECONTROLE
    {
        public SOURCECONTROLE()
        {
            this.FRAUDE = new HashSet<FRAUDE>();
        }
    
        public int PK_ID { get; set; }
        public string Libelle { get; set; }
    
        public virtual ICollection<FRAUDE> FRAUDE { get; set; }
    }
}
