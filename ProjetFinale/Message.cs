//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjetFinale
{
    using System;
    using System.Collections.Generic;
    
    public partial class Message
    {
        public int id_message { get; set; }
        public Nullable<int> id_user { get; set; }
        public string nomMembre { get; set; }
        public string sujet { get; set; }
        public string description { get; set; }
        public string emetteur { get; set; }
        public string recepteur { get; set; }
    
        public virtual Utilisateur Utilisateur { get; set; }
    }
}
