//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocialPuentes.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Modulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modulo()
        {
            this.Publicaciones = new HashSet<Publicacione>();
        }
    
        public int Id_Modulos { get; set; }
        public string ModulosNombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Publicacione> Publicaciones { get; set; }
    }
}
