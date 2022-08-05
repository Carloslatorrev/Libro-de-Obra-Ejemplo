
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LODapp.Models
{
    public class LOD_RolesCttosContrato
    {

        [PrimaryKey]        
        public int IdRCContrato { get; set; }
    
        public int IdContrato { get; set; }

        public string Contrato { get; set; }

        public int? IdRolCtto { get; set; }
        public string RolesContrato { get; set; }

        public string NombreRol { get; set; }
        public string Descripcion { get; set; }

        //[NotMapped]
        //public List<LOD_UsuariosLod> Usuarios { get; set; }

    }
}