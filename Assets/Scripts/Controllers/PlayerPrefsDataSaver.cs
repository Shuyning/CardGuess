using System;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace CardGuess.Controllers
{
    public class PlayerPrefsDataSaver : IDataSaver
    {
        public bool SaveData<T>(T data, string key) where T : class
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(data);
                
                byte[] bytesToEncode = Encoding.UTF8.GetBytes(jsonData);
                string encodedData = Convert.ToBase64String(bytesToEncode);
                
                PlayerPrefs.SetString(key, encodedData);
                PlayerPrefs.Save();
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public T LoadData<T>(string key) where T : class
        {
            try
            {
                if (PlayerPrefs.HasKey(key))
                {
                    string encodedData = PlayerPrefs.GetString(key);
                    
                    byte[] decodedBytes = Convert.FromBase64String(encodedData);
                    string jsonData = Encoding.UTF8.GetString(decodedBytes);
                    
                    T data = JsonConvert.DeserializeObject<T>(jsonData);
                    return data;
                }
                
                return null;
            }
            catch (Exception ex) { return null; }
        }
    }
}