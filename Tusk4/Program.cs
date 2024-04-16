using System.Runtime.Serialization.Formatters.Binary;

namespace Module8_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:/Users/Sickle-shaped/Desktop";
            var students = ReadStudentsFromBinFile(path + "/students.dat");

            var directory = new DirectoryInfo(path);
            directory.CreateSubdirectory("students");
            foreach (var student in students) //если это можно сделать лучше, а наверное можно, то как?
            {
                if (!File.Exists(path + "/students/" + student.Group + ".txt"))
                {
                    using (StreamWriter sw = File.CreateText(path + "/students/" + student.Group + ".txt"))
                    {
                        foreach (var sameGroupStudent in students)
                        {
                            if (sameGroupStudent.Group == student.Group)
                            {
                                sw.WriteLine(sameGroupStudent.Name + ", " + sameGroupStudent.Birth + ", " + sameGroupStudent.GPA);
                            }
                        }
                    }
                }
            }
        }

        [Serializable]
        class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public DateTime Birth { get; set; }
            public decimal GPA { get; set; }
        }

        static List<Student> ReadStudentsFromBinFile(string fileName)
        {
            List<Student> result = new();
            using FileStream fs = new FileStream(fileName, FileMode.Open);
            using StreamReader sr = new StreamReader(fs);

            fs.Position = 0;

            BinaryReader br = new BinaryReader(fs);

            while (fs.Position < fs.Length)
            {
                Student student = new Student();
                student.Name = br.ReadString();
                student.Group = br.ReadString();
                long dt = br.ReadInt64();
                student.Birth = DateTime.FromBinary(dt);
                student.GPA = br.ReadDecimal();

                result.Add(student);
            }
            fs.Close();
            return result;
        }
    }
}