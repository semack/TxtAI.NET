using TxtAI.NET;
using TxtAI.NET.Models;

namespace EmbeddingsDemo;

public class EmbeddingsDemo
{
    private const string SERVICE_URL = "http://localhost:8000";

    public static async Task Main(string[] args)
    {
        try
        {
            var embeddings = new Embeddings(SERVICE_URL);

            var data = PrepareData();

            await ProcessSimilarityQueries(embeddings, data);

            await AddDocumentsToIndex(embeddings, data);

            await ProcessSearchQueries(embeddings, data);

            await TestDeleteUpsertCount(embeddings, data);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static List<string> PrepareData()
    {
        return new List<string>
        {
            "US tops 5 million confirmed virus cases",
            "Canada's last fully intact ice shelf has suddenly collapsed, forming a Manhattan-sized iceberg",
            "Beijing mobilises invasion craft along coast as Taiwan tensions escalate",
            "The National Park Service warns against sacrificing slower friends in a bear attack",
            "Maine man wins $1M from $25 lottery ticket",
            "Make huge profits without work, earn up to $100,000 a day"
        };
    }

    private static async Task ProcessSimilarityQueries(Embeddings embeddings, List<string> data)
    {
        var queries = new List<string>
        {
            "feel good story", "climate change", "public health story", "war", "wildlife", "asia", "lucky",
            "dishonest junk"
        };

        Console.WriteLine("Running similarity queries");
        Console.WriteLine($"{"Query",-20} {"Best Match"}");
        Console.WriteLine(new string('-', 50));

        foreach (var query in queries)
        {
            var result = await embeddings.SimilarityAsync(query, data);
            var id = int.Parse(result[0].Id);
            Console.WriteLine($"{query,-20} {data[id]}");
        }
    }

    private static async Task AddDocumentsToIndex(Embeddings embeddings, List<string> data)
    {
        var documents = data.Select((text, id) => new Document { Id = id.ToString(), Text = text }).ToList();

        await embeddings.AddAsync(documents);
        await embeddings.IndexAsync();
    }

    private static async Task ProcessSearchQueries(Embeddings embeddings, List<string> data)
    {
        var queries = new List<string>
        {
            "feel good story", "climate change", "public health story", "war", "wildlife", "asia", "lucky",
            "dishonest junk"
        };

        Console.WriteLine("\nBuilding an Embeddings index");
        Console.WriteLine($"{"Query",-20} {"Best Match"}");
        Console.WriteLine(new string('-', 50));

        foreach (var query in queries)
        {
            var result = await embeddings.SearchAsync(query, 1);
            var id = int.Parse(result[0].Id);
            Console.WriteLine($"{query,-20} {data[id]}");
        }
    }

    private static async Task TestDeleteUpsertCount(Embeddings embeddings, List<string> data)
    {
        data[0] = "See it: baby panda born";

        var updates = new List<Document>
        {
            new() { Id = "0", Text = data[0] }
        };

        await embeddings.DeleteAsync(new List<string> { "5" });
        await embeddings.AddAsync(updates);
        await embeddings.UpsertAsync();

        Console.WriteLine("\nTest delete/upsert/count");
        Console.WriteLine($"{"Query",-20} {"Best Match"}");
        Console.WriteLine(new string('-', 50));

        var query = "feel good story";
        var result = await embeddings.SearchAsync(query, 1);
        var id = int.Parse(result[0].Id);
        Console.WriteLine($"{query,-20} {data[id]}");

        var count = await embeddings.CountAsync();
        Console.WriteLine($"\nTotal Count: {count}");
    }
}