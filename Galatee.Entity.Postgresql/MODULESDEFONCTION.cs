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
    
    public partial class MODULESDEFONCTION
    {
        public int FK_IDFONCTION { get; set; }
        public int FK_IDMODULE { get; set; }
        public int PK_ID { get; set; }
    
        public virtual FONCTION FONCTION { get; set; }
        public virtual MODULE MODULE { get; set; }
    }
}
