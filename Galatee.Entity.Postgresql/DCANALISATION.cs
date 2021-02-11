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
    
    public partial class DCANALISATION
    {
        public DCANALISATION()
        {
            this.DEVENEMENT = new HashSet<DEVENEMENT>();
        }
    
        public string CENTRE { get; set; }
        public string CLIENT { get; set; }
        public string NUMDEM { get; set; }
        public string PRODUIT { get; set; }
        public string PROPRIO { get; set; }
        public string REGLAGECOMPTEUR { get; set; }
        public int POINT { get; set; }
        public string BRANCHEMENT { get; set; }
        public Nullable<int> SURFACTURATION { get; set; }
        public Nullable<int> DEBITANNUEL { get; set; }
        public string REPERAGECOMPTEUR { get; set; }
        public System.DateTime POSE { get; set; }
        public Nullable<System.DateTime> DEPOSE { get; set; }
        public string USERCREATION { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int PK_ID { get; set; }
        public int FK_IDCENTRE { get; set; }
        public Nullable<int> FK_IDCOMPTEUR { get; set; }
        public int FK_IDPRODUIT { get; set; }
        public int FK_IDDEMANDE { get; set; }
        public int FK_IDPROPRIETAIRE { get; set; }
        public Nullable<int> FK_IDREGLAGECOMPTEUR { get; set; }
        public byte ORDREAFFICHAGE { get; set; }
        public Nullable<int> FK_IDMAGAZINVIRTUEL { get; set; }
        public Nullable<bool> ISMODIFIER { get; set; }
        public Nullable<int> FK_IDABON { get; set; }
    
        public virtual CANALISATION CANALISATION { get; set; }
        public virtual CENTRE CENTRE1 { get; set; }
        public virtual COMPTEUR COMPTEUR { get; set; }
        public virtual MAGASINVIRTUEL MAGASINVIRTUEL { get; set; }
        public virtual PRODUIT PRODUIT1 { get; set; }
        public virtual PROPRIETAIRE PROPRIETAIRE { get; set; }
        public virtual REGLAGECOMPTEUR REGLAGECOMPTEUR1 { get; set; }
        public virtual ICollection<DEVENEMENT> DEVENEMENT { get; set; }
        public virtual DEMANDE DEMANDE { get; set; }
    }
}
