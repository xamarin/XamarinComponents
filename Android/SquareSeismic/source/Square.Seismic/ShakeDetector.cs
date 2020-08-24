using System;

namespace Square.Seismic
{
    partial class ShakeDetector
    {
        public unsafe ShakeDetector(Action hearShake)
            : this(new ListenerActions(hearShake))
        {
        }

        internal class ListenerActions : Java.Lang.Object, IListener
        {
            private Action hearShake;

            public ListenerActions(Action hearShake)
            {
                this.hearShake = hearShake;
            }

            public void HearShake()
            {
                if (hearShake != null)
                {
                    hearShake();
                }
            }
        }
    }
}
