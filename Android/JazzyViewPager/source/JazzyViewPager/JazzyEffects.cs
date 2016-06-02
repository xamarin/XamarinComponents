using Jazzy.Effects;

namespace Jazzy
{
    public static class JazzyEffects
    {
        public static readonly IJazzyEffect Standard = null;

        public static readonly IJazzyEffect Accordion = new AccordionEffect();
        public static readonly IJazzyEffect Cube = new CubeEffect();
        public static readonly IJazzyEffect Fade = new FadeEffect();
        public static readonly IJazzyEffect Flip = new FlipEffect(false);
        public static readonly IJazzyEffect FlipAway = new FlipEffect(true);
        public static readonly IJazzyEffect RotateUp = new RotateEffect(true);
        public static readonly IJazzyEffect RotateDown = new RotateEffect(false);
        public static readonly IJazzyEffect Stack = new StackEffect();
        public static readonly IJazzyEffect Tablet = new TabletEffect();
        public static readonly IJazzyEffect Zoom = new ZoomEffect();
    }
}
