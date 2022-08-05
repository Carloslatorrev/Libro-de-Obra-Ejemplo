using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class TipoDocumentoDBServices : DBMainServices
    {
        public static async Task<int> AddTipoDoc(MAE_TipoDocumento tipodoc)
        {
            await Init();
            var id = await db.InsertAsync(tipodoc);

            return id;
        }

        public static async Task RemoveTipoDoc(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_TipoDocumento>(id);

        }

        public static async Task<List<MAE_TipoDocumento>> GetTipoDoc()
        {
            await Init();

            var cod = await db.Table<MAE_TipoDocumento>().ToListAsync();

            return cod;

        }

        public static async Task<MAE_TipoDocumento> FindTipoDoc(int id)
        {
            await Init();

            var cod = await db.Table<MAE_TipoDocumento>().Where(x => x.IdTipo.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
