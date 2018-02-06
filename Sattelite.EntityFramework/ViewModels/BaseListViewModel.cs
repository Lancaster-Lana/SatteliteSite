
using Sattelite.Entities;
using Sattelite.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sattelite.Base
{
    public class BaseListViewModel
    {
        public string TitleSearchText { get; set; }

        public string SortDescription { get; set; }

        public PagingViewModel PagingData { get; set; } = new PagingViewModel();
    }
}