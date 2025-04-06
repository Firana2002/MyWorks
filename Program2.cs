using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace MultiThread02
{
    class Program2
    {
    static ManualResetEvent syncEvent = new ManualResetEvent(false);

    static void Mainn()
    {
        // Первый запуск: поток 1, затем поток 2
        Thread thread1 = new Thread(PrintNumbersFirst);
        Thread thread2 = new Thread(PrintNumbersSecond);
        
        thread1.Start();
        thread2.Start();
        
        thread1.Join();
        thread2.Join();

        // Эксперимент: меняем порядок с задержкой
        syncEvent.Reset(); // Сбрасываем событие для повторного использования
        Thread thread3 = new Thread(PrintNumbersSecond);
        Thread thread4 = new Thread(PrintNumbersFirst);
        
        thread3.Start();
        Thread.Sleep(1000); // Задержка 1 секунда
        thread4.Start();
        
        thread3.Join();
        thread4.Join();
    }

    static void PrintNumbersFirst()
    {
        for (int i = 1; i <= 100; i++)
        {
            Console.WriteLine($"Первый поток: {i}");
        }
        syncEvent.Set(); // Сигнал о завершении
    }

    static void PrintNumbersSecond()
    {
        syncEvent.WaitOne(); // Ожидание сигнала
        for (int i = 1; i <= 100; i++)
        {
            Console.WriteLine($"Второй поток: {i}");
        }
    }}}