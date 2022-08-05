using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LODApp.Models
{
    public class MAE_DireccionesMOP
    {
        [PrimaryKey]
        public int IdDireccion { get; set; }

        public string NombreDireccion { get; set; }

        public string DescripcionDireccion { get; set; }
    }
}