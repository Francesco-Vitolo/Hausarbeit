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
    
    public partial class Logins
    {
        public int ID_Login { get; set; }
        public Nullable<int> ID_Benutzer { get; set; }
        public Nullable<System.DateTime> Datum { get; set; }
    
        public virtual Benutzer Benutzer { get; set; }
    }
}