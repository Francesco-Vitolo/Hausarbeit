//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Verwaltungsprogramm_Vinothek
{
    using System;
    using System.Collections.Generic;
    
    public partial class Produzent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produzent()
        {
            this.Produkt = new HashSet<Produkt>();
        }
    
        public int ID_Produzent { get; set; }
        public string Name { get; set; }
        public string Land { get; set; }
        public string Region { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public Nullable<int> Hektar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produkt> Produkt { get; set; }
    }
}
