using System;


namespace Bot.Services
{

    /// <summary>
    /// Класс для логирования событий и ошибок, реализующий интерфейс ILogger
    /// </summary>
    class Logger : ILogger
    {
        /// <summary>
        /// Функция для логгирования в консоль ошибок
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            Console.WriteLine("Произошла ошибка: {0}", message);

        }

        /// <summary>
        /// Функция для логгирования в консоль событий
        /// </summary>
        /// <param name="message"></param>
        public void Event(string message)
        {
            Console.WriteLine("Произошло событие: {0}", message);
        }
    }
}
