using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PagingInformationModel<T>
    {
        public PagingInformationModel(List<T> items, long totalItemsCount, int pageSize, int currentPage)
        {
            this.Items = items;
            this.TotalItemsCount = totalItemsCount;
            this.PageSize = pageSize;
            this.CurrentPage = currentPage;
            if (this.PageSize < 1)
            {
                this.PageSize = 20;
            }
            if (this.TotalItemsCount < 0)
            {
                this.TotalItemsCount = 0;
            }
            this.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.TotalItemsCount) / this.PageSize));
        }

        public List<T> Items { get; private set; }

        public int CurrentPage { get; private set; }

        public int PageSize { get; private set; }

        public long TotalItemsCount { get; private set; }

        public int PageCount { get; private set; }
    }
}
