using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Data
{
    public class AnotacionesDBServices : DBMainServices
    {
        public static async Task<int> AddAnotacion(LOD_Anotaciones Anotacion)
        {
            await Init();
            var id = await db.InsertAsync(Anotacion);

            return id;
        }

        public static async Task RemoveAnotacion(int id)
        {
            await Init();
            await db.DeleteAsync<LOD_Anotaciones>(id);

        }

        public static async Task<List<LOD_Anotaciones>> GetAnotacion()
        {
            await Init();

            var anotaciones = await db.Table<LOD_Anotaciones>().ToListAsync();

            return anotaciones;

        }

        public static async Task<List<LOD_Anotaciones>> GetAnotacionesNoCreadas()
        {
            await Init();

            var anotaciones = await db.Table<LOD_Anotaciones>().Where(x => x.Actualizado == 2).ToListAsync();

            return anotaciones;

        }

        public static async Task<List<LOD_Anotaciones>> GetAnotacionesNoFirmadas()
        {
            await Init();

            var anotaciones = await db.Table<LOD_Anotaciones>().Where(x => x.Actualizado == 2 && !x.EstadoFirma).ToListAsync();

            return anotaciones;
        }

        public static async Task<List<LOD_Anotaciones>> GetAnotacionesNoActualizadas()
        {
            await Init();

            var anotaciones = await db.Table<LOD_Anotaciones>().Where(x => x.Actualizado == 4).ToListAsync();

            return anotaciones;

        }

        public static async Task<LOD_Anotaciones> FindAnotacion(int id)
        {
            await Init();

            var anotacion = await db.Table<LOD_Anotaciones>().Where(x => x.IdAnotacion.Equals(id)).FirstOrDefaultAsync();

            return anotacion;

        }

        public static async Task<LOD_Anotaciones> FindAnotacionPublicada(int id)
        {
            await Init();

            var anotacion = await db.Table<LOD_Anotaciones>().Where(x => x.IdAnotacion.Equals(id) && x.Estado == 2).FirstOrDefaultAsync();

            return anotacion;

        }

        public static async Task<List<LOD_Anotaciones>> FindAnotacionByLod(int id)
        {
            await Init();

            var anotacion = await db.Table<LOD_Anotaciones>().Where(x => x.IdLod.Equals(id)).ToListAsync();

            return anotacion;

        }

        public static async Task<List<LOD_Anotaciones>> FindAnotacionPrincipal(int id)
        {
            await Init();

            var anotacion = await db.Table<LOD_Anotaciones>().Where(x => x.IdLod.Equals(id) && x.Estado == 2).ToListAsync();

            return anotacion;

        }

        public static async Task<List<int>> FindIdAnotacionByLod(int id)
        {
            await Init();

            var anotacion = await db.Table<LOD_Anotaciones>().Where(x => x.IdLod.Equals(id)).ToListAsync();
            List<int> listadoInt = anotacion.Select(x => x.IdAnotacion).ToList();
            return listadoInt;

        }

        public static async Task<List<LOD_Anotaciones>> FindAnotacionByUserBorrador(string userid, int IdLod)
        {
            await Init();

            var anotacion = await db.Table<LOD_Anotaciones>().Where(x => x.UserIdBorrador.Equals(userid) && x.Estado == 0 && x.IdLod == IdLod).OrderByDescending(x => x.FechaIngreso).ToListAsync();

            return anotacion;

        }

        public static async Task<List<LOD_Anotaciones>> FindSolicitudesResp(int IdLod)
        {
            await Init();

            var anotacion = await db.Table<LOD_Anotaciones>().Where(x => x.IdLod == IdLod && x.SolicitudRest && x.EstadoFirma).OrderByDescending(x => x.FechaIngreso).ToListAsync();

            return anotacion;

        }

        public static async Task<List<LOD_Anotaciones>> FindAnotacionByUserPub(string userid, int IdLod)
        {
            await Init();

            var anotacion = await db.Table<LOD_Anotaciones>().Where(x => x.UserId.Equals(userid) && x.Estado == 2 && x.IdLod == IdLod).OrderByDescending(x => x.FechaIngreso).ToListAsync();

            return anotacion;

        }

        public static async Task<List<LOD_Anotaciones>> FindFirmasPendientes(string userid, int IdLod)
        {
            await Init();

            var anotacion = await db.Table<LOD_Anotaciones>().Where(x => x.UserId.Equals(userid) && x.Estado == 1 && x.IdLod == IdLod).OrderByDescending(x => x.FechaIngreso).ToListAsync();

            return anotacion;

        }

        public static async Task<List<LOD_Anotaciones>> FindAnotacionByLodPub(int IdLod)
        {
            await Init();

            var anotacion = await db.Table<LOD_Anotaciones>().Where(x => x.IdLod.Equals(IdLod) && x.Estado == 2).OrderByDescending(x => x.FechaIngreso).ToListAsync();

            return anotacion;

        }


        public static async Task<List<LOD_Anotaciones>> SearchAnotacion(string query)
        {
            await Init();

            var cod = await db.Table<LOD_Anotaciones>().Where(x => x.Titulo.Equals(query) || x.Cuerpo.Equals(query) || x.Correlativo.Equals(query) || x.Titulo.Contains(query) || x.Cuerpo.Contains(query) || x.Correlativo.ToString().Contains(query)).ToListAsync();

            return cod;

        }

        public static async Task<int> LastAnotacion()
        {
            await Init();

            var cod = await db.Table<LOD_Anotaciones>().ToListAsync();
            int id = cod.OrderByDescending(x => x.IdAnotacion).Select(x => x.IdAnotacion).FirstOrDefault();

            return id;

        }

        public static async Task<int> UpdateAnotacion(LOD_Anotaciones anotacion)
        {
            await Init();
            int rows = await db.UpdateAsync(anotacion);
            return rows;
        }

    }
}
