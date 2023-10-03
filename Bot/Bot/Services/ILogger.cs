namespace Bot.Services
{
    /// <summary>
    /// Интерфейс, определяющий логгирование
    /// </summary>
    interface ILogger 
    {
        void Error(string message);
        void Event(string message);
    }
}
