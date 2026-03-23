using System.Diagnostics;

public class Program
{
    static void Main(string[] args)
    {
        SortStatistics statistics = new SortStatistics();
        Sorter sorter = new Sorter();
        sorter.InitCollection();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=========== MENU ===========");
            Console.WriteLine("1. Fill collection with automatic reviews.");
            Console.WriteLine("2. Add new review.");
            Console.WriteLine("3. Delete review.");
            Console.WriteLine("4. Print collection.");
            Console.WriteLine("5. Sort collection.");
            Console.WriteLine("6. Print main steps of algorithm.");
            Console.WriteLine("7. Print algorithm statistics.");
            Console.WriteLine("8. Print movie rating.");
            Console.WriteLine("9. Find moda.");
            Console.WriteLine("10. Find mediana.");
            Console.WriteLine("11. Show statistics for each rating.");
            Console.WriteLine("0. Exit.");
            Console.Write("Your choice: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                sorter.GenerateControlData();
            }
            else if (choice == "2")
            {
                string movieTitle = "";
                
                if (sorter.count < 1)
                {
                    Console.Write("Enter a movie title: ");
                    movieTitle = Console.ReadLine();
                }
                else
                {
                    movieTitle = sorter.elements[0].movieTitle;
                }

                Console.Write("Enter a username: ");
                string userName = Console.ReadLine();

                while (true)
                {
                    Console.Write("Rate film from 1 to 10: ");
                    string ratingStr = Console.ReadLine();

                    bool check = int.TryParse(ratingStr, out int rating);

                    if (!check || rating < 1 || rating > 10)
                    {
                        Console.WriteLine("Ivalid input. Try again.");
                        continue;
                    }

                    Record newRecord = new Record(movieTitle, userName, rating);
                    sorter.AddRecord(newRecord);
                    break;
                }
            }
            else if (choice == "3")
            {
                if (sorter.count == 0)
                {
                    Console.WriteLine("Collection is empty.");
                    continue;
                }
                
                sorter.PrintCollection();

                while (true) 
                {
                    Console.Write("Enter an ID of review you want to delete: ");
                    string idStr = Console.ReadLine();

                    bool check = int.TryParse(idStr, out int id);

                    if (!check || sorter.GetElementIndex(id) == -1)
                    {
                        Console.WriteLine("Ivalid input. Try again.");
                        continue;
                    }

                    sorter.RemoveRecord(id);
                    break;
                }

            }
            else if (choice == "4")
            {
                if (sorter.count == 0)
                {
                    Console.WriteLine("Collection is empty.");
                    continue;
                }

                sorter.PrintCollection();
            }
            else if (choice == "5")
            {
                if (sorter.count == 0)
                {
                    Console.WriteLine("Collection is empty.");
                    continue;
                }

                sorter.SortCollection(statistics);
            }
            else if (choice == "6")
            {
                if (sorter.GetMainSteps() == null)
                {
                    Console.WriteLine("Impossible to show algorithm steps. Collection hasn`t been sorted.");
                    continue;
                }

                sorter.PrintIntermediateSteps();
            }
            else if (choice == "7")
            {
                if (sorter.GetMainSteps() == null)
                {
                    Console.WriteLine("Impossible to show algorithm statistics. Collection hasn`t been sorted.");
                    continue;
                }

                sorter.PrintStatistics(statistics);
            }
            else if (choice == "8")
            {
                float movieRaiting = sorter.CountRaiting();

                if (movieRaiting == -1)
                {
                    Console.WriteLine("Impossible to count raiting. Movie has no reviews.");
                    continue;
                }

                Console.WriteLine($"Movie rating: {movieRaiting}");
            }
            else if (choice == "9")
            {
                if (sorter.count == 0)
                {
                    Console.WriteLine("Collection is empty.");
                    continue;
                }

                if (sorter.GetMainSteps() == null)
                {
                    Console.WriteLine("Impossible to find moda. Collection hasn`t been sorted.");
                    continue;
                }

                int[] moda = sorter.GetModa();

                if (moda == null)
                {
                    Console.WriteLine("There is no moda in this collection");
                    continue;
                }

                Console.Write("Moda: ");
                for (int i = 0; i < moda.Length; i++)
                {
                    Console.Write($"{moda[i]} ");
                }
            }
            else if (choice == "10")
            {
                if (sorter.count == 0)
                {
                    Console.WriteLine("Collection is empty.");
                    continue;
                }

                if (sorter.GetMainSteps() == null)
                {
                    Console.WriteLine("Impossible to find mediana. Collection hasn`t been sorted.");
                    continue;
                }

                Console.WriteLine("Mediana: " + sorter.GetMediana());
            }
            else if (choice == "11")
            {
                if (sorter.count == 0)
                {
                    Console.WriteLine("Collection is empty.");
                    continue;
                }

                int[] raitingStatistics = sorter.StatisticsForEachRating();
                for (int i = 0; i < raitingStatistics.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {raitingStatistics[i]}");
                }
            }
            else if (choice == "0")
            {
                break;
            }
            else 
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        
    }
}

