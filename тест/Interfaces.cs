using Classes;
namespace Interfaces
{
    public interface ICounter
    {
        int DailyFlow { get; }
        decimal Revenue { get; }
        decimal DailyRevenue { get; }
    }
}