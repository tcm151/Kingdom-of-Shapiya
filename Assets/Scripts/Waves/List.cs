
using System.Collections.Generic;

namespace KOS.Waves
{
    static public class List
    {
        static private System.Random rng = new System.Random();
        static public void Shuffle<T>(this IList<T> list)
        {
            int i = list.Count;
            while (i > 1)
            {  
                i--;
                int k = rng.Next(i + 1);
                T value = list[k];
                list[k] = list[i];
                list[i] = value;
            }
        }
    }
}