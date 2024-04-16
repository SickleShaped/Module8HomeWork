namespace Module8_3
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
                Console.WriteLine("Изначальный размер папки: " + dyrectoryBytes);

                long deletedBytes = 0;
                uint deletedFiles = 0;
                Delete(directoryInfo, ref deletedBytes, ref deletedFiles);
                Console.WriteLine("Удалено файлов: " + deletedFiles);
                Console.WriteLine("Размер удаленных файлов: " + deletedBytes);

                dyrectoryBytes = 0;
                GetBytes(directoryInfo, ref dyrectoryBytes);
                Console.WriteLine("Конечный размер папки: " + dyrectoryBytes);
            }
            else
            {
                Console.WriteLine("Такой директории не существует!");
            }
        }

        public static void Delete(DirectoryInfo directoryInfo, ref long deletedBytes, ref uint deletedFiles)
        {
            try
            {
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    if (DateTime.Now - file.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        deletedFiles++;
                        deletedBytes += file.Length;
                        file.Delete();
                    }

                }
                foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
                {
                    Delete(dir, ref deletedBytes, ref deletedFiles);
                    if (DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(30))
                        dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
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