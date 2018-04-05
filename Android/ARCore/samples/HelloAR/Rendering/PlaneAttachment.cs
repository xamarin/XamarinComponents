using System;
using Google.AR.Core;

namespace HelloAR
{
	public class PlaneAttachment
	{
		Plane plane;
		Anchor anchor;

		// Allocate temporary storage to avoid multiple allocations per frame.
		float[] mPoseTranslation = new float[3];
		float[] mPoseRotation = new float[4];

		public PlaneAttachment(Plane plane, Anchor anchor)
		{
			this.plane = plane;
			this.anchor = anchor;
		}

		public bool IsTracking
		{
			get {
				return /*true if*/
					plane.TrackingState == TrackingState.Tracking
                         && anchor.TrackingState == TrackingState.Tracking;
			}
		}

		public Pose GetPose()
		{
			var pose = anchor.Pose;
			pose.GetTranslation(mPoseTranslation, 0);
			pose.GetRotationQuaternion(mPoseRotation, 0);
			mPoseTranslation[1] = plane.CenterPose.Ty();
			return new Pose(mPoseTranslation, mPoseRotation);
		}

		public Anchor GetAnchor()
		{
			return anchor;
		}

	}
}
