using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace LODapp.Models
{
    public class MAE_ClassOne
    {
        [PrimaryKey]
        public int IdClassOne { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        //[OneToMany]
        //public List<MAE_ClassTwo> ClassTwos { get; set; }
        
    }
}