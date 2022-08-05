using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LODapp.Models
{
    public class CON_Contratos
    {
        
        [PrimaryKey]
        public int IdContrato { get; set; }
              
        public string CodigoContrato { get; set; }
        public string NombreContrato { get; set; }

        public string DescripcionContrato { get; set; }
        public string RutaImagenContrato { get; set; }
        public DateTime? FechaCreacionContrato { get; set; }
        public string UserId { get; set; }
        public string UsuarioCreador { get; set; }
        public string NumeroResolucion { get; set; }
        public int? IdTipoContrato { get; set; }

        public int? IdDireccionContrato { get; set; }

        public string DireccionMOP { get; set; }

        public string Sucursal { get; set; }

        public int? IdModalidadContrato { get; set; }

        //[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? MontoInicialContrato { get; set; }    

        public DateTime? FechaInicioContrato { get; set; }

        public int? IdEmpresaContratista { get; set; }
        public string Empresa_Contratista { get; set; }

        public int? ModalidadReajuste { get; set; }
        public decimal? MontoVigenteContrato { get; set; }
        public int? PlazoInicialContrato { get; set; }
        public DateTime? FechaAdjudicacion { get; set; }
        public DateTime? FechaSubcripcion { get; set; }
        public decimal? MontoPresupuestado { get; set; }
        public decimal? MontoModTramite { get; set; }
        public int? PlazoVigente { get; set; }
        public bool? Activo { get; set; }
        public int? EstadoContrato { get; set; }

        public int? IdEmpresaFiscalizadora{ get; set; }

        public string Empresa_Fiscalizadora { get; set; }

        public int? Actualizado { get; set; }

        [Ignore]
        public bool ShowListLod { get; set; }

        [Ignore]
        public List<LOD_LibroObras> LibroObras { get; set; }
        [Ignore]
        public bool UsarRuta { get; set; }
        [Ignore]
        public bool UsarLocal { get; set; }
        [Ignore]
        public string rutaCompleta { get; set; }


    }

    public class ContratoSelectView
    {
        public int Id { get; set; }
        public string Numero { get; set; }

        public string Nombre { get; set; }

        public string EmpContratista { get; set; }

        public string DireccionMOP { get; set; }

        public string RutaImg { get; set; }
    }
}