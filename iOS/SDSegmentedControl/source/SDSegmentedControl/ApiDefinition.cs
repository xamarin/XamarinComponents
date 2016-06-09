using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace SegmentedControl
{
	[BaseType(typeof(UISegmentedControl))]
	interface SDSegmentedControl : IUIScrollViewDelegate
	{
		[New]
		[Export("backgroundColor", ArgumentSemantic.Retain)]
		UIColor BackgroundColor { get; set; }

		[Export("borderColor", ArgumentSemantic.Retain)]
		UIColor BorderColor { get; set; }

		[Export("borderWidth", ArgumentSemantic.Assign)]
		nfloat BorderWidth { get; set; }

		[Export("arrowSize", ArgumentSemantic.Assign)]
		nfloat ArrowSize { get; set; }

		[Export("arrowPosition", ArgumentSemantic.Assign)]
		SDSegmentedArrowPosition ArrowPosition { get; set; }

		[Export("arrowHeightFactor", ArgumentSemantic.Assign)]
		nfloat ArrowHeightFactor { get; set; }

		[Export("animationDuration", ArgumentSemantic.Assign)]
		double AnimationDuration { get; set; }

		[Export("interItemSpace", ArgumentSemantic.Assign)]
		nfloat InterItemSpace { get; set; }

		[Export("stainEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets StainEdgeInsets { get; set; }

		[Export("shadowColor", ArgumentSemantic.Retain)]
		UIColor ShadowColor { get; set; }

		[Export("shadowRadius", ArgumentSemantic.Assign)]
		nfloat ShadowRadius { get; set; }

		[Export("shadowOpacity", ArgumentSemantic.Assign)]
		nfloat ShadowOpacity { get; set; }

		[Export("shadowOffset", ArgumentSemantic.Assign)]
		CGSize ShadowOffset { get; set; }

		[Export("titleFont", ArgumentSemantic.Assign)]
		UIFont TitleFont { get; set; }

		[Export("selectedTitleFont", ArgumentSemantic.Assign)]
		UIFont SelectedTitleFont { get; set; }

		[Export("scrollView")]
		UIScrollView ScrollView { get; set; }
	}

	[BaseType(typeof(UIButton))]
	interface SDSegmentView
	{

		[Export("imageSize", ArgumentSemantic.Assign)]
		CGSize ImageSize { get; set; }

		[Export("titleFont", ArgumentSemantic.Retain)]
		UIFont TitleFont { get; set; }

		[Export("selectedTitleFont", ArgumentSemantic.Retain)]
		UIFont SelectedTitleFont { get; set; }

		[New]
		[Export("titleShadowOffset", ArgumentSemantic.Assign)]
		CGSize TitleShadowOffset { get; set; }
	}

	[BaseType(typeof(UIView))]
	interface SDStainView
	{

		[New]
		[Export("backgroundColor", ArgumentSemantic.Retain)]
		UIColor BackgroundColor { get; set; }

		[Export("cornerRadius", ArgumentSemantic.Assign)]
		nfloat CornerRadius { get; set; }

		[Export("edgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets EdgeInsets { get; set; }

		[Export("shadowOffset", ArgumentSemantic.Assign)]
		CGSize ShadowOffset { get; set; }

		[Export("shadowBlur", ArgumentSemantic.Assign)]
		nfloat ShadowBlur { get; set; }

		[Export("shadowColor", ArgumentSemantic.Assign)]
		UIColor ShadowColor { get; set; }

		[Export("innerStrokeLineWidth", ArgumentSemantic.Assign)]
		nfloat InnerStrokeLineWidth { get; set; }

		[Export("innerStrokeColor", ArgumentSemantic.Assign)]
		UIColor InnerStrokeColor { get; set; }
	}
}
