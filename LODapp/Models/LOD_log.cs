
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LODapp.Models
{
    public class LOD_log
    {
        [PrimaryKey]
        public int IdLog { get; set; }
        public string Objeto { get; set; }
        public int IdObjeto { get; set; }
        public DateTime FechaLog { get; set; }

        public string UserId { get; set; }
        public string UserAccion { get; set; }
        public string Accion { get; set; }
        public string Campo { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorActualizado { get; set; }

        //[NotMapped]
        //public string NombUser { get; set; }

        //[NotMapped]
        //public LOD_Anotaciones anotacion { get; set; }

    }
}