
namespace GameMehanics.WorldGenerator {
    public static class ArrayExtensions {
        public static string Debug(this int[,] array) {
            string msg = "";

            for (int y = 0, rows = array.GetUpperBound(0) + 1; y < array.Length / rows; y++) {
                for (int x = 0; x < rows; x++)
                    msg += array[y, x] + " ";
                msg += "\n";
                }

            return msg;
            }
        }
    }