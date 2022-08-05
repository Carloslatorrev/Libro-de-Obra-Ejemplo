using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace LODapp.Models
{
    public class LOD_Anotaciones
    {
        [PrimaryKey]
        public int IdAnotacion { get; set; }

        public string Titulo { get; set; }

        public int IdLod { get; set; }
        public string LibroObras { get; set; }


        public int IdTipoSub { get; set; }

        public String SubtipoComunicacion { get; set; }

        public int Correlativo { get; set; }

        public bool EsEstructurada { get; set; }
        public string Cuerpo { get; set; }

        public int Estado { get; set; }

        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaPub { get; set; }
        public DateTime? FechaFirma { get; set; }

        public bool SolicitudRest { get; set; }
        public DateTime? FechaResp { get; set; }
        public DateTime? FechaTopeRespuesta { get; set; }
        
        public string UserId { get; set; }

        public string UsuarioRemitente { get; set; }

        public bool SolicitudVB { get; set; }
        public int TipoFirma { get; set; }
        public bool EstadoFirma { get; set; }

        public string UserIdBorrador { get; set; }

        public string UsuarioBorrador { get; set; }

        public string TempCode { get; set; }

        public int? Actualizado { get; set; }

        public string RutaPdfSinFirma { get; set; }
        public string RutaPdfConFirma { get; set; }

        [Ignore]
        public bool Favorito { get; set; }
        [Ignore]
        public bool Favorito2 { get; set; }
        [Ignore]
        public bool Borrador { get; set; }
        [Ignore]
        public bool Publicada { get; set; }
        [Ignore]
        public bool PendienteFirma { get; set; }
        [Ignore]
        public bool DestacadaOn { get; set; }
        [Ignore]
        public bool DestacadaOff { get; set; }

        //public List<LOD_docAnotacion> LOD_DocAnotacion { get; set; }


    }

    public class AnotacionUserView
    {
        public string UserID { get; set; }
        public int IdLod { get; set; }
    }

    public class VistoBuenoView
    {
        public int IdUsLod { get; set; }
        public bool RespVB { get; set; }
        public int IdAnotacion { get; set; }
        public string NombreCompleto { get; set; }
        public bool VistoBueno { get; set; }
        public int? TipoVB { get; set; }

    }

    public class AnotacionesCreate
    {
        public string titulo { get; set; }
        public string cuerpo { get; set; }
        public string UserId { get; set; }
        public int IdSubTipo { get; set; }
        public int IdLod { get; set; }

        public bool SolicitudResp { get; set; }
        public DateTime? FechaSolicitud { get; set; }

        public bool SolicitudVB { get; set; }


    }

    public class UpdateAnotacion
    {
        public int IdAnotacion { get; set; }
        public string titulo { get; set; }
        public string cuerpo { get; set; }
        public int IdSubTipo { get; set; }
        public int IdLod { get; set; }
        public bool SolicitudResp { get; set; }
        public DateTime? FechaSolicitud { get; set; }


    }

    public class ReceptoresView
    {
        public int IdUsLod { get; set; }

        public int IdAnotacion { get; set; }

        public bool EsPrincipal { get; set; }

        public bool EsRespRespuesta { get; set; }

    }

    public class CreateReferencia
    {
        public int IdAnontacionRef { get; set; }
        public int IdAnotacion { get; set; }
        public bool EsRepuesta { get; set; }
        public string Observacion { get; set; }
    }

    public class FirmarAnotacion
    {
        public string password { get; set; }
        public int IdAnotacion { get; set; }
        public string UserId { get; set; }
    }

    public class SolicitudFirma
    {
        public int IdAnotacion { get; set; }

        public int IdFirmante { get; set; }
    }

    public class VBConfirmedView
    {
        public int IdUsLod { get; set; }
        public int IdAnotacion { get; set; }
    }

}