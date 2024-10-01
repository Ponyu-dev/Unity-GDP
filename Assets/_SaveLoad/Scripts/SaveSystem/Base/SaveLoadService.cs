using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using SaveSystem.Config;
using SaveSystem.Utils;
using UnityEngine;
using VContainer;

namespace SaveSystem.Base
{
    public abstract class SaveLoadService //: ISaveLoadService
    {
        protected string _saveFileName;
        protected readonly EncryptionUtils _encryptionUtils = new();
        protected readonly SaveConfig _saveConfig;

        [Inject]
        protected SaveLoadService(SaveConfig saveConfig)
        {
            _saveConfig = saveConfig;
        }

        protected async UniTask SaveAsync<T>(T data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data); // Или JsonUtility.ToJson(data)
                var jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
                var encryptedBytes = _encryptionUtils.Encrypt(jsonBytes);

                await using var fs = new FileStream(_saveConfig.SaveFilePath(_saveFileName), FileMode.Create, FileAccess.Write);
                await fs.WriteAsync(encryptedBytes, 0, encryptedBytes.Length);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error saving data: {ex.Message}");
            }
        }

        protected async UniTask<T> LoadAsync<T>()
        {
            try
            {
                if (!File.Exists(_saveConfig.SaveFilePath(_saveFileName)))
                {
                    Debug.LogError($"Save file {_saveFileName} not found");
                    return default;
                }

                byte[] encryptedBytes;
                await using (var fs = new FileStream(_saveConfig.SaveFilePath(_saveFileName), FileMode.Open, FileAccess.Read))
                {
                    encryptedBytes = new byte[fs.Length];
                    var readAsync = await fs.ReadAsync(encryptedBytes, 0, encryptedBytes.Length);
                }

                var decryptedBytes = _encryptionUtils.Decrypt(encryptedBytes);
                var json = System.Text.Encoding.UTF8.GetString(decryptedBytes);
                var data = JsonConvert.DeserializeObject<T>(json); // Или JsonUtility.FromJson<T>(json)
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