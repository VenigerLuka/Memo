﻿using MemoProject.Contracts;
using MemoProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Contracts
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        Task<bool> AddRange(IEnumerable<string> tagList, Memo memo);
        Task DeleteByMemoAsync(long memoId);

    }
}
