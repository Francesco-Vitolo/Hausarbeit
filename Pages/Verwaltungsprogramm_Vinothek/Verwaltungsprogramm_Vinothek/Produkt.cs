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
    
    public partial class Produkt
    {
        public int ID_Produkt { get; set; }
        public string Name { get; set; }
        public string Art { get; set; }
        public string Qualitätssiegel { get; set; }
        public string Rebsorten { get; set; }
        public string Geschmack { get; set; }
        public Nullable<double> Alkoholgehalt { get; set; }
        public Nullable<int> Jahrgang { get; set; }
        public string Beschreibung { get; set; }
        public byte[] Picture { get; set; }
        public byte[] PDF_file { get; set; }
        public int ID_Produzent { get; set; }
    
        public virtual Produzent Produzent { get; set; }
    }
}
