
using LODapp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LODapp.Data
{
    public class DBMainServices
    {
        public static SQLiteAsyncConnection db;

        public static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "DB_LOD_APR.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<ApplicationUser>();
            await db.CreateTableAsync<CON_Contratos>();
            await db.CreateTableAsync<LOD_UsuariosLod>();
            await db.CreateTableAsync<LOD_Anotaciones>();
            await db.CreateTableAsync<LOD_LibroObras>();
            await db.CreateTableAsync<LOD_docAnotacion>();
            await db.CreateTableAsync<LOD_UserAnotacion>();
            await db.CreateTableAsync<LOD_log>();
            await db.CreateTableAsync<LOD_PermisosRolesContrato>();
            await db.CreateTableAsync<LOD_ReferenciasAnot>();
            await db.CreateTableAsync<LOD_RolesCttosContrato>();
            //await db.CreateTableAsync<MAE_ciudad>();
            await db.CreateTableAsync<MAE_ClassDoc>();
            await db.CreateTableAsync<MAE_ClassOne>();
            await db.CreateTableAsync<MAE_ClassTwo>();
            await db.CreateTableAsync<MAE_CodSubCom>();
            //await db.CreateTableAsync<MAE_Contactos>();
            //await db.CreateTableAsync<MAE_DireccionesMOP>();
            await db.CreateTableAsync<MAE_documentos>();
            //await db.CreateTableAsync<MAE_Empresa>();
            //await db.CreateTableAsync<MAE_region>();
            await db.CreateTableAsync<MAE_RolesContrato>();
            await db.CreateTableAsync<MAE_SubtipoComunicacion>();
            await db.CreateTableAsync<MAE_TipoComunicacion>();
            //await db.CreateTableAsync<MAE_Sucursal>();
            await db.CreateTableAsync<MAE_sujetoEconomico>();
            await db.CreateTableAsync<MAE_TipoDocumento>();
            //await db.CreateTableAsync<MAE_TipoLOD>();



            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<LOD_Anotaciones>().CountAsync().Result == 0) { await db.CreateTableAsync<LOD_Anotaciones>(); }
            //if (db.Table<LOD_docAnotacion>().CountAsync().Result == 0) { await db.CreateTableAsync<LOD_docAnotacion>(); }
            //if (db.Table<LOD_LibroObras>().CountAsync().Result == 0) { await db.CreateTableAsync<LOD_LibroObras>(); }
            //if (db.Table<LOD_log>().CountAsync().Result == 0) { await db.CreateTableAsync<LOD_log>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
            //if (db.Table<CON_Contratos>().CountAsync().Result == 0) { await db.CreateTableAsync<CON_Contratos>(); }
        }

    }
}
