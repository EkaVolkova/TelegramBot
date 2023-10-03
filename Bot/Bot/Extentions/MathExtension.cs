namespace Bot.Extentions
{
    public static class MathExtension
    {
        /// <summary>
        /// Рассчитывает сумму чисел в массиве
        /// </summary>
        /// <param name="arrayNum"></param>
        /// <returns></returns>
        public static double SumArray(double[] arrayNum)
        {
            double summ = 0;
            foreach(var num in arrayNum)
            {
                summ += num;
            }
            return summ;
        }
    }



}
