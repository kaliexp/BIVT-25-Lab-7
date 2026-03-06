namespace Lab7.Purple
{
    public class Task1
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumpNumber;
            public string Name => _name ?? string.Empty;
            public string Surname => _surname ?? string.Empty;
            public double[] Coefs => _coefs == null ? Array.Empty<double>() : (double[])_coefs.Clone();
            public int[,] Marks => _marks == null ? new int[0,0] : (int[,])_marks.Clone();
            public double TotalScore
            {
                get
                {
                    double total = 0.0;
                    for (int j = 0; j < 4; j++)
                    {
                        int sum = 0;
                        int min = _marks[j, 0];
                        int max = _marks[j, 0];
                        for (int i = 0; i < 7; i++)
                        {
                            int mark = _marks[j, i];
                            sum += mark;
                            if (mark < min) min = mark;
                            if (mark > max) max = mark;
                        }
                        sum -= min;
                        sum -= max;
                        total += sum * _coefs[j];
                    }
                    return total;
                }
            }
            
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4] {2.5, 2.5, 2.5, 2.5};
                _marks = new int[4, 7];
                _jumpNumber = 0;
            }
            public void SetCriterias(double[] coefs)
            {
                if (coefs == null) return;
                for (int i = 0; i < coefs.Length; i++)
                    _coefs[i] = coefs[i];
            }
            public void Jump(int[] marks)
            {
                if (marks == null) return;
                if (_jumpNumber >= 4) return;
                for (int i = 0; i < marks.Length; i++)
                    _marks[_jumpNumber, i] = marks[i];
                _jumpNumber++;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                Array.Sort(array, (a, b) => b.TotalScore.CompareTo(a.TotalScore));
            }
            public void Print()
            {
                Console.WriteLine($"{Surname} {Name} {TotalScore}");
            }
        }
    }
}