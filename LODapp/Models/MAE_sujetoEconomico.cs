using LODapp.Helpers;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace LODapp.Models
{
    public class MAE_sujetoEconomico
    {

		[PrimaryKey]
        public int IdSujEcon { get; set; }

		[RutValido]
		public string Rut { get; set; }

        public string RazonSocial { get; set; }

        public string NomFantasia { get; set; }

		public string Giro { get; set; }

		public string Direccion { get; set; }

		public string Telefono { get; set; }

		public string email { get; set; }

		public string web { get; set; }


		public string UrlFacebook { get; set; }

		public string UrlTwitter { get; set; }

		public string UrlLinkedin { get; set; }

		public string Ciudad { get; set; }

		public bool EsMandante { get; set; }

		public bool EsContratista { get; set; }

        public bool EsGubernamental { get; set; }

        public DateTime FechaCreacion { get; set; }
		public string RutaImagen { get; set; }
		public bool Activo { get; set; }

		public string DataLetters { get; set; }


        //public List<MAE_Sucursal> Sucursales { get; set; }

        //[NotMapped]
        //public HttpPostedFile fileImage { get; set; }

        //[NotMapped]
        //public string GetCiudad
        //{
        //    get
        //    {
        //        return (MAE_ciudad != null) ? MAE_ciudad.Ciudad : "-";
        //    }
        //}

        //[NotMapped]
        //public string Iniciales
        //{
        //    get
        //    {
        //        return Data_Letters.ImageLetter(this.RazonSocial);
        //    }
        //}
        //[NotMapped]
        //public SelectDropdown SelectDropdown
        //{
        //    get
        //    {
        //        SelectDropdown d = new SelectDropdown()
        //        {
        //            value = this.IdSujEcon.ToString(),
        //            text = this.RazonSocial,
        //            image = (String.IsNullOrEmpty(this.RutaImagen) ? null : "/Images/Sujetos/" + this.RutaImagen),
        //            description = this.GetCiudad,
        //            shortext = this.Iniciales,
        //            dataletters = this.DataLetters
        //        };

        //        return d;
        //    }
        //}
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    //LOD_DB db = new LOD_DB();
        //    var errores = new List<ValidationResult>();
        //    //var existeRut = db.MAE_sujetoEconomico.Where(s => s.Rut == Rut).FirstOrDefault();
        //    //if (existeRut != null) {
        //    //	errores.Add(new ValidationResult("El Rut ya se encuentra ingresado",new string[] { "Rut" }));
        //    //}
        //    return errores;
        //}


    }
}


  


  
