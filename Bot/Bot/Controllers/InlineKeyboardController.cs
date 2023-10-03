using Bot.Model;
using Bot.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Controllers
{
    /// <summary>
    /// Класс для обработки кнопок
    /// </summary>
    class InlineKeyboardController 
    {
        //определяем переменных для клиента, хранилища сессий 
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        //определяем и инициализируем переменную для логера
        ILogger logger = new Logger();

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            //инициализируем переменные для клиента, хранилища сессий
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        /// <summary>
        /// Обработчик кнопок
        /// </summary>
        /// <param name="callbackQuery"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            //логируем, что контроллер получил сообщение
            logger.Event($"Контроллер {GetType().Name} получил сообщение");

            //Проверяем, есть ли данные
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            switch(callbackQuery.Data)
            {
                case " Количество символов":
                    _memoryStorage.GetSession(callbackQuery.From.Id).ModesCode = Modes.CounterLetter;
                    break;
                case " Сумма чисел":
                    _memoryStorage.GetSession(callbackQuery.From.Id).ModesCode = Modes.SummNumber;
                    break;
                default:
                    _memoryStorage.GetSession(callbackQuery.From.Id).ModesCode = Modes.CounterLetter;
                    break;
            };



            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбран режим - {callbackQuery.Data.ToLower()}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
            
            //логируем, что выбрали режим работы
            logger.Event("Выбран режим работы");
        }


    }
}
