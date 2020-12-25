using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        /* КЛАСС НЕЙРОНА */
        public class Neuron
        {
            private decimal weight = 0.5m; //вес связи
            public decimal LastError { get; private set; }
            public decimal Smoothing { get; set; } = 0.00001m; //сглаживание

            /* Перевод долларов в евро */
            public decimal ProcessInputData(decimal input)
            {
                return input * weight;
            }

            /* Перевод евро в доллары */
            public decimal RestoreInputData(decimal output) 
            {
                return output / weight;
            }

            /* Корректировка веса связи */
            public void Train(decimal input, decimal expectedResult)
            {
                var actualResult = input * weight;
                LastError = expectedResult - actualResult;
                var correction = (LastError / actualResult) * Smoothing;
                weight += correction; 
            }
        }

        static void Main(string[] args)
        {
            /* АКУТАЛЬНЫЙ КУРС ВАЛЮТ */
            decimal dollar = 100;
            decimal euro = 82.1m;

            /* Создание новго нейрона */
            Neuron neuron = new Neuron();

            /* ОБУЧЕНИЕ ДО ТОГО МОМЕНТА ПОКА НЕ ДОСТИГНЕМ НУЖНОЙ ТОЧНОСТИ */
            int i = 0;
            do
            {
                i++;
                neuron.Train(dollar, euro);
            } while (neuron.LastError > neuron.Smoothing || neuron.LastError < -neuron.Smoothing);

            /* Спрашиваем у пользователя сколько долларов ему нужно перевести в евро */
            Console.Write("Введите количество долларов: ");
            decimal put = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine();

            /* ВЫВОД */
            Console.WriteLine($"{put} долларов = {Math.Round(neuron.ProcessInputData(put), 3)} евро");
            Console.WriteLine();
            Console.WriteLine($"Конечная ошибка (отличие истинного значения от того что вычислила нейросеть): {Math.Round((euro * put)/dollar - neuron.ProcessInputData(put), 7)}");

            Console.ReadKey();
        }
    }
}
