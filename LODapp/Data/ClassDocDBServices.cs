using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class ClassDocDBServices : DBMainServices
    {
        public static async Task<int> AddClassDoc(MAE_ClassDoc classdoc)
        {
            await Init();
            var id = await db.InsertAsync(classdoc);

            return id;
        }

        public static async Task RemoveClassDoc(int id)
        {
            await Init();
            await db.DeleteAsync<MAE_ClassDoc>(id);

        }

        public static async Task<List<MAE_ClassDoc>> GetClassDoc()
        {
            await Init();

            var classdoc = await db.Table<MAE_ClassDoc>().ToListAsync();

            return classdoc;

        }

        public static async Task<List<MAE_ClassDoc>> GetClassDoc(int IdClassTwo)
        {
            await Init();

            var classdoc = await db.Table<MAE_ClassDoc>().Where(x => x.IdClassTwo == IdClassTwo).ToListAsync();

            return classdoc;

        }

        public static async Task<MAE_ClassDoc> FindClassDoc(int id)
        {
            await Init();

            var classdoc = await db.Table<MAE_ClassDoc>().Where(x => x.IdClassDoc.Equals(id)).FirstOrDefaultAsync();

            return classdoc;

        }
    }
}
