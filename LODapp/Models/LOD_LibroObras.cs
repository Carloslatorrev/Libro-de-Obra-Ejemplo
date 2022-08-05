
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Xamarin.Forms;

namespace LODapp.Models
{
    public class LOD_LibroObras
    {

        [PrimaryKey]
        public int IdLod { get; set; }
        public int IdContrato { get; set; }

        public string nombreContrato { get; set; }

        public string NombreLibroObra { get; set; }

        public string CodigoLObras { get; set; }
        public string DescripcionLObra { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UserId { get; set; }
        public string Usuario_Creacion { get; set; }

        public int IdTipoLod { get; set; }

        public string TipoLOD { get; set; }

        public int? Estado { get; set; }

        public string FechaCierre { get; set; }

        public string UsuarioApertura { get; set; }
        
        public string Usuario_Apertura { get; set; }

        public string UsuarioCierre { get; set; }
        public string Usuario_Cierre { get; set; }

        public string FechaApertura { get; set; }
        public bool TipoApertura { get; set; }

        public string RutaImagenLObras { get; set; }
        public string OTP { get; set; }

        public int? Actualizado { get; set; }

        public bool HerImgPadre { get; set; }
        [Ignore]
        public Color Color { get; set; }

        [Ignore]
        public bool UsarRuta { get; set; }
        [Ignore]
        public bool UsarLocal { get; set; }

        //[Ignore]
        //public HttpPostedFileBase fileImage { get; set; }


        //[Ignore]
        //public List<LOD_Anotaciones> LstAnotaciones { get; set; }

        //[Ignore]
        //public List<LOD_Anotaciones> AnotPendRespuesta { get; set; }

        //[Ignore]
        //public List<int> LstLeidas { get; set; }
        //[Ignore]
        //public List<int> LstDestacadas { get; set; }



        //[Ignore]
        //public string ContratoNombre
        //{
        //    get
        //    {
        //        return CON_Contratos.NombreContrato + "/" + NombreLibroObra;
        //    }
        //}

        //public virtual ICollection<LOD_UsuariosLod> LOD_UsuariosLod { get; set; }

        //[Ignore]
        //public string Creador { get; set; }

        //public virtual ICollection<LOD_Anotaciones> LOD_Anotaciones { get; set; }

        //[Ignore]
        //public int AnotFirmadasCount
        //{
        //    get
        //    {
        //        return (this.LOD_Anotaciones != null) ? this.LOD_Anotaciones.Where(w => w.EstadoFirma).Count() : 0;
        //    }
        //}

        //[Ignore]
        //public int MisRespuestasPendientes { get; set; }

        //[Ignore]
        //public int MisVBPendientes { get; set; }
        //[Ignore]
        //public int tipo { get; set; }

        //[Ignore]
        //public string TipoFirmaApertura
        //{
        //    get
        //    {
        //        string tipo = "Firma Electrónica Avanzada";
        //        if (this.TipoApertura)
        //            tipo = "Firma MOP";

        //        return tipo;
        //    }
        //}
    }


    public class LibroObrasSelectView
    {
        public int IdLod { get; set; }
        public string NombreLibroObra { get; set; }

        public string FechaApertura { get; set; }

        public string Usuario_Apertura { get; set; }

        public string Usuario_Creación { get; set; }

        public string rutaImg { get; set; }
    }

    public class LibroObrasUserView
    {
        public int IdContrato { get; set; }
        public string UserId { get; set; }
    }
}