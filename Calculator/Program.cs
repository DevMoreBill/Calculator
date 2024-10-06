using System;

// Интерфейс для функции сложения
interface IAddition
{
    int Add(int a, int b);
}

// Интерфейс для логгера
interface ILogger
{
    void LogError(string message);
    void LogInfo(string message);
}

// Класс, реализующий интерфейс сложения
class Calculator : IAddition
{
    private ILogger _logger;

    public Calculator(ILogger logger)
    {
        _logger = logger;
    }

    public int Add(int a, int b)
    {
        try
        {
            return a + b;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка сложения: {ex.Message}");
            throw;
        }
    }
}

// Класс-логгер
class ConsoleLogger : ILogger
{
    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Внедрение зависимостей
        ILogger logger = new ConsoleLogger();
        Calculator calculator = new Calculator(logger);

        int num1 = 0;
        int num2 = 0;

        // Ввод первого числа
        while (true)
        {
            Console.WriteLine("Введите первое число:");
            try
            {
                num1 = int.Parse(Console.ReadLine());
                logger.LogInfo("Ввод первого числа завершен.");
                break;
            }
            catch (FormatException)
            {
                logger.LogError("Некорректный ввод. Введите целое число.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Произошла ошибка: {ex.Message}");
            }
        }

        // Ввод второго числа
        while (true)
        {
            Console.WriteLine("Введите второе число:");
            try
            {
                num2 = int.Parse(Console.ReadLine());
                logger.LogInfo("Ввод второго числа завершен.");
                break;
            }
            catch (FormatException)
            {
                logger.LogError("Некорректный ввод. Введите целое число.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Произошла ошибка: {ex.Message}");
            }
        }

        int sum = calculator.Add(num1, num2);
        Console.WriteLine($"Сумма: {sum}");
    }
}
