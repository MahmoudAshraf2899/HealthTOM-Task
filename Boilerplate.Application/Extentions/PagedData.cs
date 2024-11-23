using Microsoft.EntityFrameworkCore;
namespace Boilerplate.Application.Extentions;

public class PagedData<T>
{
      private int? _take;

        public int? PageSize
        {
            get
            {
                return this._take;
            }
            set
            {
                if (value > 0)
                    this._take = value;
            }
        }
        public int? CurrentPage { get; set; }
        public int? TotalCount { get; private set; }

        public int? Skip { get; private set; }
        public int? TotalPages { get; private set; }
        public int? StartPage { get; private set; }
        public int? EndPage { get; private set; }
        public IList<T> items { get; private set; }

        public async Task Paginate(IQueryable<T> query, int? pageSize, int? currentPage)
        {
            var totalCount = await query.CountAsync();
            if (currentPage.HasValue && pageSize.HasValue)
            {
                PageSize = pageSize;
                CurrentPage = currentPage.Value > 0 ? currentPage.Value : 1;
            }
            TotalCount = totalCount;

            if (TotalCount.HasValue && TotalCount.Value > 0)
            {
                if (PageSize.HasValue && PageSize.Value > 0)
                    TotalPages = (int)Math.Ceiling((decimal)TotalCount.Value / (decimal)PageSize.Value);

                if (CurrentPage.HasValue && CurrentPage.Value > 0)
                {
                    if (TotalPages.HasValue && CurrentPage.Value > TotalPages.Value)
                        CurrentPage = TotalPages.Value;
                    Skip = (CurrentPage.Value - 1) * (PageSize.HasValue ? PageSize.Value : 0);
                }

                if (CurrentPage.HasValue && CurrentPage.Value > 0)
                {
                    StartPage = CurrentPage.Value - 2;
                    EndPage = CurrentPage + 2;

                    if (StartPage <= 0)
                    {
                        EndPage = EndPage - (StartPage - 1);
                        StartPage = 1;
                    }
                    if (EndPage > TotalPages)
                    {
                        EndPage = TotalPages;
                        if (EndPage > 5)
                            StartPage = EndPage - 4;
                        else
                            StartPage = 1;
                    }
                }
            }
             items = await query.Skip(Skip.Value).Take(_take.Value).ToListAsync();
        }
}