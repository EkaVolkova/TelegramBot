using Bot.Model;

namespace Bot.Services
{
    /// <summary>
    /// Интерфейс, определяющий параметры хранилища
    /// </summary>
    public interface IStorage
    {
        Session GetSession(long chatId);

    }
}
