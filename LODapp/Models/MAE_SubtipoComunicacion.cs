
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LODapp.Models
{
    public class MAE_SubtipoComunicacion
    {
        [PrimaryKey]
        public int IdTipoSub { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int IdTipoCom { get; set; }

        public string TipoComunicacion { get; set; }

        public bool Activo { get; set; }

    }
}