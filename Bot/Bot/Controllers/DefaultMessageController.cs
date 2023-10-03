using Bot.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Controllers
{
    /// <summary>
    /// Класс для нераспознанных типов сообщений:
    /// аудио, видео, фото
    /// </summary>
    class DefaultMessageController 
    {
        //определяем переменной для клиента 
        private readonly ITelegramBotClient _telegramClient;

        //определяем и инициализируем переменную для логера
        ILogger logger = new Logger();
        public DefaultMessageController(ITelegramBotClient telegramBotClient)
        {
            //инициализируем переменную для клиента
            _telegramClient = telegramBotClient;
        }
        /// <summary>
        /// Обработка неподдерживаемого сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Handle(Message message, CancellationToken ct)
        {
            logger.Event($"Контроллер {GetType().Name} получил сообщение");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Получено сообщение не поддерживаемого формата", cancellationToken: ct);
        }

    }
}
