using System.Text;

namespace ConsoleGraphics
{
    public static class Graphics
    {
        public static void DrawField(ECell[][] field)
        {
            int indexSize = MathIndexSize(field.Length);
            char[][] resultField = MakeField(field.Length, indexSize);

            FillField(ref resultField, field, indexSize);

            for (int i = 0;i < resultField.Length;i++)
            {
                for (int j = 0;j < resultField.Length;j++)
                {
                    char c = resultField[j][i];

                    if (c == 'O')
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else if (c == 'X')
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }

                    Console.Write(c);

                    Console.ResetColor();
                }

                Console.Write('\n');
            }
        }

        private static int MathIndexSize(int fieldSize)
        {
            return (int) Math.Ceiling((float) fieldSize / 10);
        }

        private static char[][] MakeField(int fieldSize, int indexSize)
        {
            int fieldSideSize = fieldSize * 2 + indexSize;

            char[][] resultField = new char[fieldSideSize][];

            for (int i = 0;i < fieldSideSize;i++)
            {
                resultField[i] = new char[fieldSideSize];
            }

            return resultField;
        }

        private static void FillField(ref char[][] resultField, ECell[][] field, int indexSize)
        {
            for (int x = 0;x < resultField.Length;x++)
            {
                for (int y = 0;y < resultField.Length;y++)
                {
                    resultField[x][y] = FillCell(x, y, field, indexSize);
                }
            }
        }

        private static char FillCell(int x, int y, ECell[][] field, int indexSize)
        {
            if (y % 2 == 1 && y >= indexSize)
            {
                return '-';
            }
            else if (x % 2 == 1 && x >= indexSize)
            {
                return '|';
            }
            else if (x < indexSize || y < indexSize)
            {
                return FillIndex(x, y, indexSize);
            }
            else
            {
                if (field[(x - indexSize) / 2][(y - indexSize) / 2] == ECell.Alive)
                {
                    return 'X';
                }
                else
                {
                    return 'O';
                }
            }
        }

        private static char FillIndex(int x, int y, int indexSize)
        {
            if (x < indexSize && y < indexSize)
            {
                return ' ';
            }

            string index = (((x < indexSize ? y : x) - indexSize) / 2).ToString();

            if (index.Length < indexSize)
            {
                var indexZerosCount = (int) (indexSize - Math.Ceiling((double) index.Length / 10));
                var builder = new StringBuilder(indexZerosCount);

                for (int i = 0;i < indexZerosCount;i++)
                {
                    builder.Append('0');
                }

                index = builder + index;
            }

            return index[x < indexSize ? x : indexSize - y - 1];
        }
    }
}