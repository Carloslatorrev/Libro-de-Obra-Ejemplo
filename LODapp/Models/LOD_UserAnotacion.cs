using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LODapp.Models
{
    public class LOD_UserAnotacion
    {
        [PrimaryKey, AutoIncrement]
        public int IdUserAnot { get; set; }
        public int IdUsLod { get; set; }
        public string UsuarioLod { get; set; }

        public int IdAnotacion { get; set; }
        public string Anotacion { get; set; }

        public bool Destacado { get; set; }
        public string TempCode { get; set; }
        public bool Leido { get; set; }

        public bool EsPrincipal { get; set; }

        public bool EsRespRespuesta { get; set; }

        public bool RespVB { get; set; }

        public bool VistoBueno { get; set; }

        public DateTime? FechaVB { get; set; }

        public int? TipoVB { get; set; }
        public string RutaImg { get; set; }

        public int? Actualizado { get; set; }

        [Ignore]
        public LOD_Anotaciones AnotacionModel { get; set; }

        [Ignore]
        public ApplicationUser UserModel { get; set; }

        [Ignore]
        public LOD_UsuariosLod UserLod { get; set; }


        //[NotMapped]
        //public string RutaImg { get {
        //        if (LOD_UsuarioLod.ApplicationUser.RutaImagen == null)
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            return "/Images/Contactos/" + LOD_UsuarioLod.ApplicationUser.RutaImagen;
        //        }
        //    } }

        //[NotMapped]
        //public string Nombre { get {
        //        return LOD_UsuarioLod.ApplicationUser.Nombres.Split(' ')[0] + " " + LOD_UsuarioLod.ApplicationUser.Apellidos;
        //    } }
    }

    public class UserBorradorView
    {
        public int IdUsLod { get; set; }

        public int IdAnotacion { get; set; }

    }
}