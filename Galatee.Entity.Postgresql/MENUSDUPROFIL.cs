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
    
    public partial class MENUSDUPROFIL
    {
        public int FK_IDPROFIL { get; set; }
        public int FK_IDMENU { get; set; }
        public int PK_ID { get; set; }
    
        public virtual ADMMENU ADMMENU { get; set; }
        public virtual PROFIL PROFIL { get; set; }
    }
}
