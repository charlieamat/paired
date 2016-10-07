namespace Paired.Scripts.Order
{
    public class OrderFactory
    {
        public int[] Create(int count)
        {
            var order = new int[count];
            for (int i = 0; i < count; i++)
            {
                order[i] = i % (count / 2);
            }

            Shuffle(order);

            return order;
        }

        private void Shuffle(int[] array)
        {
            FisherYatesShuffle(array);
        }

        private void FisherYatesShuffle<T>(T[] array)
        {
            var random = new System.Random();
            for (int i = array.Length; i > 1; i--)
            {
                int j = random.Next(i);
                T tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
        }
    }
}