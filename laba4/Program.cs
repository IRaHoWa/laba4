using System;
using System.Collections.Generic;

namespace BehavioralPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Паттерн 'Стратегия' ");
            var context = new StrategyContext(new ConcreteStrategyA());
            context.ExecuteStrategy();

            context.SetStrategy(new ConcreteStrategyB());
            context.ExecuteStrategy();

            Console.WriteLine(" Паттерн 'Цепочка обязанностей'");
            var handlerA = new ConcreteHandlerA();
            var handlerB = new ConcreteHandlerB();
            handlerA.SetNext(handlerB);

            Console.WriteLine("Отправляем запрос 'A':");
            handlerA.HandleRequest("A");

            Console.WriteLine("Отправляем запрос 'B':");
            handlerA.HandleRequest("B"); 

            Console.WriteLine("Отправляем запрос 'C':");
            handlerA.HandleRequest("C"); 

            Console.WriteLine(" Паттерн 'Итератор' ");
            var collection = new ConcreteCollection<string>();
            collection.Add("Первый");
            collection.Add("Второй");
            collection.Add("Третий");

            var iterator = collection.CreateIterator();
            Console.WriteLine("Итерация по коллекции:");
            while (iterator.MoveNext())
            {
                Console.WriteLine(iterator.Current);
            }
        }
    }
    // Стратегия
    public interface IStrategy
    {
        void Execute();
    }

    public class ConcreteStrategyA : IStrategy
    {
        public void Execute()
        {
            Console.WriteLine("Выполняется стратегия A.");
        }
    }

    public class ConcreteStrategyB : IStrategy
    {
        public void Execute()
        {
            Console.WriteLine("Выполняется стратегия B.");
        }
    }

    public class StrategyContext
    {
        private IStrategy _strategy;

        public StrategyContext(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void ExecuteStrategy()
        {
            _strategy.Execute();
        }
    }
    // Цепочка обязанностей
    public abstract class Handler
    {
        protected Handler _nextHandler;

        public void SetNext(Handler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public virtual void HandleRequest(string request)
        {
            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(request);
            }
            else
            {
                Console.WriteLine("Нет обработчика для этого запроса.");
            }
        }
    }

    public class ConcreteHandlerA : Handler
    {
        public override void HandleRequest(string request)
        {
            if (request == "A")
            {
                Console.WriteLine("Handler A обработал запрос.");
            }
            else
            {
                base.HandleRequest(request);
            }
        }
    }

    public class ConcreteHandlerB : Handler
    {
        public override void HandleRequest(string request)
        {
            if (request == "B")
            {
                Console.WriteLine("Handler B обработал запрос.");
            }
            else
            {
                base.HandleRequest(request);
            }
        }
    }
    // Итератор
    public interface IIterator<T>
    {
        T Current { get; }
        bool MoveNext();
        void Reset();
    }

    public interface IIterableCollection<T>
    {
        IIterator<T> CreateIterator();
    }

    public class ConcreteCollection<T> : IIterableCollection<T>
    {
        private List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
        }

        public IIterator<T> CreateIterator()
        {
            return new ConcreteIterator<T>(this);
        }

        public int Count => _items.Count;

        public T this[int index] => _items[index];
    }

    public class ConcreteIterator<T> : IIterator<T>
    {
        private ConcreteCollection<T> _collection;
        private int _position = -1;

        public ConcreteIterator(ConcreteCollection<T> collection)
        {
            _collection = collection;
        }

        public T Current => _collection[_position];

        public bool MoveNext()
        {
            if (_position < _collection.Count - 1)
            {
                _position++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}