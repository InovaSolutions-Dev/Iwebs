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
    
    public partial class GRC_TYPE_VEHICULE
    {
        public GRC_TYPE_VEHICULE()
        {
            this.GRC_VEHICULE = new HashSet<GRC_VEHICULE>();
        }
    
        public int ID { get; set; }
        public string LIBELLE { get; set; }
    
        public virtual ICollection<GRC_VEHICULE> GRC_VEHICULE { get; set; }
    }
}
