using API.Models;

namespace API.DTOs.Message;

public class MessageDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
    public SentimentAnalysis Analysis { get; set; }
}
