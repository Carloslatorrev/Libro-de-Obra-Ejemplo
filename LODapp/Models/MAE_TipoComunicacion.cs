using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace LODapp.Models
{
    public class MAE_TipoComunicacion
    {
        [PrimaryKey]
        public int IdTipoCom { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public int IdTipoLod { get; set; }

        public string TipoLibro { get; set; }

        //[OneToMany]
        //public List<MAE_SubtipoComunicacion> Subtipos { get; set; }
        
    }
}