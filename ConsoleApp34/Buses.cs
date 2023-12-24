using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avtomobili
{
    public class Buses : Avto
    {
        private int traveled_temp3;
        private int passengers;
        private int pass_in;
        private int pass_out;
        public static Buses[] bus = new Buses[2];
        public Buses(int number, int amount, float expense, int passangers) : base(number, amount, expense)
        {
            path = rand.Next(600, 2500);
            speed = rand.Next(20, 50);
            index = number;
            gasoline_amount = amount;
            gasoline_expense = expense;
            end_point = path;
            passengers = passangers;
        }
        public override void accident()
        {
            for (int i = 0; i < bus.Length - 1; i++)
            {
                if (i != transport_choose - 1)
                {
                    accident_points = bus[transport_choose - 1].path - bus[i].path;
                    Console.WriteLine($"Количество точек возможного столкновения с автобусом №{i + 1}: {Math.Abs(accident_points)}\n");
                }
            }
            for (int i = 0; i < Trucks.truck.Length; i++)
            {
                accident_points = bus[transport_choose - 1].path - Trucks.truck[i].path;
                Console.WriteLine($"Количество точек возможного столкновения с тягачом №{i + 1}: {Math.Abs(accident_points)}\n");
            }
            for (int i = 0; i < cars.Length; i++)
            {

                accident_points = bus[transport_choose - 1].path - cars[i].path;
                Console.WriteLine($"Количество точек возможного столкновения с автомобилем №{i + 1}: {Math.Abs(accident_points)}\n");
            }
        }
        public override void path_info()
        {
            Console.WriteLine($"Оставшееся расстояние: {path}");
            Console.WriteLine($"Оставшееся топливо: {Math.Round(gasoline, 2)}");
            Console.WriteLine($"Скорость автомобиля: {speed}");
            Console.WriteLine($"Количество пассажиров: {passengers}");
        }
        public override void cout()
        {
            Console.WriteLine($"Номер автомобиля: {index}");
            Console.WriteLine($"Объём бензобака: {gasoline_amount}");
            Console.WriteLine($"Расход топлива на 100 км: {Math.Round(gasoline_expense, 2)}");
            Console.WriteLine($"Тип авто: Автобус\n");
        }
        private void stop(int pass_in, int pass_out)
        {
            passengers += pass_in;
            passengers -= pass_out;
            gasoline_expense += pass_in * 0.1f;
            gasoline_expense -= pass_out * 0.1f;
        }
        public override int move(int km)
        {
            start_zapravka();
            if (path / 100 > gasoline / gasoline_expense)
            {
                Console.WriteLine("Расход не позволит доехать до места назначения. Заправляться на пути? (д/н):");
                z_choice = char.Parse(Console.ReadLine());
                Console.Clear();
            }
            else Console.Clear();
            while (path != 0)
            {
                path -= speed;
                traveled_temp += speed;
                traveled_temp2 += speed;
                traveled_temp3 += speed;
                mileage_calc();
                cout();
                path_info();
                if (traveled_temp >= 100)
                {
                    gasoline -= gasoline_expense;
                    traveled_temp = 0;
                }
                if (traveled_temp2 >= 200)
                {
                    Console.WriteLine($"\nЖелаете разогнаться или затормозить? (р/з) или (н):");
                    choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "р":
                            accelerate();
                            break;
                        case "з":
                            brake();
                            break;
                        case "н":
                            break;
                    }
                    traveled_temp2 = 0;
                }
                if (traveled_temp3 >= 300)
                {
                    pass_in = rand.Next(5, 10);
                    pass_out = rand.Next(2, 6);
                    if (passengers == 0)
                    {
                        pass_out = 0;
                        stop(pass_in, pass_out);
                    }
                    else
                    {
                        stop(pass_in, pass_out);
                    }
                    Console.WriteLine($"Автобус доехал до остановки. Вошло {pass_in} человек, вышло {pass_out}");
                    traveled_temp3 = 0;
                }
                if (gasoline < gasoline_expense) gasoline = 0;
                {
                    switch (gasoline)
                    {
                        case 0:
                            Console.WriteLine("Топливо закончилось. Желаете заправиться? (д/н)");
                            z_choice = char.Parse(Console.ReadLine());
                            switch (z_choice)
                            {
                                case 'д':
                                    zapravka(gasoline_amount);
                                    break;
                                case 'н':
                                    break;
                            }
                            break;
                    }
                }
                if (path < speed) path = 0;
                if (path == 0 || gasoline == 0)
                {
                    Console.Clear();
                    cout();
                    Console.WriteLine($"Оставшееся расстояние: {path}");
                    ostatok();
                    Console.WriteLine($"Общий пробег: {mileage}");
                    start_end();
                    if (path == 0) Console.WriteLine("Вы доехали!\n");
                    else Console.WriteLine("Вы не доехали!\n");
                    break;
                }
                Thread.Sleep(2000);
                Console.Clear();
            }
            return 0;
        }
    }
}