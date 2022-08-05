using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LODApp.Models
{
	public class MAE_moneda
	{
		[PrimaryKey]
		public int IdMoneda { get; set; }
		[Required(ErrorMessage = "Dato obligatorio")]
		public string NomMoneda { get; set; }
		[Required(ErrorMessage = "Dato obligatorio")]
		public string Abreviatura { get; set; }
	}
}