﻿using System;
using System.Collections.Generic;
using VisitorPattern.Interfaces;
using VisitorPattern.Classes;

namespace VisitorPattern
{
    class Program
    {
        static void Main(string[] args)
        {

            List<IComponent> Goals = new List<IComponent>
            {
                new Family(LevelAffluence.Medium, true),
                new Bank(ScaleOfHazard.Hight, 30000),
                new Company(LevelPrestige.Low, 10000)
            };

            Insurer Employee = new();

            Employee.GetPrices();

            Teamlead.SendEmployees(Goals, Employee);

            Employee.GetPrices();

            Family Family = new(LevelAffluence.Low, true);

            Family.Accept(Employee);

            Employee.GetPrices();


        }

        //Паттерн посетитель 

        /*
         * Слабо распростронен в C# из-за сложности реализации
         Необходим при 
         1. Если нам необходимо работать с классом при этом нельзя изменять его
         2. Если нужно выполнить какую-нибудь операцию над элементами сложной структуры
         3. Если в объектах сложной структуры вы не хотите захламлять классы
         4. Создать поведение только для некоторых объектов сложной структуры 


         Реализация даннего паттерна
         1.Создайте интерфейс посетителя и объявите в нем методы для посещения класса (IVisitor)
         2.Создайте интерфейс элементов, которое будут посещать, объявите метод принятия (IComponent)
         3.Реализуйте интерфейс IComponent и метод принятия в конкретных классах
         4.Создайте конкретный класс посетителя, реализуйте интерфейс, методы и логику
         5.Создадим метод(лучше в отдельном классе) который будет принимать классы которые необходимо посетить и посетителя
         6. Через foreach посещаем каждый класс
         
         *Можно и без 5 и 6 пункта
         

         Плюсы+
         +Позволяет добавлять операции к объектам сложной структуры
         +Все операции находятся в одном классе
         +Посетитель накапливает состояние в себе
         
         Минусы-
         -Может привести к нарушению инкапсуляции
         -Не подходит если части сменяется иерархия 


         Вывод: Использовать для добавление операций объектам сложной структуры с постоянной иерархиея
         и с условием что нельзя изменять эти объекты


         Цитирование из книги Тепляковаэ
         
         Использовать паттерн «Посетитель» нужно тогда, когда набор типов иерархии стабилен, а набор операций — нет.
         
         

         */
    }
}
