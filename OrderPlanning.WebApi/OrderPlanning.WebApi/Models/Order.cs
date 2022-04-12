namespace OrderPlanning.WebApi.Models;

public class Order
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public int MOQ { get; set; }
    public int Amount { get; set; }
}