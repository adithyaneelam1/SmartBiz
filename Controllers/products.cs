internal class products
{
    internal string category;

    public string ImagePath { get; internal set; }
    public bool AvailableForDelivery { get; internal set; }
    public bool InStock { get; internal set; }
    public bool IsFeatured { get; internal set; }
    public bool IsNew { get; internal set; }
    public string Address { get; internal set; }
    public string Gender { get; internal set; }
    public long? Price { get; internal set; }
    public string PName { get; internal set; }
    public int? Pid { get; internal set; }
}