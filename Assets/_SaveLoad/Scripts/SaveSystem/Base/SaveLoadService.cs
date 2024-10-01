using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SaveSystem.Config;
using SaveSystem.Utils;
using UnityEngine;
using VContainer;

namespace SaveSystem.Base
{
    public class SaveLoadService : ISaveLoadService
    {
        public event Action SaveCompleted;
        public event Action SaveFailed;
        public event Action LoadCompleted;
        public event Action<Exception> LoadFailed;

        private readonly EncryptionUtils _encryptionUtils = new();
        private readonly SaveConfig _saveConfig;

        [Inject]
        public SaveLoadService(SaveConfig saveConfig)
        {
            _saveConfig = saveConfig;
        }

        public async Task SaveAsync<T>(T data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data); // Или JsonUtility.ToJson(data)
                var jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
                var encryptedBytes = _encryptionUtils.Encrypt(jsonBytes);

                await using (var fs = new FileStream(_saveConfig.SaveFilePath, FileMode.Create, FileAccess.Write))
                {
                    await fs.WriteAsync(encryptedBytes, 0, encryptedBytes.Length);
                }

                SaveCompleted?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error saving data: {ex.Message}");
            }
        }

        public async Task<T> LoadAsync<T>()
        {
            try
            {
                if (!File.Exists(_saveConfig.SaveFilePath))
                {
                    Debug.LogError("Save file not found");
                    return default;
                }

                byte[] encryptedBytes;
                await using (var fs = new FileStream(_saveConfig.SaveFilePath, FileMode.Open, FileAccess.Read))
                {
                    encryptedBytes = new byte[fs.Length];
                    var readAsync = await fs.ReadAsync(encryptedBytes, 0, encryptedBytes.Length);
                }

                var decryptedBytes = _encryptionUtils.Decrypt(encryptedBytes);
                var json = System.Text.Encoding.UTF8.GetString(decryptedBytes);
                var data = JsonConvert.DeserializeObject<T>(json); // Или JsonUtility.FromJson<T>(json)

                LoadCompleted?.Invoke();
                return data;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading data: {ex.Message}");
                return default;
            }
        }
    }
}