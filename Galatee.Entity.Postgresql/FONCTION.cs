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
    
    public partial class FONCTION
    {
        public FONCTION()
        {
            this.MODULESDEFONCTION = new HashSet<MODULESDEFONCTION>();
            this.PROFIL = new HashSet<PROFIL>();
        }
    
        public string CODE { get; set; }
        public string ROLENAME { get; set; }
        public string ROLEDISPLAYNAME { get; set; }
        public Nullable<bool> ESTADMIN { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERCREATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int PK_ID { get; set; }
    
        public virtual ICollection<MODULESDEFONCTION> MODULESDEFONCTION { get; set; }
        public virtual ICollection<PROFIL> PROFIL { get; set; }
    }
}
