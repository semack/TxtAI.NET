using TxtAI.NET;

namespace PipelinesDemo;

public class PipelinesDemo
{
    public static async Task Main(string[] args)
    {
        try
        {
            var service = "http://localhost:8000";

            var segment = new Segmentation(service);

            var sentences = "This is a test. And another test.";

            Console.WriteLine("---- Segmented Text ----");
            Console.WriteLine(await segment.SegmentAsync(sentences));

            var textractor = new Textractor(service);
            var text = await textractor.TextractAsync("/tmp/txtai/article.pdf");

            Console.WriteLine("\n---- Extracted Text ----");
            Console.WriteLine(text);

            var summary = new Summary(service);
            var summaryText = await summary.SummarizeAsync(text, -1, -1);

            Console.WriteLine("\n---- Summary Text ----");
            Console.WriteLine(summaryText);

            var translate = new Translation(service);
            var translation = await translate.TranslateAsync(summaryText, "es", null);

            Console.WriteLine("\n---- Summary Text in Spanish ----");
            Console.WriteLine(translation);

            var workflow = new Workflow(service);
            var output =
                await workflow.WorkflowActionAsync("sumspanish", new List<string> { "file:///tmp/txtai/article.pdf" });

            Console.WriteLine("\n---- Workflow [Extract Text->Summarize->Translate] ----");
            Console.WriteLine(output);

            var transcribe = new Transcription(service);
            var transcription = await transcribe.TranscribeAsync("/tmp/txtai/Make_huge_profits.wav");

            Console.WriteLine("\n---- Transcribed Text ----");
            Console.WriteLine(transcription);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}