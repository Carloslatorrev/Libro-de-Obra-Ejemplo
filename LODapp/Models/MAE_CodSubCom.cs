using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LODapp.Models
{
    public class MAE_CodSubCom
    {
        [PrimaryKey]
        public int IdControl { get; set; }

        public int IdTipoSub{ get; set; }

        public string SubtipoComunicacion { get; set; }

        public int IdTipo { get; set; }
        
        public string TipoDocumento { get; set; }

        //[DisplayName("Carpeta")]
        //[ForeignKey("MAE_Path")]
        //public int? IdPath { get; set; }
        //public virtual MAE_Path MAE_Path { get; set; }

        public bool Activo { get; set; }
        public bool Obligatorio { get; set; }

        //[NotMapped]
        //public int IdTipoCom { get; set; }
        //[NotMapped]
        //public virtual MAE_TipoComunicacion MAE_TipoComunicacion { get; set; }
        //[NotMapped]
        //public MAE_ClassDoc MAE_ClassDoc { get; set; }
    }
}