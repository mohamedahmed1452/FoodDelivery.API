namespace FoodDelivery.API.Helpers
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public Pagination(IReadOnlyList<T> data, int pageSize, int pageIndex, int count)
        {
            Data = data;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;

        }
    }

}
