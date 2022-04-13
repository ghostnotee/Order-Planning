using Microsoft.EntityFrameworkCore;

namespace OrderPlanning.WebApi.Models;

public class Order
{
    public int OrderId { get; set; }
    public string? ID { get; set; }
    public DateTime Date { get; set; }
    public int MOQ { get; set; }
    public int Amount { get; set; }
}