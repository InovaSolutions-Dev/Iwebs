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
    
    public partial class COUTDEMANDE
    {
        public COUTDEMANDE()
        {
            this.ELEMENTDEVIS = new HashSet<ELEMENTDEVIS>();
        }
    
        public int PK_ID { get; set; }
        public string CENTRE { get; set; }
        public string PRODUIT { get; set; }
        public string TYPEDEMANDE { get; set; }
        public string COPER { get; set; }
        public string CATEGORIE { get; set; }
        public string REGLAGECOMPTEUR { get; set; }
        public string PUISSANCE { get; set; }
        public string TYPETARIF { get; set; }
        public Nullable<bool> OBLIGATOIRE { get; set; }
        public Nullable<bool> AUTOMATIQUE { get; set; }
        public Nullable<bool> SUBVENTIONNEE { get; set; }
        public Nullable<decimal> MONTANT { get; set; }
        public string TAXE { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERCREATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int FK_IDPRODUIT { get; set; }
        public int FK_IDCOPER { get; set; }
        public int FK_IDTYPEDEMANDE { get; set; }
        public int FK_IDCENTRE { get; set; }
        public int FK_IDTAXE { get; set; }
        public Nullable<int> FK_IDTYPETARIF { get; set; }
        public Nullable<int> FK_IDREGLAGECOMPTEUR { get; set; }
        public Nullable<int> FK_IDCATEGORIECLIENT { get; set; }
        public Nullable<int> FK_IDPUISSANCESOUSCRITE { get; set; }
    
        public virtual CATEGORIECLIENT CATEGORIECLIENT { get; set; }
        public virtual CENTRE CENTRE1 { get; set; }
        public virtual COPER COPER1 { get; set; }
        public virtual TYPEDEMANDE TYPEDEMANDE1 { get; set; }
        public virtual PRODUIT PRODUIT1 { get; set; }
        public virtual PUISSANCESOUSCRITE PUISSANCESOUSCRITE { get; set; }
        public virtual REGLAGECOMPTEUR REGLAGECOMPTEUR1 { get; set; }
        public virtual TAXE TAXE1 { get; set; }
        public virtual TYPETARIF TYPETARIF1 { get; set; }
        public virtual ICollection<ELEMENTDEVIS> ELEMENTDEVIS { get; set; }
    }
}
