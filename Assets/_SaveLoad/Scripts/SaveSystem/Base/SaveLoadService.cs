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
    public sealed class SaveLoadService : ISaveLoadService
    {
        private static string GetPath<T>() => $"{typeof(T).Name.ToLower()}.data";
        private static string GetPath(string fileName) => $"{fileName.ToLower()}.data";
        
        protected readonly EncryptionUtils _encryptionUtils = new();
        protected readonly SaveConfig _saveConfig;

        [Inject]
        public SaveLoadService(SaveConfig saveConfig)
        {
            Debug.Log($"SaveLoadService Constructor");
            _saveConfig = saveConfig;
        }

        public async UniTask SaveAsync<T>(T data, string fileName) where T : ISavableData
        {
            var saveFile = GetPath(fileName);
            Debug.Log($"SaveLoadService SaveAsync for {saveFile} {typeof(T)}");
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
                var encryptedBytes = _encryptionUtils.Encrypt(jsonBytes);
                await using var fs = new FileStream(_saveConfig.SaveFilePath(saveFile), FileMode.Create, FileAccess.Write);
                await fs.WriteAsync(encryptedBytes, 0, encryptedBytes.Length);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error saving data: {ex.Message}");
            }
        }

        public async UniTask<ISavableData> LoadAsync(string fileName, Type dataType)
        {
            Debug.Log($"SaveLoadService LoadAsync for {dataType}");
            try
            {
                var filePath = GetPath(fileName);
                Debug.Log($"SaveLoadService LoadAsync for {filePath}");
                if (!File.Exists(_saveConfig.SaveFilePath(filePath)))
                {
                    Debug.LogError($"Save file {filePath} not found");
                    return default;
                }

                byte[] encryptedBytes;
                await using (var fs = new FileStream(_saveConfig.SaveFilePath(filePath), FileMode.Open, FileAccess.Read))
                {
                    encryptedBytes = new byte[fs.Length];
                    var readAsync = await fs.ReadAsync(encryptedBytes, 0, encryptedBytes.Length);
                }

                var decryptedBytes = _encryptionUtils.Decrypt(encryptedBytes);
                var json = System.Text.Encoding.UTF8.GetString(decryptedBytes);
                var data = (ISavableData)JsonConvert.DeserializeObject(json, dataType); // Или JsonUtility.FromJson<T>(json)
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