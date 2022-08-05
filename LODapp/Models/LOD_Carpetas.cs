using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace LODApp.Models
{
    public class LOD_Carpetas
    {

        public Nullable<int> IdCarpPadre { get; set; }
        public Nullable<int> IdContrato { get; set; }
        [PrimaryKey]
        public int IdCarpeta { get; set; }
        public string NombreCarpeta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UserId { get; set; }
        public bool EsPortafolio { get; set; }

        [OneToMany]
        public virtual List<CON_Contratos> CON_Contratos { get; set; }

        //[NotMapped]
        //public string Creador { get; set; }
    }
}