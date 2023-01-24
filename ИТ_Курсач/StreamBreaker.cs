using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ИТ_Курсач
{
    public class StreamMy
    {



        const int width = 64; // количество байтов в окне (размер окна )

        const long seed = 2227; // хэш сид 

        long mask = (1 << 16) - 1; // хэш-код: 16 дает вам ~ 64 тыс.фрагменты
        // Чем больше 1-бит в маске, тем больше хэшей мы исключим и тем больше будут наши фрагменты.

        const int bufferSegmentSize = width * 1024;

      
        public IEnumerable<Segment> GetSegments(Stream stream, long length, HashAlgorithm hasher)
        {
            long maxSeed, hash, last, pos;
            byte[] buffer, circle;
            int circleIndex;
            InicilisationVarible(out maxSeed, out buffer, out circle, out hash, out circleIndex, out last, out pos);

            for (int i = 0; i < width; i++) maxSeed *= maxSeed;

            while (true)
            {
                // Получите несколько байтов для работы(не позволяйте ему считывать прошлую длину)
                var bytesRead = stream.Read(buffer, 0, (int)Math.Min(bufferSegmentSize, length - pos));
                // считывает несколько байтов из потока и перемещает позицию в потоке на считаные байты в данном случае все начиная с 0 

                for (int i = 0; i < bytesRead; i++)
                {
                    pos++;

                    hash = buffer[i] + ((hash - (maxSeed * circle[circleIndex])) * seed);

                    circle[circleIndex++] = buffer[i];


                    if (circleIndex == width) circleIndex = 0;




                    if ( ((hash | mask) == hash) || (pos == length)) 
                    {
                        //примените строгий хэш к оставшимся байтам в циклической очереди...
                        hasher.TransformFinalBlock(circle, 0, circleIndex == 0 ? width : circleIndex);

                        // вычисляет хэш значение для заданной области 

                       
                        yield return new Segment(last, pos - last, hasher.Hash);
                        last = pos;

                        //сбросьть
                        hash = 0;
                        for (int j = 0; j < width; j++) circle[j] = 0;

                        circleIndex = 0;

                        hasher.Initialize();
                    }
                    else
                    {
                        if (circleIndex == 0) hasher.TransformBlock(circle, 0, width, circle, 0);


                        // TransformBlock вычисляет хэш значение для заданной области входного массива 
                        // и копирует указанную область в выходной массив из circle в circle 
                    }
                }
                if (bytesRead == 0) break;
            }
        }

        private static void InicilisationVarible(out long maxSeed, out byte[] buffer, out byte[] circle, out long hash, out int circleIndex, out long last, out long pos)
        {
            maxSeed = seed;
            buffer = new byte[bufferSegmentSize];
            circle = new byte[width];
            hash = 0L;
            circleIndex = 0;
            last = 0L;
            pos = 0L;
            //позиция, в которой мы находимся в диапазоне потока, который мы читаем
        }
    }
}
