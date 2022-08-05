
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace LODApp.Models
{
	public class MAE_Sucursal
	{
		[PrimaryKey]
		public int IdSucursal { get; set; }

		public string Sucursal { get; set; }

		[ForeignKey(typeof(MAE_sujetoEconomico))]
		public int IdSujeto { get; set; }
		[OneToOne]
		public virtual MAE_sujetoEconomico MAE_sujetoEconomico { get; set; }

		[ForeignKey(typeof(MAE_ciudad))]
		public int IdCiudad { get; set; }
		[OneToOne]
		public virtual MAE_ciudad MAE_ciudad { get; set; }

		public string Encargado { get; set; }

		public string Direccion { get; set; }
		public string Telefono { get; set; }

		public string Email { get; set; }
		public bool EsCentral { get; set; }

		[ForeignKey(typeof(MAE_DireccionesMOP))]
		public int? IdDireccion { get; set; }
		[OneToOne]
		public MAE_DireccionesMOP MAE_DireccionesMOP {get; set;}



	}
}