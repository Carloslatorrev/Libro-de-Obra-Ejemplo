
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LODapp.Models
{
    public class LOD_PermisosRolesContrato
    {

        [PrimaryKey]        
        public int IdPermiso { get; set; }

        public int? IdRCContrato { get; set; }

        public string RolesCttosContrato { get; set; }

        public int IdLod { get; set; }
        public string LibroObras { get; set; }

        public bool Lectura { get; set; }
        public bool Escritura { get; set; }
        public bool FirmaGob { get; set; }
        public bool FirmaFea { get; set; }
        public bool FirmaSimple { get; set; }

        //[NotMapped]
        //public int Indice { get; set; }
        //[NotMapped]
        //public int SubIndice { get; set; }

    }
}