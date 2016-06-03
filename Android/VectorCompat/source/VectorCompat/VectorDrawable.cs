using Android.Graphics;
using Android.Runtime;

namespace VectorCompat
{
	abstract partial class DrawableCompatInvoker
	{
		[Register ("setColorFilter", "(Landroid/graphics/ColorFilter;)V", "GetSetColorFilter_Landroid_graphics_ColorFilter_Handler")]
		public abstract override void SetColorFilter (ColorFilter cf);
	}
}
