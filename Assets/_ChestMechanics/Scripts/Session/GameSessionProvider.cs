using SaveSystem.Base;
using VContainer;

namespace _ChestMechanics.Session
{
    //TODO Надо вернуть логику сохранения и загрузки данных
    public class GameSessionProvider : IDataProvider<ISavableData>
    {
        [Inject]
        public GameSessionProvider() { }

        public ISavableData GetDataForSaving()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyLoadedData(ISavableData data)
        {
            throw new System.NotImplementedException();
        }
    }
}