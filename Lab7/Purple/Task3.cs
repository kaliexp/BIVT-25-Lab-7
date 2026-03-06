namespace Lab7.Purple
{
    public class Task3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return new double[7];
                    double[] res = new double[_marks.Length];
                    Array.Copy(_marks, res, _marks.Length);
                    return res;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) return new int[7];
                    int[] res = new int[_places.Length];
                    Array.Copy(_places, res, _places.Length);
                    return res;
                }
            }
            public int TopPlace
            {
                get
                {
                    int best = int.MaxValue;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        int p = _places[i];
                        if (p < best) best = p;
                    }
                    return best;
                }
            }
            public double TotalMark
            {
                get
                {
                    double sum = 0;
                    double[] m = Marks;
                    for (int i = 0; i < 7; i++)
                        sum += m[i];
                    return sum;
                }
            }
            public int Score
            {
                get
                {
                    int s = 0;
                    for (int i = 0; i < 7; i++)
                        s += _places[i];
                    return s;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[0];
                _places = new int[7];
            }
            public void Evaluate(double result)
            {
                if (_marks.Length >= 7) return;

                int newLen = _marks.Length + 1;
                Array.Resize(ref _marks, newLen);
                _marks[newLen - 1] = result;
            }
            public static void SetPlaces(Participant[] participants)
            {
                double[,] table = new double[participants.Length, 7];

                for (int r = 0; r < participants.Length; r++)
                {
                    for (int c = 0; c < participants[r]._marks.Length; c++)
                        table[r, c] = participants[r]._marks[c];
                }

                for (int judge = 0; judge < 7; judge++)
                {
                    int n = participants.Length;
                    int[] pl = new int[n];

                    double[] original = new double[n];
                    double[] sorted = new double[n];

                    for (int i = 0; i < n; i++)
                    {
                        double val = table[i, judge];
                        original[i] = val;
                        sorted[i] = val;
                    }

                    for (int pass = 0; pass < n - 1; pass++)
                    {
                        for (int i = 0; i < n - 1 - pass; i++)
                        {
                            if (sorted[i] < sorted[i + 1])
                            {
                                double t = sorted[i];
                                sorted[i] = sorted[i + 1];
                                sorted[i + 1] = t;
                            }
                        }
                    }

                    for (int i = 0; i < n; i++)
                    {
                        double cur = original[i];
                        for (int k = 0; k < n; k++)
                        {
                            if (cur == sorted[k])
                                pl[i] = k + 1;
                        }
                    }

                    for (int i = 0; i < n; i++)
                        table[i, judge] = pl[i];
                }

                for (int r = 0; r < participants.Length; r++)
                {
                    for (int c = 0; c < 7; c++)
                        participants[r]._places[c] = (int)table[r, c];
                }
            }

            public static void Sort(Participant[] array)
            {
                for (int pass = 0; pass < array.Length - 1; pass++)
                {
                    for (int i = 0; i < array.Length - 1 - pass; i++)
                    {
                        if (array[i].Score > array[i + 1].Score)
                        {
                            (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        }
                    }
                }

                for (int pass = 0; pass < array.Length - 1; pass++)
                {
                    for (int i = 0; i < array.Length - 1 - pass; i++)
                    {
                        if (array[i].Score == array[i + 1].Score)
                        {
                            int leftBetter = 0;
                            int rightBetter = 0;

                            int[] p1 = array[i].Places;
                            int[] p2 = array[i + 1].Places;

                            for (int k = 0; k < 7; k++)
                            {
                                if (p1[k] > p2[k]) leftBetter++;
                                else if (p1[k] < p2[k]) rightBetter++;
                            }

                            if (leftBetter < rightBetter)
                            {
                                (array[i], array[i + 1]) = (array[i + 1], array[i]);
                            }
                            else if (leftBetter == rightBetter)
                            {
                                if (array[i].TotalMark < array[i + 1].TotalMark)
                                {
                                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                                }
                            }
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_surname} {_name} -> {Score}");
            }
        }
    }
}