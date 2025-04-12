namespace CoreBaking.API.Models;

public class PaginationResponse<TEntity>(int index, int pageSize, long count, IEnumerable<TEntity> items)
{
    public int Index { get; set; } = index;
    public int PageSize { get; set; } = pageSize;
    public long Count { get; set; } = count;
    public IEnumerable<TEntity> Items { get; set; } = items;
}
