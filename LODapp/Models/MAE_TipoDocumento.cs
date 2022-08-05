using SQLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LODapp.Models
{
    public class MAE_TipoDocumento
    {
        [PrimaryKey]
        public int IdTipo { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public int TipoClasi { get; set; }

        [Ignore]
        public bool SinCargar { get; set; }
        [Ignore]
        public bool Cargado { get; set; }
        [Ignore]
        public bool Aprobado { get; set; }
        [Ignore]
        public bool Rechazado { get; set; }
        [Ignore]
        public bool PendAprobacion { get; set; }
        [Ignore]
        public bool PermiteBorrar { get; set; }
        [Ignore]
        public bool PermiteDescargar { get; set; }
        [Ignore]
        public bool PermiteEdicion { get; set; }
        [Ignore]
        public string Ruta { get; set; }
        [Ignore]
        public int? IdDocAnotacion { get; set; }

        /*
         1 = Formulario
         2 = Documento técnico
         3 = Documento administrativo
         4 = Otros
         */

    }
}