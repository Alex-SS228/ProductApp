namespace ProductApp
{
    class Product
    {
        // Атрибуты класса «Изделие»
        public string Name;      // наименование
        public string Code;      // шифр
        public int Quantity;     // количество

        // Конструктор по умолчанию (без параметров)
        public Product()
        {
            Name = "Не указано";
            Code = "000";
            Quantity = 0;
        }

        // Конструктор с параметрами
        public Product(string name, string code, int quantity)
        {
            Name = name;
            Code = code;
            Quantity = quantity;
        }

        // Метод для вывода информации об изделии
        public override string ToString()
        {
            return $"Наименование: {Name}, Шифр: {Code}, Количество: {Quantity} шт.";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Список изделий ===\n");

            // Создание объектов с помощью конструктора с параметрами
            Product p1 = new Product("Болт М8", "BLT-001", 150);
            Product p2 = new Product("Гайка М8", "GKA-002", 200);
            Product p3 = new Product("Шайба 8", "SHB-003", 500);

            // Вывод объектов через ToString()
            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine(p3);

            Console.WriteLine("\n=== Изменение атрибутов объекта p1 ===\n");

            // Изменение атрибутов напрямую
            p1.Name = "Болт М10";
            p1.Code = "BLT-010";
            p1.Quantity = 75;

            Console.WriteLine(p1);

            Console.WriteLine("\n=== Объект по умолчанию ===\n");

            // Создание объекта через конструктор по умолчанию
            Product p4 = new Product();
            Console.WriteLine(p4);

            Console.ReadLine();
        }
    }
}
