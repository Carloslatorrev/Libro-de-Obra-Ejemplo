using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class UsuariosLodDBServices : DBMainServices
    {
        public static async Task<int> AddUserLod(LOD_UsuariosLod userlod)
        {
            await Init();
            var id = await db.InsertAsync(userlod);

            return id;
        }

        public static async Task RemoveUserLod(int id)
        {
            await Init();
            await db.DeleteAsync<LOD_UsuariosLod>(id);

        }

        public static async Task<List<LOD_UsuariosLod>> GetUserLod()
        {
            await Init();

            var cod = await db.Table<LOD_UsuariosLod>().ToListAsync();

            return cod;

        }

        public static async Task<LOD_UsuariosLod> FindUserLod(int id)
        {
            await Init();

            var cod = await db.Table<LOD_UsuariosLod>().Where(x => x.IdUsLod == id).FirstOrDefaultAsync();
            return cod;

        }

        public static async Task<List<LOD_UsuariosLod>> FindUserLodByContrato(List<int> IdLods)
        {
            await Init();

            var cod = await db.Table<LOD_UsuariosLod>().Where(x => IdLods.Contains(x.IdLod)).ToListAsync();

            return cod;

        }

        public static async Task<LOD_UsuariosLod> FindIdByUser(int IdLod, String UserId)
        {
            await Init();
            var cod = await db.Table<LOD_UsuariosLod>().Where(x => x.IdLod== IdLod && x.UserId == UserId).FirstOrDefaultAsync();

            return cod;

        }
        public static async Task<List<LOD_UsuariosLod>> GetReceptores(int IdLod, string UserID)
        {
            await Init();
            var cod = await db.Table<LOD_UsuariosLod>().Where(x => x.IdLod == IdLod && x.UserId != UserID).ToListAsync();

            return cod;

        }

    }


}
