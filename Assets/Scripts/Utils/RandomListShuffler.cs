using System.Collections.Generic;

namespace CardGuess.Utils
{
    public static class RandomListShuffler
    {
        public static List<T> ShuffleList<T>(List<T> list)
        {
            List<T> shuffledList = new List<T>(list);
            System.Random random = new System.Random();
            
            int n = shuffledList.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (shuffledList[i], shuffledList[j]) = (shuffledList[j], shuffledList[i]);
            }

            return shuffledList;
        }
    }   
}