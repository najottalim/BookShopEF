namespace BookStore.Domain.Commons;

public class PaginationParameters
{
    private readonly int _index;
    private readonly int _size;
    
    public int Skip => (_index - 1) * _size;
    public int Take => _size;
    
    public PaginationParameters(int pageIndex, int pageSize)
    {
        _index = pageIndex;
        _size = pageSize;
    }
}