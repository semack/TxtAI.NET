using TxtAI.NET;
using TxtAI.NET.Models;

namespace ExtractorDemo;

public class ExtractorDemo
{
    private const string SERVICE_URL = "http://localhost:8000";

    public static async Task Main(string[] args)
    {
        try
        {
            var extractorService = new Extractor(SERVICE_URL);

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

            var questions = new List<string> { "What team won the game?", "What was score?" };
            var queries = new List<string>
                { "Red Sox - Blue Jays", "Phillies - Braves", "Dodgers - Giants", "Flyers - Lightning" };

            await ProcessQueries(extractorService, questions, queries, data);

            var adhocQuestion = "What hockey team won?";
            await ProcessAdhocQuestion(extractorService, adhocQuestion, data);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static async Task ProcessQueries(Extractor extractorService, List<string> questions, List<string> queries,
        List<string> data)
    {
        foreach (var query in queries)
        {
            Console.WriteLine($"----{query}----");

            var seriesQueue = BuildQuestionQueue(questions, query);
            await PrintAnswers(extractorService, seriesQueue, data);

            Console.WriteLine();
        }
    }

    private static async Task ProcessAdhocQuestion(Extractor extractorService, string adhocQuestion, List<string> data)
    {
        Console.WriteLine($"----{adhocQuestion}----");
        var adhocQueue = BuildQuestionQueue(new List<string> { adhocQuestion }, adhocQuestion);
        await PrintAnswers(extractorService, adhocQueue, data);
    }

    private static List<Question> BuildQuestionQueue(List<string> questions, string query)
    {
        var queue = new List<Question>();

        foreach (var question in questions)
        {
            queue.Add(new Question
            {
                Name = question,
                Query = query,
                QuestionText = question,
                Snippet = false
            });
        }

        return queue;
    }

    private static async Task PrintAnswers(Extractor extractorService, List<Question> questionQueue, List<string> data)
    {
        foreach (var answer in await extractorService.ExtractAsync(questionQueue, data))
        {
            Console.WriteLine(answer);
        }
    }
}