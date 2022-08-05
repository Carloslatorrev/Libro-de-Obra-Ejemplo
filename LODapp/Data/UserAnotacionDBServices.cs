using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class UserAnotacionDBServices : DBMainServices
    {
        public static async Task<int> AddUserAnotacion(LOD_UserAnotacion useranot)
        {
            await Init();
            var id = await db.InsertAsync(useranot);

            return id;
        }

        public static async Task UpdateUserAnotacion(LOD_UserAnotacion useranot)
        {
            await Init();
            await db.UpdateAsync(useranot);

        }

        public static async Task RemoveUserAnotacion(int id)
        {
            await Init();
            await db.DeleteAsync<LOD_UserAnotacion>(id);

        }

        public static async Task<List<LOD_UserAnotacion>> GetUserAnotacion()
        {
            await Init();

            var cod = await db.Table<LOD_UserAnotacion>().ToListAsync();

            return cod;

        }

        public static async Task<LOD_UserAnotacion> FindUserAnotacion(int id)
        {
            await Init();

            var cod = await db.Table<LOD_UserAnotacion>().Where(x => x.IdUserAnot.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }

        public static async Task<LOD_UserAnotacion> FindUserAnotacion(int IdUsLod, int IdAnotacion)
        {
            await Init();

            var cod = await db.Table<LOD_UserAnotacion>().Where(x => x.IdUsLod.Equals(IdUsLod) && x.IdAnotacion.Equals(IdAnotacion)).FirstOrDefaultAsync();

            return cod;

        }

        public static async Task<LOD_UserAnotacion> FindUserAnotacionByUserLod(int id)
        {
            await Init();

            var cod = await db.Table<LOD_UserAnotacion>().Where(x => x.IdUsLod.Equals(id)).FirstOrDefaultAsync();

            return cod;

        }

        public static async Task<List<LOD_UserAnotacion>> FindUserAnotacionByAnot(int id)
        {
            await Init();

            var cod = await db.Table<LOD_UserAnotacion>().Where(x => x.IdAnotacion.Equals(id)).ToListAsync();

            return cod;

        }

        public static async Task<List<LOD_UserAnotacion>> FindReceptoresByAnot(int id)
        {
            await Init();

            var cod = await db.Table<LOD_UserAnotacion>().Where(x => x.IdAnotacion.Equals(id) && x.RespVB).ToListAsync();

            return cod;

        }


        public static async Task<List<LOD_Anotaciones>> FindAnotacionDestacada(int IdLod, int IdUsLod)
        {
            await Init();

            List<int> lista = await AnotacionesDBServices.FindIdAnotacionByLod(IdLod);
            List<LOD_Anotaciones> listado = new List<LOD_Anotaciones>();

            var cod = await db.Table<LOD_UserAnotacion>().Where(x => lista.Contains(x.IdAnotacion) && x.Destacado && x.IdUsLod == IdUsLod).ToListAsync();
            var listInt = cod.Select(x => x.IdAnotacion).ToList();
            foreach (var item in listInt)
            {
                listado.Add(await AnotacionesDBServices.FindAnotacion(item));
            }

            return listado;

        }

        public static async Task<List<LOD_Anotaciones>> FindNombradoEn(int IdLod, string UserId)
        {
            await Init();

            List<int> lista = await AnotacionesDBServices.FindIdAnotacionByLod(IdLod);
            LOD_UsuariosLod UserLod = await UsuariosLodDBServices.FindIdByUser(IdLod, UserId);
            List<LOD_Anotaciones> listado = new List<LOD_Anotaciones>();

            var cod = await db.Table<LOD_UserAnotacion>().Where(x => x.IdUsLod == UserLod.IdUsLod).ToListAsync();
            var listInt = cod.Select(x => x.IdAnotacion).ToList();
            foreach (var item in listInt)
            {
                listado.Add(await AnotacionesDBServices.FindAnotacionPublicada(item));
            }

            return listado;
        }


        public static async Task<List<LOD_Anotaciones>> FindIdAnotacionesReferencias(LOD_UsuariosLod UserLod, List<int> IdAnotaciones)
        {
            await Init();
            List<LOD_Anotaciones> listado = new List<LOD_Anotaciones>();

            var cod = await db.Table<LOD_UserAnotacion>().Where(x => x.IdUsLod == UserLod.IdUsLod && IdAnotaciones.Contains(x.IdAnotacion) && x.EsRespRespuesta).ToListAsync();
            var listInt = cod.Select(x => x.IdAnotacion).ToList();
            foreach (var item in listInt)
            {
                listado.Add(await AnotacionesDBServices.FindAnotacionPublicada(item));
            }

            return listado;
        }

        public static async Task<List<LOD_UserAnotacion>> GetReceptoresNoCreados()
        {
            await Init();

            var cod = await db.Table<LOD_UserAnotacion>().Where(x => x.Actualizado == 2).ToListAsync();

            return cod;
        }

        public static async Task<List<LOD_UserAnotacion>> GetReceptoresNoActualizados()
        {
            await Init();

            var cod = await db.Table<LOD_UserAnotacion>().Where(x => x.Actualizado == 4).ToListAsync();

            return cod;
        }
    }
}
