using LODapp.Models;
using LODapp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class RolesContratoDBServices : DBMainServices
    {
        public async Task<int> AddRolesContrato(MAE_RolesContrato rolescontrato)
        {
            await Init();
            var id = await db.InsertAsync(rolescontrato);

            return id;
        }

        public async Task RemoveRolesContrato(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_RolesContrato>(id);

        }

        public async Task<List<MAE_RolesContrato>> GetRolesContrato()
        {
            await Init();

            var cod = await db.Table<MAE_RolesContrato>().ToListAsync();

            return cod;

        }

        public static async Task<MAE_RolesContrato> FindRolesContrato(int id)
        {
            await Init();

            var cod = await db.Table<MAE_RolesContrato>().Where(x => x.IdRolCtto.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
