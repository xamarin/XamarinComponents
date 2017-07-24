using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ObjCRuntime;
using CoreAnimation;
using CoreGraphics;

namespace Facebook.Pop
{
	[Native]
	public enum POPAnimationEventType : ulong {
		PropertyRead = 0,
		PropertyWrite,
		ToValueUpdate,
		FromValueUpdate,
		VelocityUpdate,
		BouncinessUpdate,
		SpeedUpdate,
		FrictionUpdate,
		MassUpdate,
		TensionUpdate,
		DidStart,
		DidStop,
		DidReachToValue,
		Autoreversed
	}

	[Native]
	public enum POPAnimationClampFlags : ulong {
		None = 0,
		Start = 1UL << 0,
		End = 1UL << 1,
		Both = Start | End
	}

	static class CFunctions
	{
		//// extern CGFloat POPAnimationDragCoefficient ();
		//[DllImport ("__Internal")]
		//static extern nfloat POPAnimationDragCoefficient ();

		// extern CGFloat POPLayerGetScaleX (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetScaleX (CALayer l);

		// extern void POPLayerSetScaleX (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetScaleX (CALayer l, nfloat f);

		// extern CGFloat POPLayerGetScaleY (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetScaleY (CALayer l);

		// extern void POPLayerSetScaleY (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetScaleY (CALayer l, nfloat f);

		// extern CGFloat POPLayerGetScaleZ (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetScaleZ (CALayer l);

		// extern void POPLayerSetScaleZ (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetScaleZ (CALayer l, nfloat f);

		// extern CGPoint POPLayerGetScaleXY (CALayer *l);
		[DllImport ("__Internal")]
		static extern CGPoint POPLayerGetScaleXY (CALayer l);

		// extern void POPLayerSetScaleXY (CALayer *l, CGPoint p);
		[DllImport ("__Internal")]
		static extern void POPLayerSetScaleXY (CALayer l, CGPoint p);

		// extern CGFloat POPLayerGetTranslationX (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetTranslationX (CALayer l);

		// extern void POPLayerSetTranslationX (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetTranslationX (CALayer l, nfloat f);

		// extern CGFloat POPLayerGetTranslationY (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetTranslationY (CALayer l);

		// extern void POPLayerSetTranslationY (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetTranslationY (CALayer l, nfloat f);

		// extern CGFloat POPLayerGetTranslationZ (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetTranslationZ (CALayer l);

		// extern void POPLayerSetTranslationZ (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetTranslationZ (CALayer l, nfloat f);

		// extern CGPoint POPLayerGetTranslationXY (CALayer *l);
		[DllImport ("__Internal")]
		static extern CGPoint POPLayerGetTranslationXY (CALayer l);

		// extern void POPLayerSetTranslationXY (CALayer *l, CGPoint p);
		[DllImport ("__Internal")]
		static extern void POPLayerSetTranslationXY (CALayer l, CGPoint p);

		// extern CGFloat POPLayerGetRotationX (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetRotationX (CALayer l);

		// extern void POPLayerSetRotationX (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetRotationX (CALayer l, nfloat f);

		// extern CGFloat POPLayerGetRotationY (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetRotationY (CALayer l);

		// extern void POPLayerSetRotationY (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetRotationY (CALayer l, nfloat f);

		// extern CGFloat POPLayerGetRotationZ (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetRotationZ (CALayer l);

		// extern void POPLayerSetRotationZ (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetRotationZ (CALayer l, nfloat f);

		// extern CGFloat POPLayerGetRotation (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetRotation (CALayer l);

		// extern void POPLayerSetRotation (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetRotation (CALayer l, nfloat f);

		// extern CGPoint POPLayerGetSubScaleXY (CALayer *l);
		[DllImport ("__Internal")]
		static extern CGPoint POPLayerGetSubScaleXY (CALayer l);

		// extern void POPLayerSetSubScaleXY (CALayer *l, CGPoint p);
		[DllImport ("__Internal")]
		static extern void POPLayerSetSubScaleXY (CALayer l, CGPoint p);

		// extern CGFloat POPLayerGetSubTranslationX (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetSubTranslationX (CALayer l);

		// extern void POPLayerSetSubTranslationX (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetSubTranslationX (CALayer l, nfloat f);

		// extern CGFloat POPLayerGetSubTranslationY (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetSubTranslationY (CALayer l);

		// extern void POPLayerSetSubTranslationY (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetSubTranslationY (CALayer l, nfloat f);

		// extern CGFloat POPLayerGetSubTranslationZ (CALayer *l);
		[DllImport ("__Internal")]
		static extern nfloat POPLayerGetSubTranslationZ (CALayer l);

		// extern void POPLayerSetSubTranslationZ (CALayer *l, CGFloat f);
		[DllImport ("__Internal")]
		static extern void POPLayerSetSubTranslationZ (CALayer l, nfloat f);

		// extern CGPoint POPLayerGetSubTranslationXY (CALayer *l);
		[DllImport ("__Internal")]
		static extern CGPoint POPLayerGetSubTranslationXY (CALayer l);

		// extern void POPLayerSetSubTranslationXY (CALayer *l, CGPoint p);
		[DllImport ("__Internal")]
		static extern void POPLayerSetSubTranslationXY (CALayer l, CGPoint p);
	}
}