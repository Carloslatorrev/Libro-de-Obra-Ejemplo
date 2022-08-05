using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Xamarin.Essentials;

namespace LODapp.Models
{
    public class LOD_docAnotacion
    {
        [PrimaryKey]
        public int IdDocAnotacion { get; set; }

        public int IdDoc { get; set; }

        public string documento { get; set; }


        public int IdAnotacion { get; set; }
        
        public string Anotacion { get; set; }


        public int IdTipoDoc { get; set; }

        public string TipoDocumento { get; set; }

        public int IdContrato { get; set; }
        /*
         0 = pendiente
         1 = Aprobado
         2 = Rechazado
         */
        public int EstadoDoc { get; set; }
        public string Observaciones { get; set; }

        public DateTime? FechaEvento { get; set; }


        public string IdUserEvento { get; set; }
        public string UsuarioEvento { get; set; }
        public string ruta { get; set; }

        public int? Actualizado { get; set; }
        public bool? useRutaLocal { get; set; }

        [Ignore]
        public bool SinCargar { get; set; }
        [Ignore]
        public bool Cargado { get; set; }
        [Ignore]
        public bool Aprobado { get; set; }
        [Ignore]
        public bool Rechazado { get; set; }
        [Ignore]
        public bool PermiteBorrar { get; set; }

        //[NotMapped]
        //public HttpPostedFileBase PerFileName { get; set; }

        //[NotMapped]
        //public int IdClassDoc { get; set; }
        //[NotMapped]
        //public virtual MAE_ClassDoc ClassDoc { get; set; }
        //[NotMapped]
        //public int IdClassTwo { get; set; }
    }

    public class AddDocumentoView
    {

        public int IdAnotacion { get; set; }
        public int IdTipoDoc { get; set; }
        public int IdContrato { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string UserId { get; set; }

        //public string file { get; set; }
    }

    public class AddOtroDocumentoView
    {
        public int IdAnotacion { get; set; }
        public int IdTipoDoc { get; set; }
        public int IdClassTwo { get; set; }
        public int IdContrato { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string UserId { get; set; }

        //public string file { get; set; }
    }
}