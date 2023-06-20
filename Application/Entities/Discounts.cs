namespace Application.Entities
{
    public static class Discounts
    {
        public static (int, float) QuantityDiscount(int amount, float price) =>
            amount switch
            {
                > 200 => (amount, ((price - (price * 0.03f)) * amount)),
                > 50 => (amount, ((price - (price * 0.05f)) * amount)),
                _ => (amount, price)
            };

        public static (int, float) FreightDiscount(int amount, float price) =>
            amount switch
            {
                <= 300 => (amount, price + Constants.Constants.FREIGHT),
                _ => (amount, price)
            };
    }
}
