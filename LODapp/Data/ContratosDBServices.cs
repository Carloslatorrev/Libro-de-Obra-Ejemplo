using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class ContratosDBServices : DBMainServices
    {
        public static async Task<int> AddContrato(CON_Contratos contrato)
        {
            await Init();
            var id = await db.InsertAsync(contrato);

            return id;
        }

        public static async Task RemoveContrato(int id)
        {
            await Init();
            await db.DeleteAsync<CON_Contratos>(id);

        }

        public static async Task<List<CON_Contratos>> GetContratos()
        {
            await Init();

            var con = await db.Table<CON_Contratos>().ToListAsync();

            return con;

        }

        public static async Task<CON_Contratos> FindContrato(int id)
        {
            await Init();

            var con = await db.Table<CON_Contratos>().Where(x => x.IdContrato.Equals(id)).FirstOrDefaultAsync();

            return con;

        }

        public static async Task<List<CON_Contratos>> SearchContrato(string text)
        {
            await Init();

            var con = await db.Table<CON_Contratos>().Where(x => x.CodigoContrato.Equals(text) || x.NombreContrato.Equals(text) || x.DescripcionContrato.Equals(text) || x.CodigoContrato.Contains(text) || x.NombreContrato.Contains(text) || x.DescripcionContrato.Contains(text)).ToListAsync();

            return con;

        }
    }
}
