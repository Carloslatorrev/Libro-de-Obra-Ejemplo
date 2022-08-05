using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class CodSubComDBServices : DBMainServices
    {
        public static async Task<int> AddCodSubCom(MAE_CodSubCom codsubcom)
        {
            await Init();
            var id = await db.InsertAsync(codsubcom);

            return id;
        }

        public static async Task RemoveCodSubCom(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_CodSubCom>(id);

        }

        public static async Task<List<MAE_CodSubCom>> GetCodSubCom()
        {
            await Init();

            var cod = await db.Table<MAE_CodSubCom>().ToListAsync();

            return cod;

        }

        public static async Task<MAE_CodSubCom> FindCodSubCom(int id)
        {
            await Init();

            var cod = await db.Table<MAE_CodSubCom>().Where(x => x.IdControl.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }

        public static async Task<List<MAE_CodSubCom>> FindCodSubComBySubtipo(int id)
        {
            await Init();

            var cod = await db.Table<MAE_CodSubCom>().Where(x => x.IdTipoSub.Equals(id)).ToListAsync();

            return cod;

        }
    }
}
