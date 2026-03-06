namespace Lab7.Purple
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;

            public int[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[] copy = new int[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }
            public int Result
            {
                get
                {
                    if (_marks == null || _marks.Length < 5) return 0;
                    int sum = 0;
                    int min = _marks[0];
                    int max = _marks[0];
                    for (int i = 0; i < 5; i++)
                    {
                        int m = _marks[i];
                        sum += m;
                        if (m < min) min = m;
                        if (m > max) max = m;
                    }
                    int stylePoints = sum - min - max;
                    int distancePoints = 60 + (_distance - 120) * 2;
                    if (distancePoints < 0) distancePoints = 0;
                    return stylePoints + distancePoints;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
            }

            public void Jump(int distance, int[] marks)
            {
                if (marks == null || marks.Length != 5) return;
                _distance = distance;
                _marks = new int[5];
                Array.Copy(marks, _marks, 5);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Result < array[j + 1].Result)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_surname}: {Result}");
            }
        }
    }
}