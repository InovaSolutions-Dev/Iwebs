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
    
    public partial class COMPTEURFRAUDE
    {
        public int PK_ID { get; set; }
        public string NumeroCompteur { get; set; }
        public Nullable<int> IndexCompteur { get; set; }
        public string RefPlombCompteur { get; set; }
        public string NumeroPince { get; set; }
        public string CertificatPlombage { get; set; }
        public string RefPlombCacheBorne { get; set; }
        public string RefPlombCoffretFusible { get; set; }
        public string RefPlombCoffretSecurite { get; set; }
        public string Bordereau { get; set; }
        public Nullable<bool> IsEcartIndex { get; set; }
        public Nullable<int> FK_IDCLIENTFRAUDE { get; set; }
        public Nullable<int> FK_IDCONTROLE { get; set; }
        public Nullable<int> FK_IDPRODUIT { get; set; }
        public Nullable<int> FK_IDMARQUECOMPTEUR { get; set; }
        public Nullable<int> FK_IDTYPECOMPTEUR { get; set; }
        public Nullable<int> FK_IDANOMALIECOMPTEUR { get; set; }
        public Nullable<int> FK_IDANOMALIECACHEBORNE { get; set; }
        public Nullable<int> FK_IDANOMALIEBRANCHEMENT { get; set; }
        public Nullable<int> FK_IDPHASECOMPTEUR { get; set; }
        public Nullable<int> FK_IDCALIBRECOMPTEUR { get; set; }
        public Nullable<int> FK_IDREGLAGE { get; set; }
        public Nullable<int> FK_IDACTIONSURCOMPTEUR { get; set; }
        public Nullable<int> FK_IDUSAGEPRODUIT { get; set; }
        public Nullable<int> FK_IDTYPEDISJONCTEUR { get; set; }
        public Nullable<int> FK_IDMARQUEDISJONCTEUR { get; set; }
        public string PRODUIT { get; set; }
        public string TYPECOMPTEUR { get; set; }
        public string MARQUE { get; set; }
        public string USAGEPRODUIT { get; set; }
    
        public virtual CALIBRECOMPTEUR CALIBRECOMPTEUR { get; set; }
        public virtual MARQUECOMPTEUR MARQUECOMPTEUR { get; set; }
        public virtual PHASECOMPTEUR PHASECOMPTEUR { get; set; }
        public virtual PRODUIT PRODUIT1 { get; set; }
        public virtual REGLAGECOMPTEUR REGLAGECOMPTEUR { get; set; }
        public virtual TYPECOMPTEUR TYPECOMPTEUR1 { get; set; }
        public virtual TYPEDISJONCTEUR TYPEDISJONCTEUR { get; set; }
        public virtual USAGE USAGE { get; set; }
        public virtual ACTIONSURCOMPTEUR ACTIONSURCOMPTEUR { get; set; }
        public virtual CLIENTFRAUDE CLIENTFRAUDE { get; set; }
        public virtual CONTROLE CONTROLE { get; set; }
        public virtual MARQUEDISJONCTEUR MARQUEDISJONCTEUR { get; set; }
        public virtual TYPEFRAUDE TYPEFRAUDE { get; set; }
        public virtual TYPEFRAUDE TYPEFRAUDE1 { get; set; }
        public virtual TYPEFRAUDE TYPEFRAUDE2 { get; set; }
    }
}
