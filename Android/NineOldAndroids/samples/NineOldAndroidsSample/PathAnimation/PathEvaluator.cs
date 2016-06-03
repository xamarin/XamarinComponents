using NineOldAndroids.Animation;

namespace NineOldAndroidsSample.PathAnimation
{
    /// <summary>
    /// This evaluator interpolates between two PathPoint values given the value t, the
    /// proportion traveled between those points. The value of the interpolation depends
    /// on the operation specified by the endValue (the operation for the interval between
    /// PathPoints is always specified by the end point of that interval).
    /// </summary>
    public class PathEvaluator : Java.Lang.Object, ITypeEvaluator
    {
        public Java.Lang.Object Evaluate(float t, Java.Lang.Object startValue, Java.Lang.Object endValue)
        {
            var start = (PathPoint)startValue;
            var end = (PathPoint)endValue;

            float x, y;
            if (end.Operation == PathPointOperation.Curve)
            {
                float oneMinusT = 1 - t;
                x = oneMinusT * oneMinusT * oneMinusT * start.X +
                    3 * oneMinusT * oneMinusT * t * end.Control0X +
                    3 * oneMinusT * t * t * end.Control1X +
                    t * t * t * end.X;
                y = oneMinusT * oneMinusT * oneMinusT * start.Y +
                    3 * oneMinusT * oneMinusT * t * end.Control0Y +
                    3 * oneMinusT * t * t * end.Control1Y +
                    t * t * t * end.Y;
            }
            else if (end.Operation == PathPointOperation.Line)
            {
                x = start.X + t * (end.X - start.X);
                y = start.Y + t * (end.Y - start.Y);
            }
            else
            {
                x = end.X;
                y = end.Y;
            }
            return PathPoint.MoveTo(x, y);
        }
    }
}
