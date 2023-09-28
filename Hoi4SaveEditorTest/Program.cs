using System.Text.RegularExpressions;

namespace Hoi4SaveEditorTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program is very basic, because of this\nit has no way to check whether the values you enter are correct.\nI suggest you make a backup if you don't want to mess a savefile.\nIf you follow the prompts it should be fine. :)\njust making this to play around.");
            Console.WriteLine("Enter File Name. e.g JAP_1937_10_16_01.hoi4");
            Console.Write("File Name:");


            // Getting File
            string saveNameB4 = Console.ReadLine();
            string saveName = saveNameB4.Replace(" ", "");
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(docFolder, "Paradox Interactive", "Hearts of Iron IV", "save games", saveName);
            string[] lines = File.ReadAllLines(filePath);


            //Declaring Patterns
            string tagPattern = @"player=""[A-Z]{3}""";
            string ideologyPattern = @"ideology=.*";
            string datePattern = @"date=.*";
            string ironManPattern = @"ironman=.*";

            //Getting new variables
            Console.Write("Enter new player country tag (GER, ENG etc): ");
            string tag = Console.ReadLine();
            Console.Write("Enter new ideology (fascism, stalinism , despotism, conservatism): ");
            string ideology = Console.ReadLine();
            Console.Write("Enter new year (1936 or above): ");
            string newYear = Console.ReadLine();
            Console.Write("Ironman off (0) or on (1): ");
            string ironMan = Console.ReadLine();


            // finding line
            for (int i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], ironManPattern))
                {

                    lines[i] = $"ironman={ironMan}";
                    break;
                }
            }



            for (int i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], tagPattern))
                {

                    lines[i] = $"player=\"{tag.ToUpper()}\"";
                    break; 
                }
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], ideologyPattern))
                {

                    lines[i] = $"ideology=\"{ideology.ToLower()}\"";
                    break;
                }
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], datePattern))
                {
                    string testDate = lines[i];
                    string[] parts = testDate.Split('"');

                    if (parts.Length >= 3)
                    {
                        string yearPart = parts[1];
                        string oldYear = "";

                        

                        string[] dateParts = yearPart.Split('.');
                        dateParts[0] = oldYear;
                        string afterYear = "";
                        for (int ia = 1; ia < dateParts.Length; ia++)
                        {
                            afterYear += "." + dateParts[i];
                        }
                        lines[i] = $"date = \"{newYear}" + $"{afterYear}\"";

                       
                    }

                    
                    break;
                }
            }

            //writing the lines
            File.WriteAllLines(filePath, lines);
            Console.WriteLine("File Edited Successfully.");

        }
    }
}