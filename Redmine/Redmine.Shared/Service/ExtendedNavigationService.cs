using GalaSoft.MvvmLight.Views;
using Redmine.Portable.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Redmine.Service
{
    public class ExtendedNavigationService : NavigationService, IExtendedNavigationService
    {
        public void NavigateTo(string pageKey, bool removeBackEntry, bool clearBackStack)
        {
            NavigateTo(pageKey);

            if (removeBackEntry)
                RemoveBackEntry();

            if (clearBackStack)
                ClearBackStack();
        }

        private void RemoveBackEntry()
        {
            var frame = (Frame)Window.Current.Content;
            var lastEntry = frame.BackStack.LastOrDefault();
            frame.BackStack.Remove(lastEntry);
        }

        private void ClearBackStack()
        {
            var frame = (Frame)Window.Current.Content;
            frame.BackStack.Clear();
        }
    }
}
