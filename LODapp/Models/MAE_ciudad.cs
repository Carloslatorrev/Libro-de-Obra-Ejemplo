
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LODApp.Models
{
    public class MAE_ciudad
    {
        [PrimaryKey]
		public int IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public string ZonaHoraria { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }



        [ForeignKey(typeof(MAE_region))]
        public int? IdRegion { get; set; }
        [OneToOne]
        public MAE_region MAE_region { get; set; }


    }
}