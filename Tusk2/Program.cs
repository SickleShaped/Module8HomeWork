namespace Module8_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до папки: ");
            string path = Console.ReadLine();
            var directoryInfo = new DirectoryInfo(path);
            if (Directory.Exists(directoryInfo.FullName))
            {
                long dyrectoryBytes = 0;
                GetBytes(directoryInfo, ref dyrectoryBytes);
                Console.WriteLine(dyrectoryBytes);
            }
            else
            {
                Console.WriteLine("Такой директории не существует!");
            }
        }

        public static void GetBytes(DirectoryInfo directoryInfo, ref long dyrectoryBytes)
        {
            try
            {
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    dyrectoryBytes += file.Length;
                }
                foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
                {
                    GetBytes(dir, ref dyrectoryBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}

