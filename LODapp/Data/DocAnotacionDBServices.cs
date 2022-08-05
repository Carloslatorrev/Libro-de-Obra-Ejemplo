using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class DocAnotacionDBServices : DBMainServices
    {
        public static async Task<int> AddDocAnotacion(LOD_docAnotacion docanot)
        {
            await Init();
            var id = await db.InsertAsync(docanot);

            return id;
        }

        public static async Task RemoveDocAnotacion(int id)
        {
            await Init();
            await db.DeleteAsync<LOD_docAnotacion>(id);

        }

        public static async Task<int> UpdateDocAnotacion(LOD_docAnotacion anotacion)
        {
            await Init();
            int rows = await db.UpdateAsync(anotacion);
            return rows;
        }

        public static async Task<List<LOD_docAnotacion>> GetDocAnotacion()
        {
            await Init();

            var cod = await db.Table<LOD_docAnotacion>().ToListAsync();

            return cod;

        }

        public static async Task<LOD_docAnotacion> FindDocAnotacion(int id)
        {
            await Init();

            var cod = await db.Table<LOD_docAnotacion>().Where(x => x.IdDocAnotacion.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }

        public static async Task<List<LOD_docAnotacion>> FindDocAnotacionByAnotacion(int id)
        {
            await Init();

            var cod = await db.Table<LOD_docAnotacion>().Where(x => x.IdAnotacion.Equals(id)).ToListAsync();

            return cod;

        }


        public static async Task<List<LOD_docAnotacion>> GetDocumentosNoCreados()
        {
            await Init();

            var cod = await db.Table<LOD_docAnotacion>().Where(x => x.Actualizado == 2).ToListAsync();

            return cod;

        }

        public static async Task<List<LOD_docAnotacion>> GetDocumentosNoActualizados()
        {
            await Init();

            var cod = await db.Table<LOD_docAnotacion>().Where(x => x.Actualizado == 2).ToListAsync();

            return cod;

        }
    }
}
