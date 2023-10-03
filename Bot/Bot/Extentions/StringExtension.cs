using System;
using System.Collections.Generic;

namespace Bot.Extentions
{
    public static class StringExtension
    {
        
        /// <summary>
        /// Преобразует строку в массив 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double[] FromStringToArrayNum(string s)
        {
            //Проверка на существование
            if (string.IsNullOrEmpty(s))
                return null;

            //Разделение на массив строк
            var arrayString = s.Split(" ");
            //создание массива чисел аналогичной размерности
            var arrayNum = new double[arrayString.Length];

            
            for (int i = 0; i < arrayString.Length; i++)
            {
                //Преобразование из строки в double и
                //попытка спастись от недопонимания double числа в формате 0.0, а не 0,0
                if (!double.TryParse(arrayString[i].Replace('.',','), out arrayNum[i]))
                    throw new ArgumentException("Введите числа");
            }
            return arrayNum;
        }
        
    }


}
