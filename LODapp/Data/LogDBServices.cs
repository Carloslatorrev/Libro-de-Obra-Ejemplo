using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class LogDBServices : DBMainServices
    {
        public static async Task<int> AddLog(LOD_log log)
        {
            await Init();
            var id = await db.InsertAsync(log);

            return id;
        }

        public static async Task RemoveLog(int id)
        {
            await Init();
            await db.DeleteAsync<LOD_log>(id);

        }

        public static async Task<List<LOD_log>> GetLogs()
        {
            await Init();

            var cod = await db.Table<LOD_log>().ToListAsync();

            return cod;

        }

        public static async Task<LOD_log> FindLog(int id)
        {
            await Init();

            var cod = await db.Table<LOD_log>().Where(x => x.IdLog.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }

        public static async Task<List<LOD_log>> FindLogByObjeto(int id)
        {
            await Init();

            var cod = await db.Table<LOD_log>().Where(x => x.IdObjeto.Equals(id)).ToListAsync();

            return cod;

        }
    }
}
