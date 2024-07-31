using System.Collections;
using System.Reflection;
using System.Text;


namespace ConsoleApp2
{
  
    //реализуйте демонстрацию ковариантности делегатов при помощи следующей модели классов, более производный тип
    class Car 
    { 
        public string Model { get; set; }

        public Car()
        {
            Model = "Generic Car";
        }

        public override string ToString()
        {
            return $"Car model: {Model}";
        }
    }
    class Lexus : Car 
    {
        public Lexus()
        {
            Model = "Lexus 5X";
        }

        public override string ToString()
        {
            return $"Lexus model: {Model}";
        }
    }
    delegate Car CarFactory();
    delegate void CarProcessor(Car car);

    public class Program
    {

        public static void Main(string[] args)
        {

            CarFactory carFactory = CreateLexus; // назначаем метод (присваиваем значение), возвращаем Лексус делегату, который ожидает Кар

            Car car = carFactory(); // Это вызов делегата carFactory, которому мы дали название переменной Кар
            Console.WriteLine(car);

            // Назначаем метод CreateCar делегату CarProcessor
            CarProcessor carProcessor = CreateCar;

            // Вызываем метод CreateCar через делегат
            carProcessor(car);
            // Назначаем метод, принимающий Car, делегату, который ожидает Lexus
            Action<Lexus> lexusAction = ProcessCar; 
            Lexus myLexus = new Lexus();
            lexusAction(myLexus); //это пример контравариантности

            ChildName childName = CreateChild; //Child вместо Parent
            Parent parent = childName(); // Это вызов делегата childFactory, который возвращает Child
            Console.WriteLine(parent);

            // Тут принимаем Parent, но делегат ожидает Child (контравариантность)
            Action<Parent> parentAction = CreateParent;
            Child myChild = new Child();
            parentAction(myChild); 
        }
        static Lexus CreateLexus()
        {
            return new Lexus();
        }
        static void CreateCar(Car car) // передали в метод объект car типа Car
        {
            Console.WriteLine($"{car.Model}");
        }
        static Car ProcessCar()
        {
            return new Car();
        }

        static Child CreateChild()
        {
            return new Child();
        }

        static void CreateParent(Parent parent)
        {
            Console.WriteLine($"{parent.Name}");
        }
    }

    //реализуйте демонстрацию контравариантности делегатов при помощи следующей модели классов, менее производный тип
    class Parent {

        public string Name { get; set; }

        public Parent()
        {
            Name = "Vova";
        }

        public override string ToString()
        {
            return $"Parent name: {Name}";
        }
    }
    class Child : Parent {
        public Child()
        {
            Name = "Sasha";
        }

        public override string ToString()
        {
            return $"Child name: {Name}";
        }
    }
    delegate Child ChildName();
    delegate Parent parentName();
}

