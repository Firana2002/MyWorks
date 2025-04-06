using System;
using System.Threading;

class Program3
{
    private static double sharedValue = 0.5; // Начальное значение
    private static readonly AutoResetEvent cosineEvent = new AutoResetEvent(true); // Первым начинает поток косинуса
    private static readonly AutoResetEvent arccosEvent = new AutoResetEvent(false);

    static void Main1()
    {
        Thread cosineThread = new Thread(CalculateCosine);
        Thread arccosThread = new Thread(CalculateArccosine);

        cosineThread.Start();
        arccosThread.Start();

        // Для завершения программы по нажатию Enter
        Console.ReadLine();
    }

    static void CalculateCosine()
    {
        while (true)
        {
            cosineEvent.WaitOne(); // Ожидаем сигнал
            
            double result = Math.Cos(sharedValue);
            Console.WriteLine($"Cosine calculated: {result}");
            sharedValue = result;
            
            arccosEvent.Set(); // Сигнализируем другому потоку
        }
    }

    static void CalculateArccosine()
    {
        while (true)
        {
            arccosEvent.WaitOne(); // Ожидаем сигнал
            
            double result = Math.Acos(sharedValue);
            Console.WriteLine($"Arccosine calculated: {result}");
            sharedValue = result;
            
            cosineEvent.Set(); // Сигнализируем другому потоку
        }
    }
}