using Android.Graphics;

namespace ImageViews.Photo
{
	partial class PhotoViewAttacher
	{
		public Matrix DisplayMatrix
		{
			get
			{
				var matrix = new Matrix();
				GetDisplayMatrix(matrix);
				return matrix;
			}
			set
			{
				SetDisplayMatrix(value);
			}
		}
	}
}
