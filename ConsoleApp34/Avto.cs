using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avtomobili
{
    public class Avto
    {
        public static int transport_choose;
        protected int accident_points;
        protected int index; //Номер машины
        protected int gasoline_amount; //Объём бака
        protected float gasoline_expense; //Расход
        protected float gasoline; //Количество бензина
        protected int speed; //Скорость
        protected string choice; //Выбор разгона или торможения
        protected char z_choice; //Выбор (дозаправки)
        protected int traveled_temp; //Пройденное расстояние для вычисления расхода
        protected int traveled_temp2; //Пройденное расстояние для появления выбора дозаправки
        protected int end_point; //Пункт назначения
        protected int mileage; //Пробег
        public int path; //Предстоящий путь (генерируется случайно)
        protected int random; //Выбор направления
        protected char axis; //Направление
        public static Avto[] cars = new Avto[5];
        protected Random rand = new Random();
        public Avto(int number, int amount, float expense)
        {
            path = rand.Next(400, 1900);
            speed = rand.Next(40, 90);
            index = number;
            gasoline_amount = amount;
            gasoline_expense = expense;
            end_point = path;
        }
        public virtual void accident()
        {
            for (int i = 0; i < cars.Length - 1; i++)
            {
                if (i != transport_choose - 1)
                {
                    accident_points = cars[transport_choose - 1].path - cars[i].path;
                    Console.WriteLine($"Количество точек возможного столкновения с автомобилем №{i + 1}: {Math.Abs(accident_points)}\n");
                }
            }
            for (int i = 0; i < Trucks.truck.Length; i++)
            {
                accident_points = cars[transport_choose - 1].path - Trucks.truck[i].path;
                Console.WriteLine($"Количество точек возможного столкновения с тягачом №{i + 1}: {Math.Abs(accident_points)}\n");
            }
            for (int i = 0; i < Buses.bus.Length; i++)
            {
                accident_points = cars[transport_choose - 1].path - Buses.bus[i].path;
                Console.WriteLine($"Количество точек возможного столкновения с автобусом №{i + 1}: {Math.Abs(accident_points)}\n");
            }
        }
        public virtual void path_info()
        {
            Console.WriteLine($"Оставшееся расстояние: {path}");
            Console.WriteLine($"Оставшееся топливо: {Math.Round(gasoline, 2)}");
            Console.WriteLine($"Скорость автомобиля: {speed}");
        }
        public virtual void cout()
        {
            Console.WriteLine($"Номер автомобиля: {index}");
            Console.WriteLine($"Объём бензобака: {gasoline_amount}");
            Console.WriteLine($"Расход топлива на 100 км: {Math.Round(gasoline_expense, 2)}");
            Console.WriteLine($"Тип авто: Легковой автомобиль\n");
        }
        protected void zapravka(float top)
        {
            gasoline = top;
        }
        protected void start_zapravka()
        {
        again:
            Console.WriteLine("\nВведите объём для заправки бака:");
            float fill = float.Parse(Console.ReadLine());
            if (fill < 0)
            {
                Console.WriteLine("Отрицательные значения недопустимы");
                goto again;
            }
            if (fill <= gasoline_amount) gasoline += fill;
            else
            {
                Console.WriteLine("Ваш бензобак слишком мал для такого количества топлива");
                goto again;
            }
        }
        protected void ostatok()
        {
            Console.WriteLine($"\nОстаток топлива: {Math.Round(gasoline, 2)}\n");
        }
        protected void accelerate()
        {
            speed += 10;
            gasoline_expense += 0.4f;
        }
        protected void brake()
        {
            speed -= 10;
            gasoline_expense -= 0.4f;
        }
        protected void stop()
        {
            speed = 0;
        }
        protected void mileage_calc()
        {
            mileage += speed;
            if (speed >= path) mileage = end_point;
        }
        protected void start_end()
        {
            int coordinate = end_point - mileage;
            Console.WriteLine($"\nКонечная координата: {end_point}");
            Console.WriteLine($"\nРасстояние между начальной и конечной координатами: {coordinate}\n");
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