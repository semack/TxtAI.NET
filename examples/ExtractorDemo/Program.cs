using TxtAI.NET;
using TxtAI.NET.Models;

namespace ExtractorDemo;

public class ExtractorDemo
{
    public static async Task Main(string[] args)
    {
        try
        {
            var extractor = new Extractor("http://localhost:8000");

            var data = new List<string>
            {
                "Giants hit 3 HRs to down Dodgers",
                "Giants 5 Dodgers 4 final",
                "Dodgers drop Game 2 against the Giants, 5-4",
                "Blue Jays beat Red Sox final score 2-1",
                "Red Sox lost to the Blue Jays, 2-1",
                "Blue Jays at Red Sox is over. Score: 2-1",
                "Phillies win over the Braves, 5-0",
                "Phillies 5 Braves 0 final",
                "Final: Braves lose to the Phillies in the series opener, 5-0",
                "Lightning goaltender pulled, lose to Flyers 4-1",
                "Flyers 4 Lightning 1 final",
                "Flyers win 4-1"
            };

            // Run series of questions
            var questions = new List<string> { "What team won the game?", "What was score?" };
            var queries = new List<string>
                { "Red Sox - Blue Jays", "Phillies - Braves", "Dodgers - Giants", "Flyers - Lightning" };

            foreach (var query in queries)
            {
                Console.WriteLine($"----{query}----");

                var seriesQueue = new List<Question>();
                foreach (var seriesQuestion in questions)
                {
                    seriesQueue.Add(new Question
                    {
                        Name = seriesQuestion,
                        Query = query,
                        QuestionText = seriesQuestion,
                        Snippet = false
                    });
                }

                foreach (var answer in await extractor.ExtractAsync(seriesQueue, data))
                {
                    Console.WriteLine(answer);
                }

                Console.WriteLine();
            }

            // Ad-hoc questions
            var adhocQuestion = "What hockey team won?";

            Console.WriteLine($"----{adhocQuestion}----");
            var adhocQueue = new List<Question>
            {
                new()
                {
                    Name = adhocQuestion,
                    Query = adhocQuestion,
                    QuestionText = adhocQuestion,
                    Snippet = false
                }
            };

            foreach (var answer in await extractor.ExtractAsync(adhocQueue, data))
            {
                Console.WriteLine(answer);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}