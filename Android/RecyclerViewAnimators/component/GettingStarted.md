
**RecyclerView Animators** is an Android library that allows developers to easily create `RecyclerView`
with animations.

**Features**
 - Animate addition and removal of `ItemAnimator`
 - Appearance animations for items in `RecyclerView.Adapter`

## Using ItemAnimator

First, set the `RecyclerView`'s item animator.

    var recyclerView = FindViewById<RecyclerView>(Resource.Id.list);
    var animator = new SlideInUpAnimator(new OvershootInterpolator(1f);
    recyclerView.SetItemAnimator(animator);

Then, after modifying the list, use the `NotifyItemRemoved` and `NotifyItemInserted`
methods.

    public void Remove(int position) {
        dataset.RemoveAt(position);
        NotifyItemRemoved(position);
    }
    
    public void Add(string text, int position) {
        dataset.Insert(position, text);
        NotifyItemInserted(position);
    }

You can change the durations.

    var itemAnimator = recyclerView.GetItemAnimator();
    itemAnimator.AddDuration = 1000;
    itemAnimator.RemoveDuration = 1000;
    itemAnimator.MoveDuration = 1000;
    itemAnimator.ChangeDuration = 1000;

And, change the interpolator.

    var animator = new SlideInLeftAnimator();
    animator.SetInterpolator(new OvershootInterpolator());
    recyclerView.SetItemAnimator(animator);

### Custom ItemAnimator Animations

By extending `AnimateViewHolder`, you can override preset animation. 
So, custom animation can be set depending on view holder.

    private class MyViewHolder : AnimateViewHolder
    {
        public MyViewHolder(View itemView)
            : base (itemView)
        {
        }

        public override void PreAnimateRemoveImpl()
        {
            ViewCompat.SetTranslationY(itemView, 0.0f);
            ViewCompat.SetAlpha(itemView, 1);
        }

        public override void AnimateRemoveImpl(IViewPropertyAnimatorListener listener)
        {
            ViewCompat.Animate(itemView)
                      .TranslationY(-itemView.Height * 0.3f)
                      .Alpha(0)
                      .SetDuration(300)
                      .SetListener(listener)
                      .Start();
        }

        public override void PreAnimateAddImpl()
        {
            ViewCompat.SetTranslationY(itemView, -itemView.Height * 0.3f);
            ViewCompat.SetAlpha(itemView, 0);
        }

        public override void AnimateAddImpl(IViewPropertyAnimatorListener listener)
        {
            ViewCompat.Animate(itemView)
                      .TranslationY(0)
                      .Alpha(1)
                      .SetDuration(300)
                      .SetListener(listener)
                      .Start();
        }
    }

### Provided Item Animators

There are several `ItemAnimator` implementations that can be used 
to provide custom animations for the `RecyclerView`.

**Cool**
 - `LandingAnimator`
 
**Scale**
 - `ScaleInAnimator`
 - `ScaleInTopAnimator`
 - `ScaleInBottomAnimator`  
 - `ScaleInLeftAnimator`
 - `ScaleInRightAnimator`
 
**Fade**
 - `FadeInAnimator`
 - `FadeInDownAnimator`
 - `FadeInUpAnimator`  
 - `FadeInLeftAnimator`
 - `FadeInRightAnimator`
 
**Flip**
 - `FlipInTopXAnimator`
 - `FlipInBottomXAnimator`  
 - `FlipInLeftYAnimator`
 - `FlipInRightYAnimator`

**Slide**
 - `SlideInLeftAnimator`
 - `SlideInRightAnimator`
 - `OvershootInLeftAnimator`
 - `OvershootInRightAnimator`  
 - `SlideInUpAnimator`
 - `SlideInDownAnimator`

## RecyclerView Adapters

In addition to various item animators, there are various adapters that
also provide the means to use cusom animations. 

First, set the `RecyclerView` item animator by wrapping an existing adapter.

    var recyclerView = FindViewById<RecyclerView>(Resource.Id.list);
    var adapter = new MyAdapter();
    var wrapped = new AlphaInAnimationAdapter(adapter);
    recyclerView.SetAdapter(wrapped);

The wrapped adapter can be customized by changing the durations.

    var adapter = new MyAdapter();
    var alphaAdapter = new AlphaInAnimationAdapter(adapter);
    alphaAdapter.SetDuration(1000);
    recyclerView.SetAdapter(alphaAdapter);

Or, by changing the interpolator.

    var adapter = new MyAdapter();
    var alphaAdapter = new AlphaInAnimationAdapter(adapter);
    alphaAdapter.SetInterpolator(new OvershootInterpolator());
    recyclerView.SetAdapter(alphaAdapter);

Other customizations include the ability to disable the first scroll mode.

    var adapter = new MyAdapter();
    var alphaAdapter = new AlphaInAnimationAdapter(adapter);
    scaleAdapter.SetFirstOnly(false);
    recyclerView.SetAdapter(alphaAdapter);

And, to provide multiple animations:

    var adapter = new MyAdapter();
    var alphaAdapter = new AlphaInAnimationAdapter(adapter);
    var scaleAdapter = new ScaleInAnimationAdapter(alphaAdapter);
    var recyclerView.setAdapter(scaleAdapter);

### Provided Adapters

There are several `RecyclerView.Adapter` implementations that can be used 
to provide custom animations for the `RecyclerView`.

**Alpha**
 - `AlphaInAnimationAdapter`

**Scale**
 - `ScaleInAnimationAdapter`

**Slide**
 - `SlideInBottomAnimationAdapter`  
 - `SlideInRightAnimationAdapter`
 - `SlideInLeftAnimationAdapter`
