using System;
using System.Collections;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace MoviesMobileApp.Views
{
    public delegate void SelectedItemEventHandLer(object sender, SelectionTapItemEventArgs args);

    public class HorizontalListControl : ScrollView
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(HorizontalListControl),
            null,
            BindingMode.OneWay,
            propertyChanged: ItemsChanged);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(HorizontalListControl),
            default(DataTemplate));

        public event SelectedItemEventHandLer SelectedItemChanged;

        public HorizontalListControl()
        {
            Orientation = ScrollOrientation.Horizontal;
            Content = HolderView = new StackLayout { Spacing = 0, Orientation = StackOrientation.Horizontal };
        }

        public StackLayout HolderView { get; }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public virtual void OnItemSelection(SelectionTapItemEventArgs e) => SelectedItemChanged?.Invoke(this, e);

        void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var invalidate = false;
            if (e.OldItems != null)
            {
                HolderView.Children.RemoveAt(e.OldStartingIndex);
                invalidate = true;
            }
            if (e.NewItems != null)
            {
                for (var i = 0; i < e.NewItems.Count; ++i)
                {
                    var item = e.NewItems[i];
                    var view = CreateChildViewFor(item);
                    HolderView.Children.Insert(i + e.NewStartingIndex, view);
                }
                invalidate = true;
            }
            if (invalidate)
            {
                UpdateChildrenLayout();
                InvalidateLayout();
            }
        }

        View CreateChildViewFor(object item)
        {
            ItemTemplate.SetValue(BindableObject.BindingContextProperty, item);
            return (View)ItemTemplate.CreateContent();
        }

        static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            IEnumerable oldValueAsEnumerable;
            IEnumerable newValueAsEnumerable;
            try
            {
                oldValueAsEnumerable = oldValue as IEnumerable;
                newValueAsEnumerable = newValue as IEnumerable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var control = (HorizontalListControl)bindable;
            var oldObservableCollection = oldValue as INotifyCollectionChanged;
            if (oldObservableCollection != null)
            {
                oldObservableCollection.CollectionChanged -= control.OnItemsSourceCollectionChanged;
            }
            var newObservableCollection = newValue as INotifyCollectionChanged;

            if (newObservableCollection != null)
            {
                newObservableCollection.CollectionChanged += control.OnItemsSourceCollectionChanged;
            }
            control.HolderView.Children.Clear();

            if (newValueAsEnumerable != null)
            {
                foreach (var item in newValueAsEnumerable)
                {
                    var view = control.CreateChildViewFor(item);
                    control.HolderView.Children.Add(view);
                    var Tap = new TapGestureRecognizer();
                    view.GestureRecognizers.Add(Tap);
                    Tap.Tapped += (sender, e) =>
                    {
                        if (control.SelectedItemChanged != null)
                        {
                            control.OnItemSelection(new SelectionTapItemEventArgs(control.BindingContext, view.BindingContext));
                        }
                    };

                }
            }
            control.UpdateChildrenLayout();
            control.InvalidateLayout();
        }
    }
}
