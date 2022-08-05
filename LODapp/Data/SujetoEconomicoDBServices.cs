using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class SujetoEconomicoDBServices : DBMainServices
    {
        public async Task<int> AddSujeto(MAE_sujetoEconomico sujeto)
        {
            await Init();
            var id = await db.InsertAsync(sujeto);

            return id;
        }

        public async Task RemoveSujeto(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_sujetoEconomico>(id);

        }

        public async Task<List<MAE_sujetoEconomico>> GetSujetos()
        {
            await Init();

            var cod = await db.Table<MAE_sujetoEconomico>().ToListAsync();

            return cod;

        }

        public static async Task<MAE_sujetoEconomico> FindSujetos(int id)
        {
            await Init();

            var cod = await db.Table<MAE_sujetoEconomico>().Where(x => x.IdSujEcon.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
