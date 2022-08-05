using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class TipoLODDBServices : DBMainServices
    {
        public async Task<int> AddTipoLod(MAE_TipoLOD tipolod)
        {
            await Init();
            var id = await db.InsertAsync(tipolod);

            return id;
        }

        public async Task RemoveTipoLod(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_TipoLOD>(id);

        }

        public async Task<List<MAE_TipoLOD>> GetTipoLod()
        {
            await Init();

            var cod = await db.Table<MAE_TipoLOD>().ToListAsync();

            return cod;

        }

        public static async Task<MAE_TipoLOD> FindTipoLod(int id)
        {
            await Init();

            var cod = await db.Table<MAE_TipoLOD>().Where(x => x.IdTipoLod.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
