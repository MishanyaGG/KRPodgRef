using System;
using System.Collections.Generic;

namespace ConsoleSolution
{
    /*
     * Система обработки заказов в кафе. 
     * Код написан стажером, содержит дублирование, магические строки, отсутствие структурного подхода и не обрабатывает краевые случаи.
     * Задание:
     * 1) Разбейте код на классы с четкой зоной ответственности (Модель заказа, Калькулятор стоимости, Принтер чека).
     * 2) Замените строковые литералы на enum.
     * 3) Замените два разных списка (Items и Prices) на единый список структурированных данных (класс/структура OrderItem).
     * 4) Обработайте краевые случаи (пустой заказ, неизвестный тип оплаты).
     */
    class Program
    {
        private const double PaymentCard = 0.05;
        private const double PaymentCash = 0.5;
        static void Main()
        {
            var order = new Order(
                new List<OrderItem>
                {
                    new OrderItem("Burger", 5.99),
                    new OrderItem("Fries",2.49)
                }, 
                PaymentTypes.Card, 
                StatusType.New
            );

            ProcessOrder(order);
        }

        /// <summary>
        /// Офофрмление заказа
        /// </summary>
        /// <param name="order"></param>
        static void ProcessOrder(Order order)
        {
            double total = 0;
            
            foreach(OrderItem el in order.OrderItems)
            {
                total += el.Price;
            }

            switch (order.PaymentType)
            {
                case PaymentTypes.Card:
                    {
                        total += total * PaymentCard; break;
                    }

                case PaymentTypes.Cash:
                    {
                        total += PaymentCash; break;
                    }

                case PaymentTypes.Bonus:
                    {
                        if (total > 100) { total -= 5; } break;
                    }
            }



            switch (order.StatusType)
            {
                case StatusType.New:
                    {
                        Console.WriteLine("Order accepted"); break;
                    }
                case StatusType.Cooking:
                    {
                        Console.WriteLine("Cooking..."); break;
                    }

                case StatusType.Ready:
                    {
                        Console.WriteLine("Ready for pickup"); break;
                    }

                case StatusType.Done:
                    {
                        Console.WriteLine("Already given"); break;
                    }
            }

            Console.WriteLine("Total: " + total);
            PrintReceipt(order, total);
        }

        static void PrintReceipt(Order o, double total)
        {
            Console.WriteLine("----Receipt---");

            foreach (OrderItem el in o.OrderItems)
            {
                Console.WriteLine($"{el.Item} - {el.Price}");
            }

            Console.WriteLine("Total: " + total);
            Console.WriteLine("Payment: " + o.PaymentType);
            Console.WriteLine("Status: " + o.StatusType);
        }
    }

    public class OrderItem
    {
        private string _item;

        private double _price;

        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public OrderItem(string item, double price)
        {
            Item = item;
            Price = price;
        }
        
    }

    enum PaymentTypes
    {
        Card,
        Cash,
        Bonus
    }

    enum StatusType
    {
        New,
        Cooking,
        Ready,
        Done
    }

    class Order
    {
        private List<OrderItem> _orderItems;
        private PaymentTypes _paymentType;
        private StatusType _status;

        public List<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems = value; }
        }

        public PaymentTypes PaymentType
        {
            get { return _paymentType; }
            set { _paymentType = value; }
        }
        
        public StatusType StatusType
        {
            get { return _status; }
            set { _status = value; }
        }

        public Order(List<OrderItem> orderItems,  PaymentTypes paymentType, StatusType statusType)
        {
            OrderItems = orderItems;
            PaymentType = paymentType;
            StatusType = statusType;
        }

    }
}
