using System;
using AnimalPassport.SmartReader.Models;

namespace AnimalPassport.SmartReader
{
    class Program
    {
        private static readonly Auth Auth = new Auth();
        private static readonly ApiClient Client = new ApiClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Для початку роботи необхiдно вiйти в систему.\n");

            Login();
            Process();

            Console.Read();
        }

        private static void Login()
        {
            while (true)
            {
                Console.WriteLine("Введiть електронну пошту.");
                Auth.Email = Console.ReadLine();

                Console.WriteLine("Введiть пароль.");
                Auth.Password = Console.ReadLine();

                var result = Client.LoginAsync(Auth).ConfigureAwait(false).GetAwaiter().GetResult();

                if (result)
                {
                    Console.WriteLine("Ви успiшно ввiйшли в систему.\n");

                    break;
                }

                Console.WriteLine("Невiрно введений логiн або пароль.\n");
            }
        }

        private static void Process()
        {
            while (true)
            {
                Console.WriteLine("Введiть персональний iндентифiкатор домашньої тварини для отримання iнформацiї.");

                var animalId = Console.ReadLine();

                var result = Client.SendIdAsync(animalId).ConfigureAwait(false).GetAwaiter().GetResult();

                Console.WriteLine(result
                    ? "Операцiя виконана успiшно.\n"
                    : "Пiд час виконання операцiї виникла помилка.\n");
            }
        }
    }
}
