namespace Application.Extensions
{
    public static class Monad
    {
        public static (int, float) Pipe(this (int amount, float price) values, Func<int, float, (int, float)> fnOut) =>
            fnOut(values.amount, values.price);
    }
}
