namespace SaveSystem.Base
{
    public interface IDataProvider<T> where T : ISavableData
    {
        T GetDataForSaving(); // Возвращает один объект для сохранения
        void ApplyLoadedData(T data); // Применяет загруженные данные
    }
}