using System;
using Android.Content;
using Android.Hardware.Display;
using Android.Views;
using Java.Interop;

namespace HelloAR
{
    public class DisplayRotationHelper : Java.Lang.Object, DisplayManager.IDisplayListener
    {
        bool mViewportChanged;
        int mViewportWidth;
        int mViewportHeight;
        Context mContext;
        Display mDisplay;

        /**
         * Constructs the DisplayRotationHelper but does not register the listener yet.
         *
         * @param context the Android {@link Context}.
         */
        public DisplayRotationHelper(Context context)
        {
            mContext = context;
            mDisplay = context.GetSystemService(Java.Lang.Class.FromType(typeof(IWindowManager)))
                              .JavaCast<IWindowManager>().DefaultDisplay;
        }

        /** Registers the display listener. Should be called from {@link Activity#onResume()}. */
        public void OnResume()
        {
            mContext.GetSystemService(Java.Lang.Class.FromType(typeof(DisplayManager)))
                    .JavaCast<DisplayManager>().RegisterDisplayListener(this, null);
        }

        /** Unregisters the display listener. Should be called from {@link Activity#onPause()}. */
        public void OnPause()
        {
            mContext.GetSystemService(Java.Lang.Class.FromType(typeof(DisplayManager)))
                .JavaCast<DisplayManager>().UnregisterDisplayListener(this);
        }

        /**
         * Records a change in surface dimensions. This will be later used by
         * {@link #updateSessionIfNeeded(Session)}. Should be called from
         * {@link android.opengl.GLSurfaceView.Renderer
         *  #onSurfaceChanged(javax.microedition.khronos.opengles.GL10, int, int)}.
         *
         * @param width the updated width of the surface.
         * @param height the updated height of the surface.
         */
        public void OnSurfaceChanged(int width, int height)
        {
            mViewportWidth = width;
            mViewportHeight = height;
            mViewportChanged = true;
        }

        /**
         * Updates the session display geometry if a change was posted either by
         * {@link #onSurfaceChanged(int, int)} call or by {@link #onDisplayChanged(int)} system
         * callback. This function should be called explicitly before each call to
         * {@link Session#update()}. This function will also clear the 'pending update'
         * (viewportChanged) flag.
         *
         * @param session the {@link Session} object to update if display geometry changed.
         */
        public void UpdateSessionIfNeeded(Google.AR.Core.Session session)
        {
            if (mViewportChanged) {
                int displayRotation = (int)mDisplay.Rotation;
                session.SetDisplayGeometry(displayRotation, mViewportWidth, mViewportHeight);
                mViewportChanged = false;
            }
        }

        /**
         * Returns the current rotation state of android display.
         * Same as {@link Display#getRotation()}.
         */
        public int getRotation()
        {
            return (int)mDisplay.Rotation;
        }

        public void OnDisplayAdded(int displayId) { }

        public void OnDisplayRemoved(int displayId) { }

        public void OnDisplayChanged(int displayId)
        {
            mViewportChanged = true;
        }
    }
}
