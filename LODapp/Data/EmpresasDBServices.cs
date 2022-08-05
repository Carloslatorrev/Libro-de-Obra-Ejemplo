using LODApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODApp.Data
{
    public class EmpresasDBServices : DBMainServices
    {
        public async Task<int> AddEmpresa(MAE_Empresa empresa)
        {
            await Init();
            var id = await db.InsertAsync(empresa);

            return id;
        }

        public async Task RemoveEmpresa(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_Empresa>(id);

        }

        public async Task<List<MAE_Empresa>> GetEmpresa()
        {
            await Init();

            var cod = await db.Table<MAE_Empresa>().ToListAsync();

            return cod;

        }

        public static async Task<MAE_Empresa> FindEmpresa(int id)
        {
            await Init();

            var cod = await db.Table<MAE_Empresa>().Where(x => x.IdEmpresa.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
