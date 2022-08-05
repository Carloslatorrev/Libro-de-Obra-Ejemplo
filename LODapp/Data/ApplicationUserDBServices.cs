using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class ApplicationUserDBServices : DBMainServices
    {
        public static async Task<int> AddUser(ApplicationUser userlod)
        {
            await Init();
            var id = await db.InsertAsync(userlod);

            return id;
        }

        public static async Task RemoveUser(int id)
        {
            await Init();
            await db.DeleteAsync<ApplicationUser>(id);

        }

        public static async Task UpdateUser(ApplicationUser user)
        {
            await Init();
            await db.UpdateAsync(user);

        }

        public static async Task<List<ApplicationUser>> GetUser()
        {
            await Init();

            var cod = await db.Table<ApplicationUser>().ToListAsync();

            return cod;

        }

        public static async Task<ApplicationUser> FindUser(int id)
        {
            await Init();

            var cod = await db.Table<ApplicationUser>().Where(x => x.IdUserInt.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }

        public static async Task<ApplicationUser> FindUserString(string userid)
        {
            await Init();

            var cod = await db.Table<ApplicationUser>().Where(x => x.UserId.Equals(userid)).FirstOrDefaultAsync();

            return cod;

        }
    }
}
