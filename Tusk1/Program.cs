namespace Module8_1
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
                Delete(directoryInfo);
            }
            else
            {
                Console.WriteLine("Такой директории не существует!");
            }
        }
        public static void Delete(DirectoryInfo directoryInfo)
        {
            try
            {
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    if (DateTime.Now - file.LastAccessTime > TimeSpan.FromMinutes(30))
                        file.Delete();
                }
                foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
                {
                    Delete(dir);
                    if (DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(30))
                        dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}