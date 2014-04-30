﻿using System;
using ARK.ViewModel.Base;
using ARK.ViewModel.Base.Interfaces.Filter;

namespace ARK.ViewModel.Protokolsystem.Filters
{
    internal class EndTripFiltersViewModel : ViewModelBase, IFilterViewModel
    {
        public IFilterContainerViewModel FilterContainer { get; set; }

        public event EventHandler FilterChanged;
    }
}