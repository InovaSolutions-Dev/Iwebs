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
    
    public partial class TRANSFORMATEUR
    {
        public int PK_ID { get; set; }
        public string NUMERO { get; set; }
        public string TYPE { get; set; }
        public string MARQUE { get; set; }
        public decimal PUISSANCEINSTALLEE { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public string USERCREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public int FK_IDTYPETRANSFORMATEUR { get; set; }
        public int FK_IDMARQUETRANSFORMATEUR { get; set; }
    
        public virtual MARQUETRANSFORMATEUR MARQUETRANSFORMATEUR { get; set; }
        public virtual TYPETRANSFORMATEUR TYPETRANSFORMATEUR { get; set; }
    }
}
