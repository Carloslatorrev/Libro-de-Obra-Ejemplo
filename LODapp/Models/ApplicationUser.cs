using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LODapp.Models
{
    public class ApplicationUser
    {
        [PrimaryKey, AutoIncrement]
        public int IdUserInt { get; set; }
        public string UserId { get; set; }
        public bool Activo { get; set; }
        public int IdSucursal { get; set; }
        public string Run { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Movil { get; set; }
        public string AnexoEmpresa { get; set; }
        public string CargoContacto { get; set; }
        public string DataLetters { get; set; }
        public string RutaImagen { get; set; }
        public string IdCertificado { get; set; }
        public string Email { get; set; }
        public bool RememberData { get; set; }
        public string Password { get; set; }
    }

    public class ApplicationUserView
    {
        public string UserId { get; set; }
        public bool Activo { get; set; }
        public int IdSucursal { get; set; }
        public string Run { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Movil { get; set; }
        public string AnexoEmpresa { get; set; }
        public string CargoContacto { get; set; }
        public string DataLetters { get; set; }
        public string RutaImagen { get; set; }
        public string IdCertificado { get; set; }
        public string Email { get; set; }
    }

}
