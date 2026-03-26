// Практическая работа №2. Вариант 5 - Животное
// Классы: Animal, AnimalList, MyApplication, Program

using System;
using System.Collections.Generic;
using System.IO;

// ===== Листинг 2.4 — Точка входа =====
new MyApplication().Run();

// ===== Листинг 2.1 — Класс Animal =====
public class Animal : IComparable<Animal>
{
    public string Name { get; set; }       // имя животного
    public string AnimalClass { get; set; } // класс животного
    public double AvgWeight { get; set; }  // средний вес

    public Animal(string name, string animalClass, double avgWeight)
    {
        Name = name;
        AnimalClass = animalClass;
        AvgWeight = avgWeight;
    }

    // Сравнение по среднему весу (для сортировки)
    public int CompareTo(Animal other)
    {
        return this.AvgWeight.CompareTo(other.AvgWeight);
    }

    public override string ToString()
    {
        return $"Имя: {Name}, Класс: {AnimalClass}, Средний вес: {AvgWeight} кг";
    }
}

// ===== Листинг 2.2 — Класс AnimalList =====
public class AnimalList
{
    private List<Animal> list = new List<Animal>(); // коллекция List<T>
    private int currentIndex = -1;                  // индекс текущего элемента

    // Добавление элемента в список
    public void Add(Animal a)
    {
        list.Add(a);
        if (currentIndex == -1)
            currentIndex = 0;
    }

    // Удаление текущего элемента из списка
    public void RemoveCurrent()
    {
        if (currentIndex == -1) return;
        list.RemoveAt(currentIndex);
        if (list.Count == 0)
            currentIndex = -1;
        else if (currentIndex >= list.Count)
            currentIndex = list.Count - 1;
    }

    // Перемещение в начало списка
    public void MoveFirst()
    {
        if (list.Count > 0) currentIndex = 0;
    }

    // Перемещение в конец списка
    public void MoveLast()
    {
        if (list.Count > 0) currentIndex = list.Count - 1;
    }

    // Перемещение на следующий элемент
    public void MoveNext()
    {
        if (currentIndex < list.Count - 1) currentIndex++;
    }

    // Перемещение на предыдущий элемент
    public void MovePrev()
    {
        if (currentIndex > 0) currentIndex--;
    }

    // Получение текущего элемента
    public Animal GetCurrent() => currentIndex >= 0 ? list[currentIndex] : null;

    // Установка нового значения текущего элемента
    public void SetCurrent(Animal a)
    {
        if (currentIndex >= 0)
            list[currentIndex] = a;
    }

    // Сортировка по возрастанию среднего веса
    public void SortAsc()
    {
        list.Sort();
        currentIndex = 0;
    }

    // Сортировка по убыванию среднего веса
    public void SortDesc()
    {
        list.Sort((a, b) => b.CompareTo(a));
        currentIndex = 0;
    }

    // Вывод всех элементов списка через StreamWriter
    public void PrintAll(StreamWriter writer)
    {
        if (list.Count == 0) { writer.WriteLine("Список пуст."); return; }
        foreach (var a in list)
            writer.WriteLine(a);
    }

    public int Count => list.Count;
}

// ===== Листинг 2.3 — Класс MyApplication =====
public class MyApplication
{
    private AnimalList animalList = new AnimalList();
    private StreamWriter writer = new StreamWriter(Console.OpenStandardOutput());
    private StreamReader reader = new StreamReader(Console.OpenStandardInput());

    public void Run()
    {
        writer.AutoFlush = true;

        while (true)
        {
            writer.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
            writer.WriteLine("1. Добавить животное");
            writer.WriteLine("2. Удалить текущее животное");
            writer.WriteLine("3. Показать всех животных");
            writer.WriteLine("4. Перемещение по списку");
            writer.WriteLine("5. Изменить текущее животное");
            writer.WriteLine("6. Сортировка");
            writer.WriteLine("0. Выход");
            writer.Write("Выбор: ");

            string choice = reader.ReadLine();

            switch (choice)
            {
                case "1": AddAnimal(); break;
                case "2": RemoveAnimal(); break;
                case "3": ShowAll(); break;
                case "4": Navigate(); break;
                case "5": EditCurrent(); break;
                case "6": Sort(); break;
                case "0": return;
                default: writer.WriteLine("Неверный выбор."); break;
            }
        }
    }

    private void AddAnimal()
    {
        writer.Write("Имя животного: ");
        string name = reader.ReadLine();
        writer.Write("Класс животного: ");
        string animalClass = reader.ReadLine();
        writer.Write("Средний вес (кг): ");
        double avgWeight = double.Parse(reader.ReadLine());
        animalList.Add(new Animal(name, animalClass, avgWeight));
        writer.WriteLine("Добавлено.");
    }

    private void RemoveAnimal()
    {
        var a = animalList.GetCurrent();
        if (a == null) { writer.WriteLine("Список пуст."); return; }
        animalList.RemoveCurrent();
        writer.WriteLine($"Удалено: {a}");
    }

    private void ShowAll()
    {
        writer.WriteLine("\n--- Список животных ---");
        animalList.PrintAll(writer);
        var cur = animalList.GetCurrent();
        if (cur != null)
            writer.WriteLine($">>> Текущий: {cur}");
    }

    private void Navigate()
    {
        writer.WriteLine("1. В начало  2. В конец  3. Следующий  4. Предыдущий");
        string choice = reader.ReadLine();
        switch (choice)
        {
            case "1": animalList.MoveFirst(); break;
            case "2": animalList.MoveLast(); break;
            case "3": animalList.MoveNext(); break;
            case "4": animalList.MovePrev(); break;
        }
        writer.WriteLine($"Текущий: {animalList.GetCurrent()}");
    }

    private void EditCurrent()
    {
        if (animalList.GetCurrent() == null) { writer.WriteLine("Список пуст."); return; }
        writer.Write("Новое имя: ");
        string name = reader.ReadLine();
        writer.Write("Новый класс: ");
        string animalClass = reader.ReadLine();
        writer.Write("Новый средний вес (кг): ");
        double avgWeight = double.Parse(reader.ReadLine());
        animalList.SetCurrent(new Animal(name, animalClass, avgWeight));
        writer.WriteLine("Обновлено.");
    }

    private void Sort()
    {
        writer.WriteLine("1. По возрастанию веса  2. По убыванию веса");
        string choice = reader.ReadLine();
        if (choice == "1") animalList.SortAsc();
        else animalList.SortDesc();
        writer.WriteLine("Отсортировано.");
        animalList.PrintAll(writer);
    }
}

