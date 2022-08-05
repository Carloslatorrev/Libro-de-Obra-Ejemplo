using LODapp.Data;
using LODapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Helpers
{
    public class FiltradoAnotaciones
    {
        public FiltradoAnotaciones()
        {

        }

        public async Task<List<LOD_Anotaciones>> GetBandejaPrincipal(int idLibro)
        {
            List<LOD_Anotaciones> anotaciones = new List<LOD_Anotaciones>();
            anotaciones = await AnotacionesDBServices.FindAnotacionByLodPub(idLibro);
            return anotaciones;
        }

        public async Task<List<LOD_Anotaciones>> GetMisPublicaciones(string userid, int IdLod)
        {
            List<LOD_Anotaciones> anotaciones = new List<LOD_Anotaciones>();
            anotaciones = await AnotacionesDBServices.FindAnotacionByUserPub(userid, IdLod);
            return anotaciones;
        }

        public async Task<List<LOD_Anotaciones>> GetMisBorradores(string userid, int IdLod)
        {
            List<LOD_Anotaciones> anotaciones = new List<LOD_Anotaciones>();
            anotaciones = await AnotacionesDBServices.FindAnotacionByUserBorrador(userid, IdLod);
            return anotaciones;
        }

        public async Task<List<LOD_Anotaciones>> GetMisDestacadas(int IdLod, int IdUsLod)
        {
            List<LOD_Anotaciones> anotaciones = new List<LOD_Anotaciones>();
            anotaciones = await UserAnotacionDBServices.FindAnotacionDestacada(IdLod, IdUsLod);
            return anotaciones;
        }

        public async Task<List<LOD_Anotaciones>> GetNombradoEn(int IdLod, string UserId)
        {
            List<LOD_Anotaciones> anotaciones = new List<LOD_Anotaciones>();
            anotaciones = await UserAnotacionDBServices.FindNombradoEn(IdLod, UserId);
            return anotaciones;
        }

        public async Task<List<LOD_Anotaciones>> GetFirmasPendientes(string userid, int IdLod)
        {
            List<LOD_Anotaciones> anotaciones = new List<LOD_Anotaciones>();
            anotaciones = await AnotacionesDBServices.FindFirmasPendientes(userid, IdLod);
            return anotaciones;
        }

        public async Task<List<LOD_Anotaciones>> GetMisRespPendientes(string UserId, int IdLod)
        {
            List<LOD_Anotaciones> anotaciones = new List<LOD_Anotaciones>();
            LOD_UsuariosLod UserLod = await UsuariosLodDBServices.FindIdByUser(IdLod, UserId);
            List<LOD_Anotaciones> listadoAnotSol = await AnotacionesDBServices.FindSolicitudesResp(IdLod);
            List<int> listadoIntAnotSol = (List<int>)listadoAnotSol.Select(x => x.IdAnotacion);
            anotaciones = await UserAnotacionDBServices.FindIdAnotacionesReferencias(UserLod, listadoIntAnotSol);
            return anotaciones;
        }

        public async Task<List<LOD_Anotaciones>> GetFiltrado(AnotacionesFiltradasView filtro, List<int> IdsAnotaciones)
        {
            List<int> idAnotFiltradas = new List<int>();
            List<LOD_UserAnotacion> listadoActual = new List<LOD_UserAnotacion>();
            foreach (var item in IdsAnotaciones)
            {
                var userAnot = await UserAnotacionDBServices.FindUserAnotacion(item);
                listadoActual.Add(userAnot);
            }

            List<LOD_Anotaciones> listadoActualAnot = new List<LOD_Anotaciones>();



            if (!String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value == 0 || filtro.Destinatario == null) && String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion == null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.AnotacionModel.UserId == filtro.Remitente).Select(x => x.IdAnotacion).ToList());
            }
            else if(!String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value != 0 || filtro.Destinatario != null) && String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion == null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.IdUsLod == filtro.Destinatario && x.EsPrincipal && x.AnotacionModel.UserId == filtro.Remitente).Select(x => x.IdAnotacion).ToList());
            }
            else if (!String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value != 0 || filtro.Destinatario != null) && !String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion == null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.IdUsLod == filtro.Destinatario && x.EsPrincipal && x.AnotacionModel.UserId == filtro.Remitente && x.AnotacionModel.Cuerpo.Contains(filtro.ContenidoCuerpo)).Select(x => x.IdAnotacion).ToList());
            }
            else if (!String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value != 0 || filtro.Destinatario != null) && String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion != null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.IdUsLod == filtro.Destinatario && x.EsPrincipal && x.AnotacionModel.UserId == filtro.Remitente && x.AnotacionModel.Cuerpo.Contains(filtro.ContenidoCuerpo) && x.AnotacionModel.FechaPub.Equals(filtro.FechaPublicacion)).Select(x => x.IdAnotacion).ToList());
            }
            else if (String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value != 0 || filtro.Destinatario != null) && !String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion == null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.IdUsLod == filtro.Destinatario && x.EsPrincipal).Select(x => x.IdAnotacion).ToList());
            }
            else if (String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value != 0 || filtro.Destinatario != null) && !String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion == null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.IdUsLod == filtro.Destinatario && x.EsPrincipal && x.AnotacionModel.Cuerpo.Contains(filtro.ContenidoCuerpo)).Select(x => x.IdAnotacion).ToList());
            }
            else if (String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value != 0 || filtro.Destinatario != null) && !String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion != null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.IdUsLod == filtro.Destinatario && x.EsPrincipal && x.AnotacionModel.Cuerpo.Contains(filtro.ContenidoCuerpo) && x.AnotacionModel.FechaPub == filtro.FechaPublicacion).Select(x => x.IdAnotacion).ToList());
            }
            else if (String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value != 0 || filtro.Destinatario != null) && String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion != null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.IdUsLod == filtro.Destinatario && x.EsPrincipal && x.AnotacionModel.FechaPub == filtro.FechaPublicacion).Select(x => x.IdAnotacion).ToList());
            }
            else if (String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value == 0 || filtro.Destinatario == null) && !String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion == null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.AnotacionModel.Cuerpo.Contains(filtro.ContenidoCuerpo)).Select(x => x.IdAnotacion).ToList());
            }
            else if (String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value == 0 || filtro.Destinatario == null) && !String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion != null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.AnotacionModel.Cuerpo.Contains(filtro.ContenidoCuerpo) && x.AnotacionModel.FechaPub == filtro.FechaPublicacion).Select(x => x.IdAnotacion).ToList());
            }
            else if (String.IsNullOrEmpty(filtro.Remitente) && (filtro.Destinatario.Value == 0 || filtro.Destinatario == null) && !String.IsNullOrEmpty(filtro.ContenidoCuerpo) && filtro.FechaPublicacion != null)
            {
                idAnotFiltradas.AddRange(listadoActual.Where(x => x.AnotacionModel.FechaPub == filtro.FechaPublicacion).Select(x => x.IdAnotacion).ToList());
            }

            foreach (var item in idAnotFiltradas)
            {
                listadoActualAnot.Add(await AnotacionesDBServices.FindAnotacion(item));
            }

            return listadoActualAnot;
        }

    }

    public class AnotacionesFiltradasView{
        public string Remitente { get; set; }
        public int? Destinatario { get; set; }
        public string ContenidoCuerpo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
    }

}
