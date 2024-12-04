using System.Text.Json.Serialization;

namespace Dot.Net.WebApi.Domain;

public class Trade
{
    public int TradeId { get; set; }
    public string Account { get; set; } = string.Empty;
    public string AccountType { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? BuyQuantity { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? SellQuantity { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? BuyPrice { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? SellPrice { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? TradeDate { get; set; }
    public string TradeSecurity { get; set; } = string.Empty; 
    public string TradeStatus { get; set; } = string.Empty;
    public string Trader { get; set; } = string.Empty;
    public string Benchmark { get; set; } = string.Empty;
    public string Book { get; set; } = string.Empty;
    public string CreationName { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? CreationDate { get; set; }
    public string RevisionName { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? RevisionDate { get; set; }
    public string DealName { get; set; } = string.Empty;  
    public string DealType { get; set; } = string.Empty;
    public string SourceListId { get; set; } = string.Empty;
    public string Side { get; set; } = string.Empty;
}