﻿namespace API.Models;

public class Message
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
    public SentimentAnalysis Analysis { get; set; }

    public int ChatId { get; set; }
    public Chat Chat { get; set; }
}
