﻿using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LODApp.Models
{
    public class MAE_Empresa
    {
        
        [PrimaryKey]
        public int IdEmpresa { get; set; }
        public bool EmpresaLiderGrupo { get; set; }

        public string Rut { get; set; }

        public string LogoData { get; set; }

        //[NotMapped]
        //public HttpPostedFileBase TypeImageFile { get; set; }

        public string Razon { get; set; }

        public string Direccion { get; set; }

        public string Giro { get; set; }

        public string NomFantasia { get; set; }

        public string Telefono { get; set; }

        public string Web { get; set; }

        //[ForeignKey("MAE_tipoCtaEmails")]
        //public int? IdTCMail { get; set; }
        //public virtual MAE_tipoCtaEmails MAE_tipoCtaEmails { get; set; }

        public string Email { get; set; }

        public string EmailContacto { get; set; }

        public string PassEmail { get; set; }

        public int PuertoSalida { get; set; }

        public string UriServer { get; set; }

        public bool IsActive { get; set; }
        public bool IsSSL { get; set; }

        public int RequiredLength { get; set; }

        public bool RequireNonLetterOrDigit { get; set; }

        public bool RequireDigit { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireUppercase { get; set; }

        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }

        public int DefaultAccountLockoutTimeSpan { get; set; }

        public int IdCiudad { get; set; }

    }

    //public class DatosEmpresaView
    //{
    //    public int IdEmpresa { get; set; }

    //    [Display(Name = "Empresa Lider Grupo")]
    //    public bool EmpresaLiderGrupo { get; set; }
    //    public string LogoData { get; set; }

    //    [Required]
    //    public string Rut { get; set; }
    //    public string Email { get; set; }
    //    public string Giro { get; set; }
    //    [Display(Name = "Razón Social")]
    //    [Required(ErrorMessage = "Dato Obligatorio")]
    //    public string Razon { get; set; }
    //    [NotMapped]
    //    public HttpPostedFileBase fileImage { get; set; }

    //    [Display(Name = "Dirección")]
    //    [Required(ErrorMessage = "Dato Obligatorio")]
    //    public string Direccion { get; set; }

    //    [Display(Name = "Nombres de Fantasia")]
    //    public string NomFantasia { get; set; }

    //    [Display(Name = "Correo Contacto")]
    //    public string EmailContacto { get; set; }

    //    [Display(Name = "Teléfono")]
    //    public string Telefono { get; set; }

    //    [Display(Name = "Sitio Web")]
    //    public string Web { get; set; }

    //    public string SharepointUrl { get; set; }
    //    public string SPLibrary { get; set; }
    //    public string SPDefault { get; set; }
    //    public string SPUser { get; set; }
    //    public string SPPassword { get; set; }


    //}

    //public class CorreoEmpresaView
    //{
    //    public int IdEmpresa { get; set; }

    //    public int IdTCMail { get; set; }

    //    [DataType(DataType.EmailAddress)]
    //    public string Email { get; set; }

    //    [Display(Name = "Password")]
    //    [DataType(DataType.Password)]
    //    public string PassEmail { get; set; }

    //    [Display(Name = "Uri Servidor")]
    //    public string UriServer { get; set; }

    //    [Display(Name = "Puerto Salida")]
    //    public int PuertoSalida { get; set; }

    //    [Display(Name = "Requiere SSL")]
    //    public bool IsSSL { get; set; }


    //    public bool IsActive { get; set; }
    //}

    //public class SeguridadEmpresaView
    //{
    //    public int IdEmpresa { get; set; }

    //    [Required(ErrorMessage = "Dato obligatorio")]
    //    [Range(6, 50, ErrorMessage = "Ingrese un valor entre 6 y 50")]
    //    [Display(Name = "Largo Mínimo del Password")]
    //    public int RequiredLength { get; set; }

    //    [Required(ErrorMessage = "Dato obligatorio")]
    //    [Range(3, 10, ErrorMessage = "Ingrese un valor entre 3 y 10")]
    //    [Display(Name = "Reintentos máximos antes del bloqueo de la cuenta (mínimo 3)")]
    //    public int MaxFailedAccessAttemptsBeforeLockout { get; set; }

    //    [Required(ErrorMessage = "Dato obligatorio")]
    //    [Range(3, 1440, ErrorMessage = "Ingrese un valor entre 3 y 1440")]
    //    [Display(Name = "Duración en minutos del bloqueo de la cuenta (de 3 min. a 24 hrs")]
    //    public int DefaultAccountLockoutTimeSpan { get; set; }

    //    [Display(Name = "Requiere un caracter que no sea letra o número")]
    //    public bool RequireNonLetterOrDigit { get; set; }

    //    [Display(Name = "Requiere por lo menos un número")]
    //    public bool RequireDigit { get; set; }

    //    [Display(Name = "Requiere por lo menos una minúscula")]
    //    public bool RequireLowercase { get; set; }

    //    [Display(Name = "Requiere por lo menos una mayúscula")]
    //    public bool RequireUppercase { get; set; }
    //}
}