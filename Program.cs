using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public class Neuron
        {
            private decimal weight = 0.5m;
            public decimal LastError { get; private set; }
            public decimal Smoothing { get; set; } = 0.00001m; //сглаживание

            public decimal ProcessInputData(decimal input) //Перевод долааров в евро
            {
                return input * weight;
            }

            public decimal RestoreInputData(decimal output) //Перевод из евро доллары
            {
                return output / weight;
            }

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
            decimal dollar = 100;
            decimal euro = 82.1m;

            Neuron neuron = new Neuron();

            int i = 0;
            do
            {
                i++;
                neuron.Train(dollar, euro);
            } while (neuron.LastError > neuron.Smoothing || neuron.LastError < -neuron.Smoothing);

            Console.Write("Введите количество долларов: ");
            decimal put = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine($"{put} долларов = {Math.Round(neuron.ProcessInputData(put), 3)} евро");

            Console.ReadKey();
        }
    }
}
