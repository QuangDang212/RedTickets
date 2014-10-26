using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Interface
{
    public interface IExtendedNavigationService : INavigationService
    {
        void NavigateTo(string pageKey, bool removeBackEntry, bool clearBackStack);
    }
}
