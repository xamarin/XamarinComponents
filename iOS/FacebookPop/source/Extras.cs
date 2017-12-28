using System;
using Foundation;

namespace Facebook.Pop
{
    public partial class POPAnimation : NSObject {

        // common threshold definitions
        public static float ThresholdColor = 0.01f;
        public static float ThresholdPoint = 1.0f;
        public static float ThresholdOpacity = 0.01f;
        public static float ThresholdScale = 0.005f;
        public static float ThresholdRotation = 0.01f;
        public static float ThresholdRadius = 0.01f;

        // CALayer
        public static string LayerBackgroundColor = @"backgroundColor";
        public static string LayerBounds = @"bounds";
        public static string LayerCornerRadius = @"cornerRadius";
        public static string LayerBorderWidth = @"borderWidth";
        public static string LayerBorderColor = @"borderColor";
        public static string LayerOpacity = @"opacity";
        public static string LayerPosition = @"position";
        public static string LayerPositionX = @"positionX";
        public static string LayerPositionY = @"positionY";
        public static string LayerRotation = @"rotation";
        public static string LayerRotationX = @"rotationX";
        public static string LayerRotationY = @"rotationY";
        public static string LayerScaleX = @"scaleX";
        public static string LayerScaleXY = @"scaleXY";
        public static string LayerScaleY = @"scaleY";
        public static string LayerSize = @"size";
        public static string LayerSubscaleXY = @"subscaleXY";
        public static string LayerSubtranslationX = @"subtranslationX";
        public static string LayerSubtranslationXY = @"subtranslationXY";
        public static string LayerSubtranslationY = @"subtranslationY";
        public static string LayerSubtranslationZ = @"subtranslationZ";
        public static string LayerTranslationX = @"translationX";
        public static string LayerTranslationXY = @"translationXY";
        public static string LayerTranslationY = @"translationY";
        public static string LayerTranslationZ = @"translationZ";
        public static string LayerZPosition = @"zPosition";
        public static string LayerShadowColor = @"shadowColor";
        public static string LayerShadowOffset = @"shadowOffset";
        public static string LayerShadowOpacity = @"shadowOpacity";
        public static string LayerShadowRadius = @"shadowRadius";

        // CAShapeLayer
        public static string ShapeLayerStrokeStart = @"shapeLayer.strokeStart";
        public static string ShapeLayerStrokeEnd = @"shapeLayer.strokeEnd";
        public static string ShapeLayerStrokeColor = @"shapeLayer.strokeColor";
        public static string ShapeLayerFillColor = @"shapeLayer.fillColor";
        public static string ShapeLayerLineWidth = @"shapeLayer.lineWidth";
        public static string ShapeLayerLineDashPhase = @"shapeLayer.lineDashPhase";

        // NSLayoutConstraint
        public static string LayoutConstraintConstant = @"layoutConstraint.constant";

        // UIView
        public static string ViewAlpha = @"view.alpha";
        public static string ViewBackgroundColor = @"view.backgroundColor";
        public static string ViewBounds = LayerBounds;
        public static string ViewCenter = @"view.center";
        public static string ViewFrame = @"view.frame";
        public static string ViewScaleX = @"view.scaleX";
        public static string ViewScaleXY = @"view.scaleXY";
        public static string ViewScaleY = @"view.scaleY";
        public static string ViewSize = LayerSize;
        public static string ViewTintColor = @"view.tintColor";

        // UIScrollView
        public static string ScrollViewContentOffset = @"scrollView.contentOffset";
        public static string ScrollViewContentSize = @"scrollView.contentSize";
        public static string ScrollViewZoomScale = @"scrollView.zoomScale";
        public static string ScrollViewContentInset = @"scrollView.contentInset";
        public static string ScrollViewScrollIndicatorInsets = @"scrollView.scrollIndicatorInsets";

        // UITableView
        public static string TableViewContentOffset = ScrollViewContentOffset;
        public static string TableViewContentSize = ScrollViewContentSize;

        // UICollectionView
        public static string CollectionViewContentOffset = ScrollViewContentOffset;
        public static string CollectionViewContentSize = ScrollViewContentSize;

        // UINavigationBar
        public static string NavigationBarBarTintColor = @"navigationBar.barTintColor";

        // UIToolbar
        public static string ToolbarBarTintColor = NavigationBarBarTintColor;

        // UITabBar
        public static string TabBarBarTintColor = NavigationBarBarTintColor;

        //UILabel
        public static string LabelTextColor = @"label.textColor";

        //SceneKit
        public static string SCNNodePosition = @"scnode.position";
        public static string SCNNodePositionX = @"scnnode.position.x";
        public static string SCNNodePositionY = @"scnnode.position.y";
        public static string SCNNodePositionZ = @"scnnode.position.z";
        public static string SCNNodeTranslation = @"scnnode.translation";
        public static string SCNNodeTranslationX = @"scnnode.translation.x";
        public static string SCNNodeTranslationY = @"scnnode.translation.y";
        public static string SCNNodeTranslationZ = @"scnnode.translation.z";
        public static string SCNNodeRotation = @"scnnode.rotation";
        public static string SCNNodeRotationX = @"scnnode.rotation.x";
        public static string SCNNodeRotationY = @"scnnode.rotation.y";
        public static string SCNNodeRotationZ = @"scnnode.rotation.z";
        public static string SCNNodeRotationW = @"scnnode.rotation.w";
        public static string SCNNodeEulerAngles = @"scnnode.eulerAngles";
        public static string SCNNodeEulerAnglesX = @"scnnode.eulerAngles.x";
        public static string SCNNodeEulerAnglesY = @"scnnode.eulerAngles.y";
        public static string SCNNodeEulerAnglesZ = @"scnnode.eulerAngles.z";
        public static string SCNNodeOrientation = @"scnnode.orientation";
        public static string SCNNodeOrientationX = @"scnnode.orientation.x";
        public static string SCNNodeOrientationY = @"scnnode.orientation.y";
        public static string SCNNodeOrientationZ = @"scnnode.orientation.z";
        public static string SCNNodeOrientationW = @"scnnode.orientation.w";
        public static string SCNNodeScale = @"scnnode.scale";
        public static string SCNNodeScaleX = @"scnnode.scale.x";
        public static string SCNNodeScaleY = @"scnnode.scale.y";
        public static string SCNNodeScaleZ = @"scnnode.scale.z";
        public static string SCNNodeScaleXY = @"scnnode.scale.xy";
    }
}