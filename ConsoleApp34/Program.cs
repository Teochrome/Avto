using Avtomobili;
Random rand = new Random();
for (int i = 0; i < 5; i++) Avto.cars[i] = new Avto(i + 1, rand.Next(5, 7) * 10, rand.Next(5, 8));
for (int i = 0; i < 3; i++) Trucks.truck[i] = new Trucks(i + 1, rand.Next(14, 25) * 10, rand.Next(27, 42));
for (int i = 0; i < 2; i++) Buses.bus[i] = new Buses(i + 1, rand.Next(15, 30) * 10, rand.Next(33, 40), 0);
Console.WriteLine("Выберите тип транспорта: (л/г/а)");
char transport_type_choose = char.Parse(Console.ReadLine());
switch (transport_type_choose)
{
    case 'л':
        for (int i = 0; i < Avto.cars.Length; i++) Avto.cars[i].cout();
        Console.WriteLine("Выберите транспорт, которым вы желаете управлять (номер):");
        Avto.transport_choose = int.Parse(Console.ReadLine());
        Avto.cars[Avto.transport_choose - 1].accident();
        Avto.cars[Avto.transport_choose - 1].move(Avto.cars[Avto.transport_choose - 1].path);
        break;
    case 'г':
        for (int i = 0; i < Trucks.truck.Length; i++) Trucks.truck[i].cout();
        Console.WriteLine("Выберите транспорт, которым вы желаете управлять (номер):");
        Avto.transport_choose = int.Parse(Console.ReadLine());
        Trucks.truck[Avto.transport_choose - 1].accident();
        Trucks.truck[Avto.transport_choose - 1].move(Trucks.truck[Avto.transport_choose - 1].path);
        break;
    case 'а':
        for (int i = 0; i < Buses.bus.Length; i++) Buses.bus[i].cout();
        Console.WriteLine("Выберите транспорт, которым вы желаете управлять (номер):");
        Avto.transport_choose = int.Parse(Console.ReadLine());
        Buses.bus[Avto.transport_choose - 1].accident();
        Buses.bus[Avto.transport_choose - 1].move(Buses.bus[Avto.transport_choose - 1].path);
        break;
}