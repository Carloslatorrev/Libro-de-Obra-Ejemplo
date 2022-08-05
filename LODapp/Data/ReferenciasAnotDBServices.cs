using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class ReferenciasAnotDBServices : DBMainServices
    {
        public static async Task<int> AddReferenciaAnot(LOD_ReferenciasAnot empresa)
        {
            await Init();
            var id = await db.InsertAsync(empresa);

            return id;
        }

        public static async Task RemoveReferenciaAnot(int id)
        {
            await Init();
            await db.DeleteAsync<LOD_ReferenciasAnot>(id);

        }

        public static async Task<List<LOD_ReferenciasAnot>> GetReferenciaAnot()
        {
            await Init();

            var cod = await db.Table<LOD_ReferenciasAnot>().ToListAsync();

            return cod;

        }

        public static async Task UpdateRefAnotacion(LOD_ReferenciasAnot anotacion)
        {
            await Init();
            await db.UpdateAsync(anotacion);

        }

        public static async Task<List<LOD_ReferenciasAnot>> GetReferenciaAnotNoCreadas()
        {
            await Init();

            var cod = await db.Table<LOD_ReferenciasAnot>().Where(x => x.Actualizado == 2).ToListAsync();

            return cod;

        }

        public static async Task<LOD_ReferenciasAnot> FindReferenciaAnot(int id)
        {
            await Init();

            var cod = await db.Table<LOD_ReferenciasAnot>().Where(x => x.IdRefAnot.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }

        public static async Task<List<LOD_ReferenciasAnot>> FindReferenciaAnotByAnotacion(int id)
        {
            await Init();

            var cod = await db.Table<LOD_ReferenciasAnot>().Where(x => x.IdAnotacion.Equals(id)).ToListAsync();

            return cod;

        }
    }
}
