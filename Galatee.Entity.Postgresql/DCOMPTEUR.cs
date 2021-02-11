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
    
    public partial class DCOMPTEUR
    {
        public string PRODUIT { get; set; }
        public string NUMERO { get; set; }
        public string TYPECOMPTEUR { get; set; }
        public string MARQUE { get; set; }
        public Nullable<decimal> COEFLECT { get; set; }
        public Nullable<int> COEFCOMPTAGE { get; set; }
        public Nullable<byte> CADRAN { get; set; }
        public string ANNEEFAB { get; set; }
        public string STATUT { get; set; }
        public string FONCTIONNEMENT { get; set; }
        public string PLOMBAGE { get; set; }
        public string USERCREATION { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int FK_IDTYPECOMPTEUR { get; set; }
        public int FK_IDPRODUIT { get; set; }
        public int FK_IDMARQUECOMPTEUR { get; set; }
        public Nullable<int> FK_IDCALIBRE { get; set; }
        public int FK_IDSTATUTCOMPTEUR { get; set; }
        public int FK_IDETATCOMPTEUR { get; set; }
        public Nullable<int> FK_IDCOMPTEUR { get; set; }
        public int FK_IDDEMANDE { get; set; }
        public int PK_ID { get; set; }
    
        public virtual CALIBRECOMPTEUR CALIBRECOMPTEUR { get; set; }
        public virtual COMPTEUR COMPTEUR { get; set; }
        public virtual ETATCOMPTEUR ETATCOMPTEUR { get; set; }
        public virtual MARQUECOMPTEUR MARQUECOMPTEUR { get; set; }
        public virtual PRODUIT PRODUIT1 { get; set; }
        public virtual STATUTCOMPTEUR STATUTCOMPTEUR { get; set; }
        public virtual TYPECOMPTEUR TYPECOMPTEUR1 { get; set; }
        public virtual DEMANDE DEMANDE { get; set; }
    }
}
