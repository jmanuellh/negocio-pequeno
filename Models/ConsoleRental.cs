public class ConsoleRental
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public System.DateTime StartDate { get; set; }
    public System.DateTime EndDate { get; set; }

    public int ConsoleId { get; set; }
    public Console Console {get;set;}
}
