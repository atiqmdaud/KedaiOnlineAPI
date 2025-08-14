using KedaiOnline.Application.KedaiOnline.Dtos;

namespace KedaiOnline.Application.Common;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> theItems, int totalCount, int pageNumber, int pageSize)
    {
        TheItems = theItems;
        TotalTheItemsCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        TheItemsFrom = (pageNumber - 1) * pageSize + 1;
        //TheItemsTo = TheItemsFrom + pageSize - 1;
        TheItemsTo = Math.Min(TheItemsFrom + pageSize - 1, totalCount);

    }
    public IEnumerable<T> TheItems { get; set; }
    public int TotalPages { get; set; }
    public int TotalTheItemsCount { get; set; }
    public int TheItemsFrom { get; set; }
    public int TheItemsTo { get; set; }
}
