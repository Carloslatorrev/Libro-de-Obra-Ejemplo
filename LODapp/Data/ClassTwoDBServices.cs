using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class ClassTwoDBServices : DBMainServices
    {
        public static async Task<int> AddClassTwo(MAE_ClassTwo classtwo)
        {
            await Init();
            var id = await db.InsertAsync(classtwo);

            return id;
        }

        public static async Task RemoveClassTwo(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_ClassTwo>(id);

        }

        public static async Task<List<MAE_ClassTwo>> GetClassTwo()
        {
            await Init();

            var classtwo = await db.Table<MAE_ClassTwo>().ToListAsync();

            return classtwo;

        }

        public static async Task<List<MAE_ClassTwo>> GetClassTwo(int IdClassOne)
        {
            await Init();

            var classtwo = await db.Table<MAE_ClassTwo>().Where(x => x.IdClassOne == IdClassOne).ToListAsync();

            return classtwo;

        }

        public static async Task<MAE_ClassTwo> FindClasTwo(int id)
        {
            await Init();

            var classtwo = await db.Table<MAE_ClassTwo>().Where(x => x.IdClassTwo.Equals(id)).FirstOrDefaultAsync();

            return classtwo;

        }
    }
}
