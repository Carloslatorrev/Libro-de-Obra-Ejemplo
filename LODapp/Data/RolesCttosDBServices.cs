using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class EmpresasDBServices : DBMainServices
    {
        public async Task<int> AddRolesCtto(LOD_RolesCttosContrato rolesctto)
        {
            await Init();
            var id = await db.InsertAsync(rolesctto);

            return id;
        }

        public async Task RemoveRolesCtto(int id)
        {
            await Init();
            await db.DeleteAsync<LOD_RolesCttosContrato>(id);

        }

        public async Task<List<LOD_RolesCttosContrato>> GetRolesCttos()
        {
            await Init();

            var cod = await db.Table<LOD_RolesCttosContrato>().ToListAsync();

            return cod;

        }

        public static async Task<LOD_RolesCttosContrato> FindRolesCtto(int id)
        {
            await Init();

            var cod = await db.Table<LOD_RolesCttosContrato>().Where(x => x.IdRCContrato.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
