using UnityEngine;

namespace CardGuess.Utils
{
    public static class UnityRandom
    {
        static UnityRandom()
        {
            Random.InitState(System.Environment.TickCount);
        }
        
        public static int GetRandomInt(int min, int max)
        {
            return Random.Range(min, max);
        }
        
        public static float GetRandomFloat(float min, float max)
        {
            return Random.Range(min, max);
        }
    }   
}