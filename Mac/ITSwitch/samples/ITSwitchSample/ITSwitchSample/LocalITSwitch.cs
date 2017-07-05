using System;
using ITSwitch;
using Foundation;
using CoreGraphics;

namespace ITSwitchSample
{
	[Register("LocalITSwitch")]
	public class LocalITSwitch : ITSwitchView
	{
		public LocalITSwitch()
			: base()
		{
			
		}


		public LocalITSwitch(IntPtr ptr) 
			: base(ptr)
		{
			
		}

		public LocalITSwitch(NSCoder coder) : base(coder)
		{
			
		}

		public LocalITSwitch(CGRect frame) : base(frame)
		{
			
		}
	}
}

