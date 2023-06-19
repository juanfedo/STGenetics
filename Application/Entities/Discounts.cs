﻿namespace Application.Entities
{
    public static class Discounts
    {
        public static (int, float) QuantityDiscount(int amount, float price) =>
            amount switch
            {
                > 200 => (amount, price + (price * 0.03f)),
                > 50 => (amount, price + (price * 0.05f)),
                _ => (amount, price)
            };

        public static (int, float) BulkDiscount(int amount, float price) =>
            amount switch
            {
                <= 300 => (amount, price + 1000),
                _ => (amount, price)
            };
    }
}
