using System;
namespace MoviesMobileApp.Views
{
    public class SelectionTapItemEventArgs : EventArgs
    {
        readonly object _selecteditem;
        readonly object _selectedSubitem;

        public SelectionTapItemEventArgs(object selected, object subitem)
        {
            _selecteditem = selected;
            _selectedSubitem = subitem;
        }

        public object SelectedCategory => _selecteditem;

        public object SelectedItem => _selectedSubitem;
    }
}
