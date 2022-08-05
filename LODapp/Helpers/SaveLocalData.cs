using LODapp.Data;
using LODapp.Models;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LODapp.Helpers
{
    public static class SaveLocalData
    {

        public async static Task<bool> CreateDirectoryApp()
        {
            try
            {
                String folderName = "Files";
                IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
                if (!await IsFolderExistAsync(folderName, folder))
                {
                    folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
                    folderName = "Contratos";
                    if (!await IsFolderExistAsync(folderName, folder))
                    {
                        folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
                        return true;
                    }
                    return true;
                }
                return true;
            }catch(Exception ex)
            {
                return false;
            }            
            
        }

        public async static Task<bool> CreateDirectoryAnotacion(int IdAnotacion)
        {
            try
            {
                LOD_Anotaciones anotacion = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
                if(anotacion != null)
                {
                    String folderName = $"{anotacion.IdAnotacion}_{anotacion.Correlativo}";
                    IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
                    folder = await folder.GetFolderAsync("Files");
                    folder = await folder.GetFolderAsync("Contratos");
                    if (!await IsFolderExistAsync(folderName, folder))
                    {
                        folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
                        return true;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async static Task<string> CreateLocalFileAnotacion(int IdAnotacion, int IdDocAnotacion,int IdTipoDoc,String NombreDoc, FileResult fileSave)
        {
            string ruta = "";
            try
            {
                LOD_Anotaciones anotacion = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
                if (anotacion != null)
                {

                    String folderName = $"{anotacion.IdAnotacion}_{anotacion.Correlativo}";
                    string filename = $"{anotacion.IdAnotacion}_{anotacion.Correlativo}_{IdTipoDoc}_{NombreDoc}";
                    IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
                    folder = await folder.GetFolderAsync("Files");
                    folder = await folder.GetFolderAsync("Contratos");
                    folder = await folder.GetFolderAsync(folderName);
                    if (!await IsFileExistAsync(filename, folder))
                    {
                        if (fileSave == null)
                        {
                            ruta = null;
                            return ruta;
                        }
                        // save the file into local storage
                        var newFile = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory,$"~/Files/Contratos/{folderName}",filename);
                        using (var stream = await fileSave.OpenReadAsync())
                        using (var newStream = File.OpenWrite(newFile))
                            await stream.CopyToAsync(newStream);

                        ruta = newFile;

                    }
                    return ruta;
                }
                else
                {
                    return ruta;
                }
            }
            catch (Exception ex)
            {
                return ruta;
            }

            return ruta;
        }

        public async static Task<bool> IsFileExistAsync(this string fileName, IFolder rootFolder = null)
        {
            // get hold of the file system  
            IFolder folder = rootFolder ?? PCLStorage.FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(fileName);
            // already run at least once, don't overwrite what's there  
            if (folderexist == ExistenceCheckResult.FileExists)
            {
                return true;

            }
            return false;
        }

        public async static Task<bool> IsFolderExistAsync(this string folderName, IFolder rootFolder = null)
        {
            // get hold of the file system  
            IFolder folder = rootFolder ?? PCLStorage.FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(folderName);
            // already run at least once, don't overwrite what's there  
            if (folderexist == ExistenceCheckResult.FolderExists)
            {
                return true;

            }
            return false;
        }

        public async static Task<FileResult> GetFileFromLocal(string Ruta)
        {
            // get hold of the file system
            if (File.Exists(Ruta))
            {
                var files = File.OpenRead(Ruta);
                FileResult fileResult = new FileResult(Ruta);
                return fileResult;
            }
            else
            {
                return null;
            }
            
        }
    }
}
