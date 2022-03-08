namespace CliFlowerShop.DomainModel
{
    public class Bundle
    {
        public Bundle(int size, decimal cost)
        {
            Size = size;
            Cost = cost;
        }

        public int Size { get; }

        public decimal Cost { get; }
    }
}