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
    
    public partial class DEFPARAMABON
    {
        public string CENTRE { get; set; }
        public string CODE { get; set; }
        public string PARAM { get; set; }
        public string PRODUIT { get; set; }
        public string LIBELLE { get; set; }
        public string FORMAT { get; set; }
        public string GROUPE { get; set; }
        public Nullable<int> SOUSGROUPE { get; set; }
        public Nullable<int> MODESAISIE { get; set; }
        public string CODERECHERCHE { get; set; }
        public Nullable<int> TRAITEMENT { get; set; }
        public Nullable<System.DateTime> DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERCREATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int PK_ID { get; set; }
        public int FK_IDCENTRE { get; set; }
        public int FK_IDPRODUIT { get; set; }
    
        public virtual CENTRE CENTRE1 { get; set; }
        public virtual PRODUIT PRODUIT1 { get; set; }
    }
}
