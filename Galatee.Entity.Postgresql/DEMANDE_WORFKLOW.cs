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
    
    public partial class DEMANDE_WORFKLOW
    {
        public DEMANDE_WORFKLOW()
        {
            this.COPIE_DMD_CIRCUIT = new HashSet<COPIE_DMD_CIRCUIT>();
            this.COMMENTAIRE_REJET = new HashSet<COMMENTAIRE_REJET>();
        }
    
        public System.Guid PK_ID { get; set; }
        public string CODE { get; set; }
        public int FK_IDCENTRE { get; set; }
        public System.Guid FK_IDOPERATION { get; set; }
        public System.Guid FK_IDWORKFLOW { get; set; }
        public System.Guid FK_IDRWORKLOW { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public int FK_IDTABLETRAVAIL { get; set; }
        public bool ALLCENTRE { get; set; }
        public int FK_IDETAPEACTUELLE { get; set; }
        public int FK_IDETAPEPRECEDENTE { get; set; }
        public int FK_IDETAPESUIVANTE { get; set; }
        public string MATRICULEUSERCREATION { get; set; }
        public Nullable<int> FK_IDSTATUS { get; set; }
        public string FK_IDLIGNETABLETRAVAIL { get; set; }
        public string MATRICULEUSERMODIFICATION { get; set; }
        public Nullable<System.DateTime> DATEDERNIEREMODIFICATION { get; set; }
        public Nullable<System.Guid> FK_IDETAPECIRCUIT { get; set; }
        public string CODE_DEMANDE_TABLETRAVAIL { get; set; }
    
        public virtual ICollection<COPIE_DMD_CIRCUIT> COPIE_DMD_CIRCUIT { get; set; }
        public virtual ICollection<COMMENTAIRE_REJET> COMMENTAIRE_REJET { get; set; }
    }
}