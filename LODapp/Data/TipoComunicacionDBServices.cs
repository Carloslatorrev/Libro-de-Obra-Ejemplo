using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class TipoComunicacionDBServices : DBMainServices
    {
        public static async Task<int> AddTipoCom(MAE_TipoComunicacion tipocom)
        {
            await Init();
            var id = await db.InsertAsync(tipocom);

            return id;
        }

        public static async Task RemoveTipoCom(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_TipoComunicacion>(id);

        }

        public static async Task<List<MAE_TipoComunicacion>> GetTipoCom()
        {
            await Init();

            var cod = await db.Table<MAE_TipoComunicacion>().ToListAsync();

            return cod;

        }

        public static async Task<List<MAE_TipoComunicacion>> GetTipoComByLod(int IdTipo)
        {
            await Init();

            var cod = await db.Table<MAE_TipoComunicacion>().Where(x => x.IdTipoLod == IdTipo).ToListAsync();

            return cod;

        }

        public static async Task<MAE_TipoComunicacion> FindTipoCom(int id)
        {
            await Init();

            var cod = await db.Table<MAE_TipoComunicacion>().Where(x => x.IdTipoCom.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
