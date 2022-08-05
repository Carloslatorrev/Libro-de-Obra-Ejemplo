using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LODapp.Models
{
    public class MAE_TipoLOD
    {
        [PrimaryKey]
        public int IdTipoLod { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }
        public int TipoLodJer { get; set; }
        public bool EsObligatorio { get; set; }
        public string Color { get; set; }

        //[OneToMany]
        //public List<LOD_LibroObras> Libros { get; set; }
    }
}