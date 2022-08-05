using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class DocumentosDBServices : DBMainServices
    {
        public async Task<int> AddDocumento(MAE_documentos documentos)
        {
            await Init();
            var id = await db.InsertAsync(documentos);

            return id;
        }

        public async Task RemoveDocumento(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_documentos>(id);

        }

        public async Task<List<MAE_documentos>> GetDocumentos()
        {
            await Init();

            var cod = await db.Table<MAE_documentos>().ToListAsync();

            return cod;

        }

        public static async Task<MAE_documentos> FindDocumento(int id)
        {
            await Init();

            var cod = await db.Table<MAE_documentos>().Where(x => x.IdDoc.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
