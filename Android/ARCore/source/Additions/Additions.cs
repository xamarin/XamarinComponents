using System;

namespace Google.AR.Core
{
    // Add ITrackable interface
    // https://developers.google.com/ar/reference/java/com/google/ar/core/Trackable
    public partial class AugmentedFace : Google.AR.Core.ITrackable {}
    public partial class AugmentedImage : Google.AR.Core.ITrackable {}
    public partial class DepthPoint : Google.AR.Core.ITrackable {}
    public partial class InstantPlacementPoint : Google.AR.Core.ITrackable {}
    public partial class Plane : Google.AR.Core.ITrackable {}
    public partial class Point : Google.AR.Core.ITrackable {}

    public partial class Earth
    {
        ~Earth()
        {
            InternalFinalize();
        }
    }

    public partial class StreetscapeGeometry
    {
        ~StreetscapeGeometry()
        {
            InternalFinalize();
        }
    }
}
