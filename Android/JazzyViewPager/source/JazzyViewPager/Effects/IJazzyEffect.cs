using Android.Views;

namespace Jazzy.Effects
{
    public interface IJazzyEffect
    {
        void Animate(JazzyViewPager viewPager, View left, View right, float positionOffset, float positionOffsetPixels);
    }
}
