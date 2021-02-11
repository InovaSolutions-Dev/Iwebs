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
    
    public partial class CAMPAGNE
    {
        public CAMPAGNE()
        {
            this.DETAILCAMPAGNE = new HashSet<DETAILCAMPAGNE>();
            this.INDEXCAMPAGNE = new HashSet<INDEXCAMPAGNE>();
        }
    
        public string IDCOUPURE { get; set; }
        public string CENTRE { get; set; }
        public Nullable<decimal> MONTANT { get; set; }
        public string MATRICULEPIA { get; set; }
        public string PERIODE_RELANCABLE { get; set; }
        public Nullable<System.DateTime> DATE_EXIGIBILITE { get; set; }
        public string PREMIERE_TOURNEE { get; set; }
        public string DERNIERE_TOURNEE { get; set; }
        public string DEBUT_ORDTOUR { get; set; }
        public string FIN_ORDTOUR { get; set; }
        public Nullable<decimal> MONTANT_RELANCABLE { get; set; }
        public string DEBUT_CATEGORIE { get; set; }
        public string FIN_CATEGORIE { get; set; }
        public string NOMBRE_CLIENT { get; set; }
        public string NOMBRE_FACTURE { get; set; }
        public string DEBUT_AG { get; set; }
        public string FIN_AG { get; set; }
        public int PK_ID { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERCREATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int FK_IDCENTRE { get; set; }
        public int FK_IDMATRICULE { get; set; }
    
        public virtual ADMUTILISATEUR ADMUTILISATEUR { get; set; }
        public virtual CENTRE CENTRE1 { get; set; }
        public virtual CAMPAGNE CAMPAGNE1 { get; set; }
        public virtual CAMPAGNE CAMPAGNE2 { get; set; }
        public virtual ICollection<DETAILCAMPAGNE> DETAILCAMPAGNE { get; set; }
        public virtual ICollection<INDEXCAMPAGNE> INDEXCAMPAGNE { get; set; }
    }
}
