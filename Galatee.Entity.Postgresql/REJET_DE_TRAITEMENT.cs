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
    
    public partial class REJET_DE_TRAITEMENT
    {
        public int PK_ID { get; set; }
        public Nullable<int> NUMERO_DE_LIGNE { get; set; }
        public string MOTIF_DE_REJET { get; set; }
        public string DONNE { get; set; }
        public Nullable<int> FK_IDTRAITEMENT_FICHIER { get; set; }
    
        public virtual TRAITEMENT_FICHIER TRAITEMENT_FICHIER { get; set; }
    }
}
