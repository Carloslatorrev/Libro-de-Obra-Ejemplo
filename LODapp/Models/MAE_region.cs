using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LODApp.Models
{
    public class MAE_region
    {
        [PrimaryKey]
        public int IdRegion { get; set; }
        public string Pais { get; set; }
        public string Region { get; set; }

        [OneToMany]
        public List<MAE_ciudad> ciudades { get; set; }
    }
}