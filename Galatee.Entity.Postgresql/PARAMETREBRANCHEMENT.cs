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
    
    public partial class PARAMETREBRANCHEMENT
    {
        public string CENTRE { get; set; }
        public string PRODUIT { get; set; }
        public Nullable<int> NBPOINTSMAXI { get; set; }
        public Nullable<int> GESTIONTRANSFO { get; set; }
        public Nullable<int> MODESAISIEINDEX { get; set; }
        public string USERCREATION { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int PK_ID { get; set; }
        public int FK_IDPRODUIT { get; set; }
        public int FK_IDCENTRE { get; set; }
    
        public virtual CENTRE CENTRE1 { get; set; }
        public virtual PRODUIT PRODUIT1 { get; set; }
    }
}
