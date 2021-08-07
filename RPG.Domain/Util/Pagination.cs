namespace RPG.Domain.Util
{
    public class Pagination
    {
        const int maxPageSize = 5;
        public int totalCount { get; set; }
        public int pageNumber { get; set; }
        private int _pageSize = 3;
        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}