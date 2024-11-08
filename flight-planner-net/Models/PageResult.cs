namespace flight_planner_net.Models
{
    public class PageResult<T>
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }
    }
}