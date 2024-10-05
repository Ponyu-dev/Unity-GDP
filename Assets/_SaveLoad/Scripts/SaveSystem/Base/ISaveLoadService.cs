using System;
using Cysharp.Threading.Tasks;

namespace SaveSystem.Base
{
    public interface ISaveLoadService
    {
        UniTask SaveAsync<T>(T data, string fileName) where T : ISavableData;
        UniTask<ISavableData> LoadAsync(string fileName, Type dataType);
    }
}