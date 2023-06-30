using TxtAI.NET;

namespace PipelinesDemo;

public class PipelinesDemo
{
    private const string SERVICE_URL = "http://localhost:8000";

    public static async Task Main(string[] args)
    {
        try
        {
            var sentences = "This is a test. And another test.";
            var pdfFilePath = "/tmp/txtai/article.pdf";
            var audioFilePath = "/tmp/txtai/Make_huge_profits.wav";

            var segmentedText = await RunSegmentationDemo(sentences);
            var extractedText = await RunTextractionDemo(pdfFilePath);
            var summaryText = await RunSummaryDemo(extractedText);
            var translatedText = await RunTranslationDemo(summaryText);
            await RunWorkflowDemo(pdfFilePath);
            await RunTranscriptionDemo(audioFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static async Task<string> RunSegmentationDemo(string sentences)
    {
        var segment = new Segmentation(SERVICE_URL);
        Console.WriteLine("---- Segmented Text ----");
        var result = await segment.SegmentAsync(sentences);
        Console.WriteLine(result);
        return result;
    }

    private static async Task<string> RunTextractionDemo(string pdfFilePath)
    {
        var textractor = new Textractor(SERVICE_URL);
        Console.WriteLine("\n---- Extracted Text ----");
        var result = await textractor.TextractAsync(pdfFilePath);
        Console.WriteLine(result);
        return result;
    }

    private static async Task<string> RunSummaryDemo(string extractedText)
    {
        var summary = new Summary(SERVICE_URL);
        Console.WriteLine("\n---- Summary Text ----");
        var result = await summary.SummarizeAsync(extractedText, -1, -1);
        Console.WriteLine(result);
        return result;
    }

    private static async Task<string> RunTranslationDemo(string summaryText)
    {
        var translate = new Translation(SERVICE_URL);
        Console.WriteLine("\n---- Summary Text in Spanish ----");
        var result = await translate.TranslateAsync(summaryText, "es", null);
        Console.WriteLine(result);
        return result;
    }

    private static async Task RunWorkflowDemo(string pdfFilePath)
    {
        var workflow = new Workflow(SERVICE_URL);
        var output = await workflow.WorkflowActionAsync("sumspanish", new List<string> { $"file://{pdfFilePath}" });
        Console.WriteLine("\n---- Workflow [Extract Text->Summarize->Translate] ----");
        Console.WriteLine(output);
    }

    private static async Task RunTranscriptionDemo(string audioFilePath)
    {
        var transcribe = new Transcription(SERVICE_URL);
        Console.WriteLine("\n---- Transcribed Text ----");
        var result = await transcribe.TranscribeAsync(audioFilePath);
        Console.WriteLine(result);
    }
}