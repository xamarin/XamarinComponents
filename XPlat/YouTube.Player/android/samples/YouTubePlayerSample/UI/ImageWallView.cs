using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

namespace YouTubePlayerSample
{
	public class ImageWallView : ViewGroup
	{
		private readonly Random random = new Random();
		private readonly List<int> unInitializedImages = new List<int>();
		private ImageView[] images = new ImageView[0];

		private int imageWidth;
		private int imageHeight;
		private int interImagePadding;

		private int numberOfColumns;
		private int numberOfRows;

		public ImageWallView(Context context, int imageWidth, int imageHeight, int interImagePadding)
			: base(context)
		{
			random = new Random();

			this.imageWidth = imageWidth;
			this.imageHeight = imageHeight;
			this.interImagePadding = interImagePadding;
		}

		protected override void OnSizeChanged(int width, int height, int oldWidth, int oldHeight)
		{
			// create enough columns to fill view's width, plus an extra column at either side to allow
			// images to have diagonal offset across the screen.
			numberOfColumns = width / (imageWidth + interImagePadding) + 2;
			// create enough rows to fill the view's height (adding an extra row at bottom if necessary).
			numberOfRows = height / (imageHeight + interImagePadding);
			numberOfRows += (height % (imageHeight + interImagePadding) == 0) ? 0 : 1;

			if ((numberOfRows <= 0) || (numberOfColumns <= 0))
			{
				throw new Exception($"Error creating an ImageWallView with {numberOfRows} rows and {numberOfColumns} columns. Both values must be greater than zero.");
			}

			if (images.Length < (numberOfColumns * numberOfRows))
			{
				var old = images;
				images = new ImageView[numberOfColumns * numberOfRows];
				Array.Copy(old, images, old.Length);
			}

			RemoveAllViews();
			for (int col = 0; col < numberOfColumns; col++)
			{
				for (int row = 0; row < numberOfRows; row++)
				{
					int elementIdx = GetElementIdx(col, row);
					if (images[elementIdx] == null)
					{
						var thumbnail = new ImageView(Context);
						thumbnail.LayoutParameters = new LayoutParams(imageWidth, imageHeight);
						images[elementIdx] = thumbnail;
						unInitializedImages.Add(elementIdx);
					}
					AddView(images[elementIdx]);
				}
			}
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			var displayMetrics = Resources.DisplayMetrics;
			var width = GetDefaultSize(displayMetrics.WidthPixels, widthMeasureSpec);
			var height = GetDefaultSize(displayMetrics.HeightPixels, heightMeasureSpec);
			SetMeasuredDimension(width, height);
		}

		protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
		{
			for (int col = 0; col < numberOfColumns; col++)
			{
				for (int row = 0; row < numberOfRows; row++)
				{
					int x = (col - 1) * (imageWidth + interImagePadding) + (row * (imageWidth / numberOfRows));
					int y = row * (imageHeight + interImagePadding);
					images[col * numberOfRows + row].Layout(x, y, x + imageWidth, y + imageHeight);
				}
			}
		}

		public bool AllImagesLoaded => unInitializedImages.Count == 0;

		public int GetXPosition(int col, int row) => GetImage(col, row).Left;

		public int GetYPosition(int col, int row) => GetImage(col, row).Top;

		public void HideImage(int col, int row) => GetImage(col, row).Visibility = ViewStates.Invisible;

		public void ShowImage(int col, int row) => GetImage(col, row).Visibility = ViewStates.Visible;

		private ImageView GetImage(int col, int row) => images[GetElementIdx(col, row)];

		private int GetElementIdx(int col, int row) => (col * numberOfRows) + row;

		public void SetImageDrawable(int col, int row, Drawable drawable)
		{
			int elementIdx = GetElementIdx(col, row);

			unInitializedImages.Remove(elementIdx);
			images[elementIdx].SetImageDrawable(drawable);
		}

		public Drawable GetImageDrawable(int col, int row)
		{
			int elementIdx = GetElementIdx(col, row);
			return images[elementIdx].Drawable;
		}

		public (int col, int row) GetNextLoadTarget()
		{
			int nextElement;
			do
			{
				if (AllImagesLoaded)
				{
					// Don't choose the first or last columns (since they are partly hidden)
					nextElement = random.Next((numberOfColumns - 2) * numberOfRows) + numberOfRows;
				}
				else
				{
					nextElement = unInitializedImages[random.Next(unInitializedImages.Count)];
				}
			} while (images[nextElement].Visibility != ViewStates.Visible);

			int col = nextElement / numberOfRows;
			int row = nextElement % numberOfRows;
			return (col, row);
		}
	}
}
