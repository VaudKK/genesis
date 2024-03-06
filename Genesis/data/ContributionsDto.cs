namespace Genesis.data
{
    public class ContributionsDto
    {
        public string Name { get; set; } = default!;
        public double Amount { get; set; } = 0f;
        public string Phone { get; set; } = default!;
        public string PaymentMode { get; set; } = default!;
        public long Date { get; set; } = 0L;
    }
}
