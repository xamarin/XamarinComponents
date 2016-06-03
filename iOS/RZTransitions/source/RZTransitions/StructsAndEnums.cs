using System;
using ObjCRuntime;

namespace RZTransitions
{
	[Native]
	public enum TransitionAction : long
	{
		Push = (1 << 0),
		Pop = (1 << 1),
		Present = (1 << 2),
		Dismiss = (1 << 3),
		Tab = (1 << 4),
		PushPop = Push | Pop,
		PresentDismiss = Present | Dismiss,
		Any = Present | Dismiss | Tab
	}

}

