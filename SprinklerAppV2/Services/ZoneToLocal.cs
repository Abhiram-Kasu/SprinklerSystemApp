using Newtonsoft.Json;
using SprinklerAppV2.Helpers;
using SprinklerAppV2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SprinklerAppV2.Services
{
    internal class ZoneToLocal : IZoneToLocal
    {
        private static readonly string _mainDir = FileSystem.Current.AppDataDirectory;
        private const string _prefFileName = "ZonePrefs.json";
        private string FullFileDir => Path.Combine(_mainDir, _prefFileName);
        
        public ObservableCollection<Zone> Zones { get; set; }

       

        public async Task<bool> DoesZonePrefsExist()
        {
            var exists = File.Exists(Path.Combine(_mainDir, _prefFileName));
            if (exists)
            {
                using var stream = new FileStream(FullFileDir, FileMode.Open, FileAccess.Read, FileShare.Read);
                if (stream.Length > 0)
                {
                    using var memStream = new MemoryStream();
                    stream.CopyTo(memStream);
                    try
                    {
                        Zones = JsonConvert.DeserializeObject<ObservableCollection<Zone>>(Encoding.ASCII.GetString(memStream.ToArray()));
                        return true;
                    }
                    catch (Exception)
                    {
                        Zones = new();
                        await stream.DisposeAsync();
                        ClearPrefs();
                        
                        return false;
                    }
                    
                }
                
                 
                else
                {
                    await stream.DisposeAsync();
                    Zones = new();
                    return false;

                }
                    
            }
            else
            {
                File.Create(FullFileDir);
                Zones = new();
            }

            return exists;
        }
       
       
        public async Task Update(ObservableCollection<Zone> collecton)
        {
            ClearPrefs();
            Zones = collecton;
            using var fileStream = new FileStream(FullFileDir, FileMode.Open, FileAccess.Write, FileShare.Read);
            await fileStream.FlushAsync();
            
            var bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(collecton));
            await fileStream.WriteAsync(bytes);
        }

        public void ClearPrefs()
        {
            var fileStream = new FileStream(FullFileDir, FileMode.Open, FileAccess.Write, FileShare.Read);
            fileStream.SetLength(0);
            fileStream.Close();
        }
    }
}
