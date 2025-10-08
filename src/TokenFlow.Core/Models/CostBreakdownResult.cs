namespace TokenFlow.Core.Models
{
    /// <summary>
    /// Represents a detailed breakdown of model cost calculations.
    /// </summary>
    public class CostBreakdownResult
    {
        public string ModelId { get; set; }
        public decimal InputRatePer1K { get; set; }
        public decimal OutputRatePer1K { get; set; }
        public decimal InputCost { get; set; }
        public decimal OutputCost { get; set; }
        public decimal TotalCost => InputCost + OutputCost;
    }
}
