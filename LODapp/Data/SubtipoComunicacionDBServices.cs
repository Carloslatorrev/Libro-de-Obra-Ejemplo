using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class SubtipoComunicacionDBServices : DBMainServices
    {
        public static async Task<int> AddSubtipo(MAE_SubtipoComunicacion subtipo)
        {
            await Init();
            var id = await db.InsertAsync(subtipo);

            return id;
        }

        public static async Task RemoveSubtipo(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_SubtipoComunicacion>(id);

        }

        public static async Task<List<MAE_SubtipoComunicacion>> GetSubtipos()
        {
            await Init();

            var cod = await db.Table<MAE_SubtipoComunicacion>().ToListAsync();

            return cod;

        }

        public static async Task<List<MAE_SubtipoComunicacion>> GetSubtiposByTipo(int IdTipo)
        {
            await Init();

            var cod = await db.Table<MAE_SubtipoComunicacion>().Where(x => x.IdTipoCom == IdTipo).ToListAsync();

            return cod;

        }

        public static async Task<List<MAE_SubtipoComunicacion>> GetSubtiposByLod(int IdTipo)
        {
            await Init();
            var listTipoCom = await db.Table<MAE_TipoComunicacion>().Where(x => x.IdTipoLod == IdTipo).ToListAsync();
            List<int> listInt = listTipoCom.Select(x => x.IdTipoCom).ToList();
            var cod = await db.Table<MAE_SubtipoComunicacion>().Where(x => listInt.Contains(x.IdTipoCom)).ToListAsync();

            return cod;

        }


        public static async Task<MAE_SubtipoComunicacion> FindSubtipo(int id)
        {
            await Init();

            var cod = await db.Table<MAE_SubtipoComunicacion>().Where(x => x.IdTipoSub.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
