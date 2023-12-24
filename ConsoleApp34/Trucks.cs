using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avtomobili
{
    public class Trucks : Avto
    {
        private int cargo;
        private int zag_cargo;
        private int traveled_temp3;
        public static Trucks[] truck = new Trucks[3];
        public Trucks(int number, int amount, float expense) : base(number, amount, expense)
        {
            path = rand.Next(500, 2000);
            speed = rand.Next(20, 40);
            index = number;
            gasoline_amount = amount;
            gasoline_expense = expense;
            end_point = path;
        }
        public override void accident()
        {
            for (int i = 0; i < truck.Length - 1; i++)
            {
                if (i != transport_choose - 1)
                {
                    accident_points = truck[transport_choose - 1].path - truck[i].path;
                    Console.WriteLine($"Количество точек возможного столкновения с тягачом №{i + 1}: {Math.Abs(accident_points)}\n");
                }
            }
            for (int i = 0; i < cars.Length; i++)
            {
                accident_points = truck[transport_choose - 1].path - cars[i].path;
                Console.WriteLine($"Количество точек возможного столкновения с автомобилем №{i + 1}: {Math.Abs(accident_points)}\n");
            }
            for (int i = 0; i < Buses.bus.Length; i++)
            {
                accident_points = truck[transport_choose - 1].path - Buses.bus[i].path;
                Console.WriteLine($"Количество точек возможного столкновения с автобусом №{i + 1}: {Math.Abs(accident_points)}\n");
            }
        }
        public override void path_info()
        {
            Console.WriteLine($"Оставшееся расстояние: {path}");
            Console.WriteLine($"Оставшееся топливо: {Math.Round(gasoline, 2)}");
            Console.WriteLine($"Скорость автомобиля: {speed}");
            Console.WriteLine($"Перевозимый груз: {cargo} кг");
        }
        public override void cout()
        {
            Console.WriteLine($"Номер автомобиля: {index}");
            Console.WriteLine($"Объём бензобака: {gasoline_amount}");
            Console.WriteLine($"Расход топлива на 100 км: {Math.Round(gasoline_expense, 2)}");
            Console.WriteLine($"Тип авто: Грузовой автомобиль\n");
        }
        private void pog(int tonn)
        {
            cargo += tonn;
            gasoline_expense += tonn * 0.0001f;
        }
        private void raz(int tonn)
        {
            cargo -= tonn;
            gasoline_expense -= tonn * 0.0001f;
        }
        public virtual int move(int km)
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
                if (traveled_temp3 >= 500)
                {
                    if (cargo == 0)
                    {
                        zag_cargo = rand.Next(10, 25) * 1000;
                        Console.WriteLine($"Тягач доехал до станции загрузки. Загружено {zag_cargo} кг");
                        pog(zag_cargo);
                        traveled_temp3 = 0;
                    }
                    else
                    {
                        Console.WriteLine("На сколько разгрузить автомобиль?");
                        raz(int.Parse(Console.ReadLine()));
                        traveled_temp3 = 0;
                    }
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