using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LODapp.Models
{
    public class LOD_ReferenciasAnot
    {
        [PrimaryKey]
        public int IdRefAnot { get; set; }
        public int IdAnotacion { get; set; }
        public string AnotacionOrigen { get; set; }

        public int IdAnontacionRef { get; set; }
        public string AnotacionReferencia { get; set; }

        public bool EsRepuesta { get; set; }

        public string Observacion { get; set; }

        public int? Actualizado { get; set; }

        [Ignore]
        public LOD_Anotaciones AnotacionRef { get; set; }
    }
}