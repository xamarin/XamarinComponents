using System.Collections.Generic;

namespace NineOldAndroidsSample.PathAnimation
{
    /// <summary>
    /// A simple Path object that holds information about the points along
    /// a path. The API allows you to specify a move location (which essentially
    /// jumps from the previous point in the path to the new one), a line location
    /// (which creates a line segment from the previous location) and a curve
    /// location (which creates a cubic Bezier curve from the previous location).
    /// </summary>
    public class AnimatorPath : Java.Lang.Object
    {
        // The points in the path
        private readonly List<PathPoint> points = new List<PathPoint>();

        /// <summary>
        /// Move from the current path point to the new one
        /// specified by x and y. This will create a discontinuity if this point is
        /// neither the first point in the path nor the same as the previous point
        /// in the path.
        /// </summary>
        public void MoveTo(float x, float y)
        {
            points.Add(PathPoint.MoveTo(x, y));
        }

        /// <summary>
        /// Create a straight line from the current path point to the new one
        /// specified by x and y.
        /// </summary>
        public void LineTo(float x, float y)
        {
            points.Add(PathPoint.LineTo(x, y));
        }

        /// <summary>
        /// Create a cubic Bezier curve from the current path point to the new one
        /// specified by x and y. The curve uses the current path location as the first anchor
        /// point, the control points (c0X, c0Y) and (c1X, c1Y), and (x, y) as the end anchor point.
        /// </summary>
        public void CurveTo(float c0X, float c0Y, float c1X, float c1Y, float x, float y)
        {
            points.Add(PathPoint.CurveTo(c0X, c0Y, c1X, c1Y, x, y));
        }

        /// <summary>
        /// Returns a Collection of PathPoint objects that describe all points in the path.
        /// </summary>
        public PathPoint[] Points
        {
            get { return points.ToArray(); }
        }
    }
}
