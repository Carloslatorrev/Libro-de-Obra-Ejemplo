using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class LibroObrasDBServices : DBMainServices
    {
        public static async Task<int> AddLibroObras(LOD_LibroObras libro)
        {
            await Init();
            var id = await db.InsertAsync(libro);

            return id;
        }

        public static async Task RemoveLibroObras(int id)
        {
            await Init();
            await db.DeleteAsync<LOD_LibroObras>(id);

        }

        public static async Task<List<LOD_LibroObras>> GetLibroObras()
        {
            await Init();

            var cod = await db.Table<LOD_LibroObras>().ToListAsync();

            return cod;

        }

        public static async Task<List<LOD_LibroObras>> GetLibroObrasByContrato(int id)
        {
            await Init();

            var cod = await db.Table<LOD_LibroObras>().Where(x => x.IdContrato == id).ToListAsync();
            
            return cod;

        }


        public static async Task<LOD_LibroObras> FindLibro(int id)
        {
            await Init();

            var cod = await db.Table<LOD_LibroObras>().Where(x => x.IdLod.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }

        public static async Task<List<LOD_LibroObras>> SearchLibro(string query)
        {
            await Init();

            var cod = await db.Table<LOD_LibroObras>().Where(x => x.NombreLibroObra.Contains(query) || x.CodigoLObras.Contains(query)).ToListAsync();

            return cod;

        }


        public static async Task<List<LOD_LibroObras>> FindByContratoLibro(int id)
        {
            await Init();
            var cod = await db.Table<LOD_LibroObras>().Where(x => x.IdContrato.Equals(id)).ToListAsync();

            return cod;

        }

    }
}
