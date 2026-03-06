using System;
using System.Linq;

namespace Lab7.Purple
{
    public class Task5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;
            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;
            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }
            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || questionNumber < 1 || questionNumber > 3)
                    return 0;
                
                string myAnswer = questionNumber switch
                {
                    1 => Animal,
                    2 => CharacterTrait,
                    3 => Concept,
                    _ => null
                };

                if (string.IsNullOrEmpty(myAnswer)) return 0;

                int count = 0;
                foreach (var response in responses)
                {
                    string theirAnswer = questionNumber switch
                    {
                        1 => response.Animal,
                        2 => response.CharacterTrait,
                        3 => response.Concept,
                        _ => null
                    };

                    if (myAnswer == theirAnswer)
                    {
                        count++;
                    }
                }

                return count;
            }
            public void Print()
            {
                Console.WriteLine($"{Animal} | {CharacterTrait} | {Concept}");
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;
            public string Name => _name;
            public Response[] Responses => _responses;

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }
            public void Add(string[] answers)
            {
                if (answers == null || answers.Length != 3) return;

                if (_responses == null) _responses = new Response[0];
                
                Response newResponse = new Response(answers[0], answers[1], answers[2]);
                _responses = _responses.Append(newResponse).ToArray();
            }

            public string[] GetTopResponses(int question)
            {
                if (_responses == null || _responses.Length == 0 || question < 1 || question > 3) 
                    return Array.Empty<string>();

                var allAnswers = _responses.Select(r => question switch
                {
                    1 => r.Animal,
                    2 => r.CharacterTrait,
                    3 => r.Concept,
                    _ => null
                }).Where(ans => !string.IsNullOrEmpty(ans));

                var top5 = allAnswers
                    .GroupBy(ans => ans)
                    .OrderByDescending(group => group.Count())
                    .Take(5)
                    .Select(group => group.Key)
                    .ToArray();

                return top5;
            }

            public void Print()
            {
                Console.WriteLine($"Опрос: {Name}");
            }
        }
    }
}