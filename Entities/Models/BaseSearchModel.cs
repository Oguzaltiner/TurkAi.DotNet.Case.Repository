using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public abstract class BaseSearchModel
    {
        public int PageNumber
        {
            get
            {
                return _PageNumber;
            }
            set
            {
                _PageNumber = Math.Max(1, value);
            }
        }
        public int PageSize
        {
            get
            {

                return _PageSize;

            }
            set
            {
                _PageNumber = Math.Max(1, value);
                _PageNumber = Math.Min(_PageNumber, 100);
            }
        }
        private int _PageNumber = 1;
        private int _PageSize = 20;
        public virtual bool ReadAll { get; set; }
        public int PagingSkipSize
        {
            get
            {
                //int skipSize = CalcuateSkipSize(PageNumber, PageSize);
                int skipSize = (PageNumber - 1) * PageSize;
                return skipSize;
            }
        }
        public int CalcuateSkipSize(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            if (pageSize > 20 || pageSize < 0)
            {
                pageSize = 20;
            }
            if (pageSize > 50)
            {
                pageSize = 50;
            }
            int skipSize = (pageNumber - 1) * pageSize;
            return skipSize;
        }
    }
}
