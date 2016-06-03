namespace NineOldAndroidsSample.PathAnimation
{
    /// <summary>
    /// The possible path operations that describe how to move from a preceding PathPoint to the
    /// location described by this PathPoint.
    /// </summary>
    public enum PathPointOperation
    {
        Move,
        Line,
        Curve
    }

    /// <summary>
    /// A class that holds information about a location and how the path should get to that
    /// location from the previous path location (if any). Any PathPoint holds the information for
    /// its location as well as the instructions on how to traverse the preceding interval from the
    /// previous location.
    /// </summary>
    public class PathPoint : Java.Lang.Object
    {
        public float X { get; private set; }

        public float Y { get; private set; }

        public float Control0X { get; private set; }

        public float Control0Y { get; private set; }

        public float Control1X { get; private set; }

        public float Control1Y { get; private set; }

        /// <summary>
        /// The motion described by the path to get from the previous PathPoint in an AnimatorPath
        /// to the location of this PathPoint. This can be one of MOVE, LINE, or CURVE.
        /// </summary>
        public PathPointOperation Operation { get; private set; }

        /// <summary>
        /// Line/Move constructor
        /// </summary>
        private PathPoint(PathPointOperation operation, float x, float y)
        {
            Operation = operation;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Curve constructor
        /// </summary>
        private PathPoint(float c0X, float c0Y, float c1X, float c1Y, float x, float y)
            : this(PathPointOperation.Curve, x, y)
        {
            Control0X = c0X;
            Control0Y = c0Y;
            Control1X = c1X;
            Control1Y = c1Y;
        }

        /// <summary>
        /// Constructs and returns a PathPoint object that describes a line to the given xy location.
        /// </summary>
        public static PathPoint LineTo(float x, float y)
        {
            return new PathPoint(PathPointOperation.Line, x, y);
        }

        /// <summary>
        /// Constructs and returns a PathPoint object that describes a cubic Bezier curve to the
        /// given xy location with the control points at c0 and c1.
        /// </summary>
        public static PathPoint CurveTo(float c0X, float c0Y, float c1X, float c1Y, float x, float y)
        {
            return new PathPoint(c0X, c0Y, c1X, c1Y, x, y);
        }

        /// <summary>
        /// Constructs and returns a PathPoint object that describes a discontinuous move to the given
        /// xy location.
        /// </summary>
        public static PathPoint MoveTo(float x, float y)
        {
            return new PathPoint(PathPointOperation.Move, x, y);
        }
    }
}
