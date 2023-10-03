using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Bot.Controllers;
using Bot.Services;
using Bot.Configuration;

/// <summary>
/// Токен - 6490804649:AAF72qTze9y6eyx4CwYmtNc6J6BVpOveOYo
/// 1. Бот должен иметь две функции: подсчёт количества символов в тексте и вычисление суммы чисел, которые вы ему отправляете (одним сообщением через пробел).
/// То есть в ответ на условное сообщение «сова летит» он должен прислать что-то вроде «в вашем сообщении 10 символов». 
/// А в ответ на сообщение «2 3 15» должен прислать «сумма чисел: 20».
/// 2. Выбор одной из двух функций должен происходить на старте в «Главном меню». 
/// При старте (через /start) бот должен присылать клиенту ответное сообщение — меню с кнопками, из которого можно выбрать, какое действие пользователь хочет выполнить 
/// (по аналогии с тем, как мы выбирали язык в VoiceTexterBot).
/// </summary>
namespace Bot
{
    class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            ILogger logger = new Logger();
            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            logger.Event("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            logger.Event("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(appSettings);

            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddSingleton<IStorage, MemoryStorage>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "6490804649:AAF72qTze9y6eyx4CwYmtNc6J6BVpOveOYo",
            };
        }



    }


}
