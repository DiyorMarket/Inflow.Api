namespace Inflow.Domain.Exeptions
{
    public class LowStockException : Exception
    {
        public LowStockException() { }
        public LowStockException(string message) : base(message) { }
    }
}
