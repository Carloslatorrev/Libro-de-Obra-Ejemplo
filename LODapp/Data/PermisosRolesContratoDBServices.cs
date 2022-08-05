using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class PermisosRolesContratoDBServices : DBMainServices
    {
        public async Task<int> AddPermisoRol(LOD_PermisosRolesContrato permisosroles)
        {
            await Init();
            var id = await db.InsertAsync(permisosroles);

            return id;
        }

        public async Task RemovePermisoRol(int id)
        {
            await Init();
            await db.DeleteAsync<LOD_PermisosRolesContrato>(id);

        }

        public async Task<List<LOD_PermisosRolesContrato>> GetPermisoRol()
        {
            await Init();

            var cod = await db.Table<LOD_PermisosRolesContrato>().ToListAsync();

            return cod;

        }

        public static async Task<LOD_PermisosRolesContrato> FindPermisoRol(int id)
        {
            await Init();

            var cod = await db.Table<LOD_PermisosRolesContrato>().Where(x => x.IdPermiso.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
