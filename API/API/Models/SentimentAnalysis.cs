using Azure.AI.TextAnalytics;

namespace API.Models;

public class SentimentAnalysis
{
    public TextSentiment Sentiment { get; set; }
    public double Score { get; set; }
}
