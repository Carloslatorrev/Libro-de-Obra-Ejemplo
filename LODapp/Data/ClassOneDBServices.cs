using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class ClassOneDBServices : DBMainServices
    {
        public static async Task<int> AddClassOne(MAE_ClassOne classone)
        {
            await Init();
            var id = await db.InsertAsync(classone);

            return id;
        }

        public static async Task RemoveClassOne(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_ClassOne>(id);

        }

        public static async Task<List<MAE_ClassOne>> GetClassOne()
        {
            await Init();

            var classone = await db.Table<MAE_ClassOne>().ToListAsync();

            return classone;

        }

        public static async Task<MAE_ClassOne> FindClassOne(int id)
        {
            await Init();

            var classone = await db.Table<MAE_ClassOne>().Where(x => x.IdClassOne.Equals(id)).FirstOrDefaultAsync();

            return classone;

        }
    }
}
