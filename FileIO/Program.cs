using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    class Program
    {
        static void Main(string[] args)
        {
            // To Do: make your program do something
            
            // Check and see if our toy file already exists
            if (File.Exists(@"test.zip"))
            {
                Console.Write("File already exists; overwrite?  (Y/N) ");
                string answer = Console.ReadLine();
                if (answer == "Y" || answer == "y")
                {
                    File.Delete(@"test.zip");   // make it go away
                }
                else
                {
                    Console.WriteLine("Aborting...");   // run away!!!
                }
            }

            int numFiles = 0;

            // Create our zip file and open it
            using (FileStream zipToOpen = File.Create(@"test.zip"))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    Random random = new Random();
                    string fileName;

                    // make maxI directories
                    int maxI = random.Next(10);
                    for (int i = 0; i < maxI; i++)
                    {
                        // make maxJ files
                        int maxJ = random.Next(10);
                        for (int j = 0; j < maxJ; j++)
                        {
                            fileName = i.ToString() + @"\" + j.ToString() + ".txt";
                            CreateFile(archive, fileName, random.Next().ToString());
                            numFiles++;
                        }
                    }
                }
            }

            Console.WriteLine(numFiles.ToString() + " files created.");
            Console.ReadLine();
        }

        static void CreateFile(ZipArchive zipArchive, string fileName, string content)
        {
            ZipArchiveEntry entry = zipArchive.CreateEntry(fileName);
            using (StreamWriter writer = new StreamWriter(entry.Open()))
            {
                writer.Write(content);
            }
        }
    }
}
