using API.Models;
using Azure;
using Azure.AI.TextAnalytics;

namespace API.Services;

public class SentimentAnalysisService
{

    private readonly Uri _endpoint = new("https://chatapptextanalytics.openai.azure.com/");
    private readonly TextAnalyticsClient _textClient;

    public SentimentAnalysisService(IConfiguration config)
    {
        AzureKeyCredential credentials = new(config["Azure:TextAnalysis:Key"]);
        _textClient = new(_endpoint, credentials);
    }

    public async Task<SentimentAnalysis> DetectSentimentAsync(string text)
    {
        Response<DocumentSentiment> sentimentResponse = await _textClient.AnalyzeSentimentAsync(text);
        DocumentSentiment sentiment = sentimentResponse.Value;

        double maxScore = Math.Max(
            sentiment.ConfidenceScores.Neutral,
            Math.Max(
                sentiment.ConfidenceScores.Positive,
                sentiment.ConfidenceScores.Negative
            )
        );

        return new SentimentAnalysis
        {
            Score = maxScore,
            Sentiment = sentiment.Sentiment
        };
    }

}
