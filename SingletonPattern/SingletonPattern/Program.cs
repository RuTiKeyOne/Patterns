﻿using System;
using System.Threading;
using SingletonPattern.Classes;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            TestNaiveSingleton();
            TestMultiThreadsSingleton();

            /*
             Паттерн Одиночка(Singleton)

             Зачем нужен данный паттер:
             1.Если нужен единственный экземпляр объекта
             2.Если нужен контроль над глобальными переменными
             3.Если нужна глобальная точка доступа

             С данным паттерном мы можем создать один экземпляр объекта в любой части программы и иметь 
             доступ к глобальным переменным. Singleton может быть однопоточным и многопоточным
            
             *Реализация данного паттерна одна из самых простых, сравнимой с паттерном стратегия
             
             Как реализовать однопоточный singleton (NaiveSingleton)
             1. Создаем класс в котором будет оснавная логика:
                      a)Скрываем конструктор для того чтобы случайно не создался новый класс
                      б)Создаем переменную которая будет статической и будет ссылаться на данный класс
                      в)Реализуем логику создания единственного объекта в методе GetInstance
             2. Мы можем использовать класс в единственном экземпляре и глобальные переменные
             Пример описан в методе TestNaiveSingleton

            *Основной минус данной реализации паттерна состоит в том что он не может в многопоточность
                      
             Как реализовать многопоточный singleton (MultiThreadsSingleton)
             1. Создаем класс в котором будет оснавная логика:
                      a)Скрываем конструктор для того чтобы случайно не создался новый класс
                      б)Создаем переменную которая будет статической и будет ссылаться на данный класс
                      в)Реализуем логику создания единственного объекта в методе GetInstance
                      Конкретно о GetInstanse, там используется двойная проверка для того чтобы
                      не лочить потоки если объект создан а просто получить его
                      
                      !Но если объект не создан, то первый поток создает его и !ВАЖНО устанавливает 
                      какие-нибудь данные полям/свойствам.

                      Остальные потоки же просто получают объект. Это прямо важно понять.
                      
             2. Мы можем использовать класс в единственном экземпляре и глобальные переменные
             Пример описан в методе TestMultiThreadsSingleton
             

            *Не начинай рассказывать на собесе о паттернах с него, иначе подумают что ты не можешь в ООП(upcast, downcast, наследование и т.д)

            Плюсы:
            +Гарантирует наличие единственного класса
            +Глобальный доступ к переменным
            +Отложенная инициализация

            Минусы:
            -Проблема с многопоточностью
            -Может маскировать плохой дизайн
            -Нарушает принцип единственной ответственности класса
            -Проблема с юнит тестамиы

            Вывод: Использовать если нужен единственный экземпляр класса и возможность работы с глобальными переменными
            !Осторожно с говнокодом

             */
        }

        public static void TestMultiThreadsSingleton()//Метод для проверки и описания многопоточного одиночки
        {
            Console.WriteLine("TestMultiThreadsSingleton result: \n"); //Сообщение для обозначения тестируемого метода

            Thread Thread1 = new Thread(() =>  //Запускаем первый поток
            {
                //Создаем класс и устанавливаем значение так как этот поток первый
                MultiThreadsSingleton Singleton = MultiThreadsSingleton.GetInstance("DOR\n");
                //Получаем значение выражения
                Console.WriteLine(Singleton.Value);
            });

            Thread Thread2 = new Thread(() =>
            {
                //Поток второй, объект уже создан и остается его только получить
                MultiThreadsSingleton Singleton = MultiThreadsSingleton.GetInstance("BAR\n");
                //Получаем значение выражения
                Console.WriteLine(Singleton.Value);
            });

            //Запускаем потоки
            Thread1.Start();

            Thread2.Start();

            /*
             Если получили одинаковые значения value то паттерн работает (получаем DOR)
             
             */
        }


        public static void TestNaiveSingleton() //Метод для проверки и описания однопоточного одиночки
        {
            Console.WriteLine("TestNaiveSingleton result: \n"); //Сообщение для обозначения тестируемого метода

            //Создаем первый объект и этот объект един для всех так как он static
            NaiveSingleton Fist = NaiveSingleton.GetInstance(); 
            
            //Значение переменной
            Console.WriteLine($"Fist: {Fist.Password}\n");

            //Создаем второй объект, он равен второму
            NaiveSingleton Second = NaiveSingleton.GetInstance();
            //Значение переменной
            Console.WriteLine($"Second: {Second.Password}\n");

            //Меняем значение переменной у общего объекта для Fist и Second
            Fist.Password = 4321;

            //Выводим значение
            Console.WriteLine($"Fist is change: {Fist.Password}\n");

            //Проверяем изменилось ли значение, да изменилось так как объект общий 
            Console.WriteLine($"Is second change ?: {Fist.Password.Equals(Second.Password)}\n");
        }
    }
}