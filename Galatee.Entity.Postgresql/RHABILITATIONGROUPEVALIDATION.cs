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
    
    public partial class RHABILITATIONGROUPEVALIDATION
    {
        public System.Guid PK_ID { get; set; }
        public int RANG { get; set; }
        public int FK_IDADMUTILISATEUR { get; set; }
        public System.Guid FK_IDGROUPE_VALIDATION { get; set; }
        public System.DateTime DATE_HABILITATION { get; set; }
        public Nullable<System.DateTime> DATE_FIN_VALIDITE { get; set; }
        public string MATRICULE_USER_CREATION { get; set; }
        public Nullable<System.DateTime> DATE_DERNIEREMODIFICATION { get; set; }
        public string MATRICULE_USER_MODIFICATION { get; set; }
        public System.DateTime DATE_CREATION_HABILITATION { get; set; }
        public Nullable<bool> ESTRESPONSABLE { get; set; }
        public Nullable<bool> ESTCONSULTATION { get; set; }
    
        public virtual ADMUTILISATEUR ADMUTILISATEUR { get; set; }
        public virtual GROUPE_VALIDATION GROUPE_VALIDATION { get; set; }
    }
}
