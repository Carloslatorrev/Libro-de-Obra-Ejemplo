using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Linq;
using System.Web;

namespace LODapp.Models
{
    public class MAE_ClassTwo
    {
        [PrimaryKey]
        public int IdClassTwo { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public int IdClassOne { get; set; }

        public string ClassOne { get; set; }
    }
}