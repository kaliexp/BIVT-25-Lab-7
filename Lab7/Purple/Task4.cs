using System;

namespace Lab7.Purple
{
    public class Task4
    {
        public struct Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool _isRun;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
                _isRun = false;
            }

            public void Run(double time)
            {
                if (_isRun) return;
                _time = time;
                _isRun = true;
            }

            public void Print()
            {
                Console.WriteLine($"Name:{_name}");
                Console.WriteLine($"Surname:{_surname}");
                Console.WriteLine($"Time:{_time}");
            }
        }
        public struct Group
        {
            private string _name;
            private Sportsman[] _sportsmen;
            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group group)
            {
                _name = group._name;

                if (group._sportsmen == null)
                {
                    _sportsmen = new Sportsman[0];
                }
                else
                {
                    _sportsmen = new Sportsman[group._sportsmen.Length];
                    Array.Copy(group._sportsmen, _sportsmen, group._sportsmen.Length);
                }
            }
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null) _sportsmen = new Sportsman[0];

                int oldLen = _sportsmen.Length;
                Sportsman[] updated = new Sportsman[oldLen + 1];
                Array.Copy(_sportsmen, updated, oldLen);
                updated[oldLen] = sportsman;
                _sportsmen = updated;
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || sportsmen.Length == 0) return;
                if (_sportsmen == null) _sportsmen = new Sportsman[0];
                int a = _sportsmen.Length;
                int b = sportsmen.Length;
                Sportsman[] updated = new Sportsman[a + b];
                Array.Copy(_sportsmen, 0, updated, 0, a);
                Array.Copy(sportsmen, 0, updated, a, b);
                _sportsmen = updated;
            }
            public void Add(Group group)
            {
                if (group._sportsmen == null || group._sportsmen.Length == 0) return;
                Add(group._sportsmen);
            }
            public void Sort()
            {
                if (_sportsmen == null || _sportsmen.Length < 2) return;

                for (int pass = 0; pass < _sportsmen.Length - 1; pass++)
                {
                    for (int i = 0; i < _sportsmen.Length - 1 - pass; i++)
                    {
                        if (_sportsmen[i].Time > _sportsmen[i + 1].Time)
                        {
                            (_sportsmen[i], _sportsmen[i + 1]) = (_sportsmen[i + 1], _sportsmen[i]);
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2)
            {
                Group merged = new Group("Финалисты");
                Sportsman[] a = group1._sportsmen ?? new Sportsman[0];
                Sportsman[] b = group2._sportsmen ?? new Sportsman[0];
                int i = 0, j = 0;
                while (i < a.Length && j < b.Length)
                {
                    if (a[i].Time <= b[j].Time)
                    {
                        merged.Add(a[i]);
                        i++;
                    }
                    else
                    {
                        merged.Add(b[j]);
                        j++;
                    }
                }
                for (; i < a.Length; i++) merged.Add(a[i]);
                for (; j < b.Length; j++) merged.Add(b[j]);
                return merged;
            }
            public void Print()
            {
                Console.WriteLine($"Группа: {_name}");
                if (_sportsmen == null) return;
                for (int i = 0; i < _sportsmen.Length; i++)
                    _sportsmen[i].Print();
            }
        }
    }
}