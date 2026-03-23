using System.Diagnostics;

public class Sorter
{
    public Record[] elements;
    public int count;

    private string mainSteps = null;

    public void InitCollection()
    {
        elements = new Record[0];
        count = 0;
    }

    public string? GetMainSteps()
    {
        return mainSteps;
    }

    public void ExpandArray()
    {
        Record[] temp = new Record[(elements.Length + 1) * 2];

        for (int i = 0; i < elements.Length; i++)
        {
            temp[i] = elements[i];
        }

        elements = temp;
    }

    public void AddRecord(Record record)
    {
        if (count >= elements.Length)
        {
            ExpandArray();
        }

        elements[count] = record;
        count++;

        mainSteps = null;
    }

    public bool RemoveRecord(int reviewId)
    {
        int index = GetElementIndex(reviewId);
        if (index == -1)
        {
            return false;
        }

        for (int i = index + 1; i < count; i++)
        {
            elements[i - 1] = elements[i];
        }

        elements[count - 1] = null;
        count--;
        return true;
    }

    public int GetElementIndex(int id)
    {
        for (int i = 0; i < count; i++)
        {
            if (elements[i].reviewId == id)
            {
                return i;
            }
        }

        return -1;
    }

    public void PrintCollection()
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(elements[i]);
        }
    }

    public void GenerateControlData()
    {
        elements = new Record[15];

        Random random = new Random();

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = new Record($"Movie", $"user{i + 1}", random.Next(1, 11));
        }

        count = 15;
        mainSteps = null;
    }

    public void SortCollection(SortStatistics statistics)
    {
        mainSteps = "Initial array:\n";
        for (int i = 0; i < count; i++)
        {
            mainSteps += elements[i].ToString() + "\n";
        }

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int[] counts = new int[10];

        for (int i = 0; i < count; i++)
        {
            counts[elements[i].rating - 1]++;
        }
        statistics.passageCount++;

        stopwatch.Stop();

        mainSteps += "Counts array:\n";
        for (int i = 0; i < counts.Length; i++)
        {
            mainSteps += counts[i] + " ";
        }

        stopwatch.Start();

        for (int i = 1; i < counts.Length; i++)
        {
            counts[i] += counts[i - 1];
        }
        statistics.passageCount++;

        stopwatch.Stop();

        mainSteps += "\nCounts sums array:\n";
        for (int i = 0; i < counts.Length; i++)
        {
            mainSteps += counts[i] + " ";
        }

        stopwatch.Start();

        Record[] sortedArray = new Record[count];

        for (int i = count - 1; i >= 0; i--)
        {
            sortedArray[counts[elements[i].rating - 1] - 1] = elements[i];
            counts[elements[i].rating - 1]--;
            statistics.copyCount++;

            if (i == count / 2)
            {
                stopwatch.Stop();

                mainSteps += "\nArray in the middle of sorting:\n";
                for (int j = 0; j < count; j++)
                {
                    if (sortedArray[j] != null)
                    {
                        mainSteps += sortedArray[j].ToString() + "\n";
                    }
                    else
                    {
                        mainSteps += "null\n";
                    }
                }

                stopwatch.Start();
            }
        }
        statistics.passageCount++;

        elements = sortedArray;

        stopwatch.Stop();
        statistics.timeMs = stopwatch.Elapsed.TotalMilliseconds;

        mainSteps += "Sorted array:\n";
        for (int i = 0; i < count; i++)
        {
            mainSteps += elements[i].ToString() + "\n";
        }
    }

    public void PrintIntermediateSteps()
    {
        Console.WriteLine(mainSteps);
    }

    public void PrintStatistics(SortStatistics statistics)
    {
        Console.WriteLine($"Copy operations: {statistics.copyCount}");
        Console.WriteLine($"Time: {statistics.timeMs}");
        Console.WriteLine($"Passages through the arrays: {statistics.passageCount} " +
            $"(1 - update counts, 2 - count sums in counts, 3 - create sorted array)");
    }

    public float CountRaiting()
    {
        if (count == 0)
        {
            return -1;
        }

        int sum = 0;

        for (int i = 0; i < count; i++)
        {
            sum += elements[i].rating;
        }

        return (float)sum / count;
    }

    public int[] StatisticsForEachRating()
    {
        int[] statistics = new int[10];

        for (int i = 0; i < count; i++)
        {
            statistics[elements[i].rating - 1]++;
        }

        return statistics;
    }

    public float GetMediana()
    {
        if (count % 2 == 0)
        {
            int sum = elements[count / 2 - 1].rating + elements[count / 2].rating;
            return (float)sum / 2;
        }

        return (float)elements[count / 2].rating;
    }

    public int[] GetModa()
    {
        int[] moda = new int[10];
        int modaCount = 0;

        int[] statistics = StatisticsForEachRating();

        int maxCount = 0;

        for (int i = 0; i < statistics.Length; i++)
        {
            if (statistics[i] > maxCount)
            {
                maxCount = statistics[i];
            }
        }

        for (int i = 0; i < statistics.Length; i++)
        {
            if (statistics[i] == maxCount)
            {
                moda[modaCount] = i + 1;
                modaCount++;
            }
        }

        if (modaCount == moda.Length)
        {
            return null;
        }

        int[] temp = new int[modaCount];
        for (int i = 0; i < modaCount; i++)
        {
            temp[i] = moda[i];
        }

        return temp;
    }
}