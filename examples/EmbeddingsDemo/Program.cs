using TxtAI.NET;
using TxtAI.NET.Models;

namespace EmbeddingsDemo;

public class EmbeddingsDemo 
{
    public static async Task Main(string[] args) 
    {
        try 
        {
            var embeddings = new Embeddings("http://localhost:8000");

            List<string> data = new List<string> 
            {
                "US tops 5 million confirmed virus cases", 
                "Canada's last fully intact ice shelf has suddenly collapsed, forming a Manhattan-sized iceberg",
                "Beijing mobilises invasion craft along coast as Taiwan tensions escalate",
                "The National Park Service warns against sacrificing slower friends in a bear attack",
                "Maine man wins $1M from $25 lottery ticket",
                "Make huge profits without work, earn up to $100,000 a day"
            };

            var documents = new List<Document>();
            for (var x = 0; x < data.Count; x++) 
            {
                var d = new Document
                {
                    Id = x.ToString(), 
                    Text = data[x]
                };
                documents.Add(d);
            }

            Console.WriteLine("Running similarity queries");
            Console.WriteLine($"{"Query",-20} {"Best Match"}");
            Console.WriteLine(new string('-', 50));

            var queries = new List<string> { "feel good story", "climate change", "public health story", "war", "wildlife", "asia", "lucky", "dishonest junk" };

            foreach (var similarityQuery in queries)
            {
                var similarityResults = await embeddings.SimilarityAsync(similarityQuery, data);
                var similarityUid = Int32.Parse(similarityResults[0].Id);
                Console.WriteLine($"{similarityQuery,-20} {data[similarityUid]}");
            }

            await embeddings.AddAsync(documents);
            await embeddings.IndexAsync();

            Console.WriteLine("\nBuilding an Embeddings index");
            Console.WriteLine($"{"Query",-20} {"Best Match"}");
            Console.WriteLine(new string('-', 50));

            foreach (var searchQuery in queries) 
            {
                var searchResults = await embeddings.SearchAsync(searchQuery, 1);
                var searchUid = Int32.Parse(searchResults[0].Id);
                Console.WriteLine($"{searchQuery,-20} {data[searchUid]}");
            }

            data[0] = "See it: baby panda born";
            var updates = new List<Document> { new()
            {
                Id = "0",
                Text = data[0]
            } };

            await embeddings.DeleteAsync(new List<string> { "5" });
            await embeddings.AddAsync(updates);
            await embeddings.UpsertAsync();

            Console.WriteLine("\nTest delete/upsert/count");
            Console.WriteLine($"{"Query",-20} {"Best Match"}");
            Console.WriteLine(new string('-', 50));

            string query = "feel good story";
            var results = await embeddings.SearchAsync(query, 1);
            var uid = Int32.Parse(results[0].Id);
            Console.WriteLine($"{query,-20} {data[uid]}");

            var count = await embeddings.CountAsync();
            Console.WriteLine($"\nTotal Count: {count}");
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex);
        }
    }    
}