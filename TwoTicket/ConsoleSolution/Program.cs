using System.Reflection.Metadata;

namespace ConsoleSolution
{
    abstract class DiscountType
    {

        /// <summary>
        /// Стоимость заказа
        /// </summary>
        protected decimal _price;
        
        /// <summary>
        /// Возраст покупателя
        /// </summary>
        protected int _customerYears;

        /// <summary>
        /// Скидка
        /// </summary>
        protected decimal _discount;

        /// <summary>
        /// Функция, которая устанавливает значение скидки
        /// </summary>
        protected abstract void SetDiscount();
        
        /// <summary>
        /// Свойство для чтения цены
        /// </summary>
        public decimal Price { get { return _price; } }

        /// <summary>
        /// Свойство для чтения возраста покупателя
        /// </summary>
        public int CustomerYears { get { return _customerYears; } }


        /// <summary>
        /// Конструктор. Выполняется проверка на положительное значения стоимости
        /// </summary>
        /// <param name="price">Стоимость</param>>
        /// <param name="customerYears">Возраст покупателя</param>
        public DiscountType(decimal price, int customerYears)
        {
            _price = price;
            _customerYears = customerYears;
            isPosistivePrice();
        }

        /// <summary>
        /// Расчет стоимости
        /// </summary>
        /// <returns>стоимость с учетом скидки</returns>
        public abstract decimal Calculate();
        
        /// <summary>
        /// Проверка что стоимость положительная
        /// </summary>
        /// <param name="price">стомость для проверки</param>
        /// <returns>статус что является ли стоимость положительной</returns>
        public void isPosistivePrice()
        {
            if (_price < 0) throw new Exception("Стоимость отрицательная");
        }

    }
    class NewYearDiscount : DiscountType
    {
        public NewYearDiscount(decimal price, int customerYears) : base(price, customerYears) { }

        protected override void SetDiscount()
        {
            _discount = 0.15m;
        }

        public override decimal Calculate()
        {
            decimal result = _price - _discount;
            return result < 0 ? 0 : result;
        }
    }

    class LoyaltyDiscount : DiscountType
    {
        public LoyaltyDiscount(decimal price, int customerYears) : base(price, customerYears) { }

        protected override void SetDiscount()
        {
            _discount = 0.02m;
        }

        public override decimal Calculate()
        {
            decimal result = _price - _discount;
            return result < 0 ? 0 : result;
        }
    }

    class BlackFridayDiscount : DiscountType
    {
        public BlackFridayDiscount(decimal price, int customerYears) : base(price, customerYears) { }

        protected override void SetDiscount()
        {
            _discount = 0.30m;
        }

        public override decimal Calculate()
        {
            decimal result = _price - _discount;
            return result < 0 ? 0 : result;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            DiscountType newYear = new NewYearDiscount(1000, 0);
            Console.WriteLine(newYear.Calculate());

            DiscountType loalty = new LoyaltyDiscount(500, 5);
            Console.WriteLine(loalty.Calculate());

            DiscountType blackFriday = new BlackFridayDiscount(100, 0);
            Console.WriteLine(blackFriday.Calculate());
        }
    }
}
