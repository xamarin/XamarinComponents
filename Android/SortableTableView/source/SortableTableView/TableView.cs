using CodeCrafters.TableViews.Listeners;
using Java.Interop;
using System;

namespace CodeCrafters.TableViews
{
    partial class TableView
    {
        private WeakReference dataClickListener;

        public TableDataAdapter DataAdapter
        {
            get { return TableDataAdapter; }
            set { SetDataAdapter(value); }
        }

        public event EventHandler<TableDataClickEventArgs> DataClick
        {
            add
            {
                EventHelper.AddEventHandler<ITableDataClickListener, ITableDataClickListenerImplementor>(
                        ref dataClickListener,
                        () => new ITableDataClickListenerImplementor(this),
                        AddDataClickListener,
                        h => h.Handler += value);
            }
            remove
            {
                EventHelper.RemoveEventHandler<ITableDataClickListener, ITableDataClickListenerImplementor>(
                        ref dataClickListener,
                        ITableDataClickListenerImplementor.__IsEmpty,
                        RemoveDataClickListener,
                        h => h.Handler -= value);
            }
        }
    }
}
