using System.Collections;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

using SortableTableViewSample;
using SortableTableViewSample.Data;

namespace CodeCrafters.TableViews
{
    public class CarTableDataAdapter : TableDataAdapter
    {
        private const int TextSize = 14;

        public CarTableDataAdapter(Context context, IList data)
            : base(context, data)
        {
        }

        public override View GetCellView(int rowIndex, int columnIndex, ViewGroup parentView)
        {
            var car = (Car)GetRowData(rowIndex);
            View renderedView = null;

            switch (columnIndex)
            {
                case 0:
                    renderedView = RenderProducerLogo(car, parentView);
                    break;
                case 1:
                    renderedView = RenderName(car);
                    break;
                case 2:
                    renderedView = RenderPower(car, parentView);
                    break;
                case 3:
                    renderedView = RenderPrice(car);
                    break;
            }

            return renderedView;
        }

        private View RenderPrice(Car car)
        {
            var textView = new TextView(Context);
            textView.Text = (car.Price / 1000).ToString("R #,##0K");
            textView.SetPadding(20, 10, 20, 10);
            textView.TextSize = TextSize;

            if (car.Price < 500000)
            {
                textView.SetTextColor(Color.DarkGreen);
            }
            else if (car.Price > 1000000)
            {
                textView.SetTextColor(Color.DarkRed);
            }

            return textView;
        }

        private View RenderPower(Car car, ViewGroup parentView)
        {
            var view = LayoutInflater.Inflate(Resource.Layout.TableCellPower, parentView, false);
            var kwView = view.FindViewById<TextView>(Resource.Id.kw_view);
            var psView = view.FindViewById<TextView>(Resource.Id.ps_view);

            kwView.Text = car.Kw + " kW";
            psView.Text = car.Ps + " PS";

            return view;
        }

        private View RenderName(Car car)
        {
            var textView = new TextView(Context);
            textView.Text = car.Name;
            textView.SetPadding(20, 10, 20, 10);
            textView.TextSize = TextSize;
            return textView;
        }

        private View RenderProducerLogo(Car car, ViewGroup parentView)
        {
            var view = LayoutInflater.Inflate(Resource.Layout.TableCellImage, parentView, false);
            var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
            imageView.SetImageResource(car.Producer.Logo);
            return view;
        }
    }
}
