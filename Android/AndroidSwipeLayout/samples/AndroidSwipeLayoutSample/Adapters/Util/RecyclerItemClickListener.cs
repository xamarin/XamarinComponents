using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;

namespace AndroidSwipeLayoutSample.Adapters.Utils
{
//	public class RecyclerItemClickListener : RecyclerView.OnItemTouchListener {
//	    private OnItemClickListener mListener;
//
//	    public interface OnItemClickListener {
//	        public void onItemClick(View view, int position);
//	    }
//
//	    GestureDetector mGestureDetector;
//
//	    public RecyclerItemClickListener(Context context, OnItemClickListener listener) {
//	        mListener = listener;
//	        mGestureDetector = new GestureDetector(context, new GestureDetector.SimpleOnGestureListener() {
//	            @Override
//	            public boolean onSingleTapUp(MotionEvent e) {
//	                return true;
//	            }
//	        });
//	    }
//
//	    @Override
//	    public boolean onInterceptTouchEvent(RecyclerView view, MotionEvent e) {
//	        View childView = view.findChildViewUnder(e.getX(), e.getY());
//	        if (childView != null && mListener != null && mGestureDetector.onTouchEvent(e)) {
//	            mListener.onItemClick(childView, view.getChildPosition(childView));
//	        }
//	        return false;
//	    }
//
//	    @Override
//	    public void onTouchEvent(RecyclerView view, MotionEvent motionEvent) {
//	    }
//	}
}
