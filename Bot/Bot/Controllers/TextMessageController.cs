using Bot.Extentions;
using Bot.Model;
using Bot.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Controllers
{
    /// <summary>
    /// Класс для обработки текстовых сообщений
    /// </summary>
    public class TextMessageController
    {
        //определяем переменные для клиента, хранилища сессий 
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        //определяем и инициализируем переменную для логера
        private ILogger logger = new Logger();


        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            //инициализируем переменные для клиента, хранилища сессий
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        /// <summary>
        /// Задача обработки текстовых сообщений
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Handle(Message message, CancellationToken ct)
        {
            //логируем начало задчи
            logger.Event($"Контроллер {GetType().Name} получил сообщение");

            switch (message.Text)
            {
                //Пришла команда начала работы
                case "/start":
                    {
                        // Объект, представляющий кноки
                        var buttons = new List<InlineKeyboardButton[]>();
                        buttons.Add(new[]
                        {
                            InlineKeyboardButton.WithCallbackData($" Кол-во символов"),
                            InlineKeyboardButton.WithCallbackData($" Сумма чисел")
                        });

                        //логируем запуск выбора команд
                        logger.Event($"Запущен выбор команд");

                        // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $" Бот умеет выполнять две функции: {Environment.NewLine}" +
                            $"{Environment.NewLine}<b> 1. Подсчитывать количсетво символов в строке.</b>{Environment.NewLine}" +
                            $"<b> 2. Подсчитывать сумму чисел в строке.</b>{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                        break;
                    }
                //Пришло обычное сообщение
                default:
                    {
                        string text = string.Empty;

                        //выбираем режим
                        switch (_memoryStorage.GetSession(message.Chat.Id).ModesCode)
                        {
                            //режим подчета количества символов
                            case Modes.CounterLetter:
                                //логируем
                                logger.Event("Начат подсчет количества символов");

                                //считаем количество символов и засовываем числов в строку
                                text = "Количество символов: " + message.Text.Length.ToString() + ".";
                                //отправляем полученную строку в чат
                                await _telegramClient.SendTextMessageAsync(message.Chat.Id, text, cancellationToken: ct);
                                //логируем окончание подсчета
                                logger.Event("Закончен подсчет количества символов");
                                break;
                            case Modes.SummNumber:
                                //может так случиться, что числа не являются таковыми
                                try
                                {
                                    logger.Event("Начат подсчет суммы чисел");
                                    text = "Сумма чисел: " + MathExtension.SumArray(StringExtension.FromStringToArrayNum(message.Text)).ToString() + ".";
                                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, text, cancellationToken: ct);
                                    logger.Event("Закончен подсчет суммы чисел");
                                }
                                catch (Exception ex)
                                {
                                    text = $"Ошибка: {ex.Message}.";
                                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, text, cancellationToken: ct);
                                    logger.Error("Не число");

                                }

                                break;
                            default:
                                break;

                        }

                        break;
                    }
            }
        }


    }


}
