namespace CardGuess.Controllers
{
    public interface IDataSaver
    {
        public bool SaveData<T>(T data, string key) where T : class;
        public T LoadData<T>(string key) where T : class;
    }   
}