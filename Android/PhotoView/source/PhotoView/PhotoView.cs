using Android.Graphics;

namespace ImageViews.Photo
{
	partial class PhotoView
	{
		public Matrix DisplayMatrix
		{
			get
			{
				var matrix = new Matrix();
				GetDisplayMatrix(matrix);
				return matrix;
			}
			set => SetDisplayMatrix(value);
		}

		public Matrix SuppMatrix
		{
			get
			{
				var matrix = new Matrix();
				GetSuppMatrix(matrix);
				return matrix;
			}
			set => SetSuppMatrix(value);
		}
	}
}