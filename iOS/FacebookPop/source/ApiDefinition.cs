using System;
using System.Drawing;

using ObjCRuntime;
using Foundation;
using UIKit;
using CoreAnimation;

namespace Facebook.Pop
{

    delegate void POPAnimatablePropertyReadBlock(NSObject obj, IntPtr values);
    delegate void POPAnimatablePropertyWriteBlock(NSObject obj, IntPtr values);

    // @interface POPAnimatableProperty : NSObject <NSCopying, NSMutableCopying>
    [BaseType (typeof (NSObject))]
    interface POPAnimatableProperty : INSCopying, INSMutableCopying {

        // @property (readonly, copy, nonatomic) NSString * name;
        [Export ("name")]
        string Name { get; }

        // @property (readonly, copy, nonatomic) void (^)(id, CGFloat *) readBlock;
        [Export ("readBlock", ArgumentSemantic.Copy)]
        POPAnimatablePropertyReadBlock ReadBlock { get; }

        // @property (readonly, copy, nonatomic) void (^)(id, const CGFloat *) writeBlock;
        [Export ("writeBlock", ArgumentSemantic.Copy)]
        POPAnimatablePropertyWriteBlock WriteBlock { get; }

        // @property (readonly, assign, nonatomic) CGFloat threshold;
        [Export ("threshold", ArgumentSemantic.UnsafeUnretained)]
        nfloat Threshold { get; }

        // +(id)propertyWithName:(NSString *)name;
        [Static, Export ("propertyWithName:")]
        NSObject PropertyWithName (string name);

        // +(id)propertyWithName:(NSString *)name initializer:(void (^)(POPMutableAnimatableProperty *))block;
        [Static, Export ("propertyWithName:initializer:")]
        NSObject PropertyWithName (string name, Action<POPMutableAnimatableProperty> block);
    }

    // @interface POPMutableAnimatableProperty : POPAnimatableProperty
    [BaseType (typeof (POPAnimatableProperty))]
    interface POPMutableAnimatableProperty {

        // @property (readwrite, copy, nonatomic) NSString * name;
        [Export ("name")]
        string Name { get; set; }

        //// @property (readwrite, copy, nonatomic) void (^)(id, CGFloat *) readBlock;
        //[Export ("readBlock", ArgumentSemantic.Copy)]
        //POPAnimatablePropertyReadBlock ReadBlock { get; set; }

        //// @property (readwrite, copy, nonatomic) void (^)(id, const CGFloat *) writeBlock;
        //[Export ("writeBlock", ArgumentSemantic.Copy)]
        //POPAnimatablePropertyWriteBlock WriteBlock { get; set; }

        // @property (assign, readwrite, nonatomic) CGFloat threshold;
        [Export ("threshold")]
        nfloat Threshold { get; set; }
    }

    // @interface POPAnimationEvent : NSObject
    [BaseType (typeof (NSObject))]
    interface POPAnimationEvent {

        // @property (readonly, assign, nonatomic) POPAnimationEventType type;
        [Export ("type", ArgumentSemantic.UnsafeUnretained)]
        POPAnimationEventType Type { get; }

        // @property (readonly, assign, nonatomic) CFTimeInterval time;
        [Export ("time", ArgumentSemantic.UnsafeUnretained)]
        double Time { get; }

        // @property (readonly, copy, nonatomic) NSString * animationDescription;
        [Export ("animationDescription")]
        string AnimationDescription { get; }
    }

    // @interface POPAnimationValueEvent : POPAnimationEvent
    [BaseType (typeof (POPAnimationEvent))]
    interface POPAnimationValueEvent {

        // @property (readonly, nonatomic, strong) id value;
        [Export ("value", ArgumentSemantic.Strong)]
        NSObject Value { get; }

        // @property (readonly, nonatomic, strong) id velocity;
        [Export ("velocity", ArgumentSemantic.Strong)]
        NSObject Velocity { get; }
    }

    // @interface POPAnimationTracer : NSObject
    [BaseType (typeof (NSObject))]
    interface POPAnimationTracer {

        // @property (readonly, assign, nonatomic) NSArray * allEvents;
        [Export ("allEvents", ArgumentSemantic.UnsafeUnretained)]
        POPAnimationEvent [] AllEvents { get; }

        // @property (readonly, assign, nonatomic) NSArray * writeEvents;
        [Export ("writeEvents", ArgumentSemantic.UnsafeUnretained)]
        POPAnimationEvent [] WriteEvents { get; }

        // @property (assign, nonatomic) BOOL shouldLogAndResetOnCompletion;
        [Export ("shouldLogAndResetOnCompletion", ArgumentSemantic.UnsafeUnretained)]
        bool ShouldLogAndResetOnCompletion { get; set; }

        // -(void)start;
        [Export ("start")]
        void Start ();

        // -(void)stop;
        [Export ("stop")]
        void Stop ();

        // -(void)reset;
        [Export ("reset")]
        void Reset ();

        // -(NSArray *)eventsWithType:(POPAnimationEventType)type;
        [Export ("eventsWithType:")]
        POPAnimationEvent [] EventsWithType (POPAnimationEventType type);
    }

    // @interface POPAnimation : NSObject
    [BaseType (typeof (NSObject))]
    interface POPAnimation {

        // @property (copy, nonatomic) NSString * name;
        [Export ("name")]
        string Name { get; set; }

        // @property (assign, nonatomic) CFTimeInterval beginTime;
        [Export ("beginTime", ArgumentSemantic.UnsafeUnretained)]
        double BeginTime { get; set; }

        // @property (nonatomic, weak) id delegate;
        [Export ("delegate", ArgumentSemantic.Weak)]
        [NullAllowed]
        NSObject WeakDelegate { get; set; }

        // @property (nonatomic, weak) id delegate;
        [Wrap ("WeakDelegate")]
        POPAnimationDelegate Delegate { get; set; }

        // @property (readonly, nonatomic) POPAnimationTracer * tracer;
        [Export ("tracer")]
        POPAnimationTracer Tracer { get; }

        // @property (copy, nonatomic) void (^)(POPAnimation *) animationDidStartBlock;
        [Export ("animationDidStartBlock", ArgumentSemantic.Copy)]
        Action<POPAnimation> AnimationStartedAction { get; set; }

        // @property (copy, nonatomic) void (^)(POPAnimation *) animationDidReachToValueBlock;
        [Export ("animationDidReachToValueBlock", ArgumentSemantic.Copy)]
        Action<POPAnimation> AnimationReachedToValueAction { get; set; }

        // @property (copy, nonatomic) void (^)(POPAnimation *, BOOL) completionBlock;
        [Export ("completionBlock", ArgumentSemantic.Copy)]
        Action<POPAnimation, bool> CompletionAction { get; set; }

        // @property (copy, nonatomic) void (^)(POPAnimation *) animationDidApplyBlock;
        [Export ("animationDidApplyBlock", ArgumentSemantic.Copy)]
        Action<POPAnimation> AnimationAppliedAction { get; set; }

        // @property (assign, nonatomic) BOOL removedOnCompletion;
        [Export ("removedOnCompletion", ArgumentSemantic.UnsafeUnretained)]
        bool RemovedOnCompletion { get; set; }

        // @property (assign, nonatomic, getter = isPaused) BOOL paused;
        [Export ("paused", ArgumentSemantic.UnsafeUnretained)]
        bool Paused { [Bind ("isPaused")] get; set; }

        // @property (assign, nonatomic) BOOL autoreverses;
        [Export ("autoreverses", ArgumentSemantic.UnsafeUnretained)]
        bool AutoReverse { get; set; }

        // @property (assign, nonatomic) NSInteger repeatCount;
        [Export ("repeatCount", ArgumentSemantic.UnsafeUnretained)]
        nint RepeatCount { get; set; }

        // @property (assign, nonatomic) BOOL repeatForever;
        [Export ("repeatForever", ArgumentSemantic.UnsafeUnretained)]
        bool RepeatForever { get; set; }
    }

    // @protocol POPAnimationDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject))]
    interface POPAnimationDelegate {

        // @optional -(void)pop_animationDidStart:(POPAnimation *)anim;
        [Export ("pop_animationDidStart:")]
        void AnimationStarted (POPAnimation anim);

        // @optional -(void)pop_animationDidReachToValue:(POPAnimation *)anim;
        [Export ("pop_animationDidReachToValue:")]
        void AnimationReachedToValue (POPAnimation anim);

        // @optional -(void)pop_animationDidStop:(POPAnimation *)anim finished:(BOOL)finished;
        [Export ("pop_animationDidStop:finished:")]
        void Finished (POPAnimation anim, bool finished);

        // @optional -(void)pop_animationDidApply:(POPAnimation *)anim;
        [Export ("pop_animationDidApply:")]
        void AnimationDidApplied (POPAnimation anim);
    }

    // @interface POP (NSObject)
    [Category]
    [BaseType (typeof (NSObject))]
    interface POP {

        // -(void)pop_addAnimation:(POPAnimation *)anim forKey:(NSString *)key;
        [Export ("pop_addAnimation:forKey:")]
        void AddAnimation (POPAnimation anim, string key);

        // -(void)pop_removeAllAnimations;
        [Export ("pop_removeAllAnimations")]
        void RemoveAllAnimations ();

        // -(void)pop_removeAnimationForKey:(NSString *)key;
        [Export ("pop_removeAnimationForKey:")]
        void RemoveAnimationForKey (string key);

        // -(NSArray *)pop_animationKeys;
        [Export ("pop_animationKeys")]
        string [] AnimationKeys ();

        // -(id)pop_animationForKey:(NSString *)key;
        [Export ("pop_animationForKey:")]
        POPAnimation AnimationForKey (string key);
    }

    // @interface POP (NSObject)
    [Category]
    [BaseType(typeof(NSObject))]
    interface NSObject_POP
    {
        // -(void)pop_addAnimation:(POPAnimation *)anim forKey:(NSString *)key;
        [Export("pop_addAnimation:forKey:")]
        void POPAddAnimation(POPAnimation anim, string key);

        // -(void)pop_removeAllAnimations;
        [Export("pop_removeAllAnimations")]
        void POPRemoveAllAnimations();

        // -(void)pop_removeAnimationForKey:(NSString *)key;
        [Export("pop_removeAnimationForKey:")]
        void POPRemoveAnimationForKey(string key);

        // -(NSArray *)pop_animationKeys;
        [Export("pop_animationKeys")]
        string[] POPAnimationKeys();

        // -(id)pop_animationForKey:(NSString *)key;
        [Export("pop_animationForKey:")]
        POPAnimation POPAnimationForKey(string key);
    }

    // @interface POPAnimationExtras (CAAnimation)
    [Category]
    [BaseType(typeof(CAAnimation))]
    interface CAAnimation_POPAnimationExtras
    {
        // -(void)pop_applyDragCoefficient;
        [Export("pop_applyDragCoefficient")]
        void POPApplyDragCoefficient();
    }

    // @interface POPPropertyAnimation : POPAnimation
    [BaseType (typeof (POPAnimation))]
    interface POPPropertyAnimation {

        // @property (nonatomic, strong) POPAnimatableProperty * property;
        [Export ("property", ArgumentSemantic.Strong)]
        POPAnimatableProperty Property { get; set; }

        // @property (copy, nonatomic) id fromValue;
        [Export ("fromValue", ArgumentSemantic.Copy)]
        NSObject FromValue { get; set; }

        // @property (copy, nonatomic) id toValue;
        [Export ("toValue", ArgumentSemantic.Copy)]
        NSObject ToValue { get; set; }

        // @property (assign, nonatomic) CGFloat roundingFactor;
        [Export ("roundingFactor", ArgumentSemantic.UnsafeUnretained)]
        nfloat RoundingFactor { get; set; }

        // @property (assign, nonatomic) NSUInteger clampMode;
        [Export ("clampMode", ArgumentSemantic.UnsafeUnretained)]
        POPAnimationClampFlags ClampMode { get; set; }

        // @property (assign, nonatomic, getter = isAdditive) BOOL additive;
        [Export ("additive", ArgumentSemantic.UnsafeUnretained)]
        bool Additive { [Bind ("isAdditive")] get; set; }
    }


    // @interface CustomProperty (POPPropertyAnimation)
    [Category]
    [BaseType(typeof(POPPropertyAnimation))]
    interface POPPropertyAnimation_CustomProperty
    {
        // +(instancetype)animationWithCustomPropertyNamed:(NSString *)name readBlock:(POPAnimatablePropertyReadBlock)readBlock writeBlock:(POPAnimatablePropertyWriteBlock)writeBlock;
        [Static]
        [Export("animationWithCustomPropertyNamed:readBlock:writeBlock:")]
        POPPropertyAnimation AnimationWithCustomPropertyNamed(string name, POPAnimatablePropertyReadBlock readBlock, POPAnimatablePropertyWriteBlock writeBlock);

        // +(instancetype)animationWithCustomPropertyReadBlock:(POPAnimatablePropertyReadBlock)readBlock writeBlock:(POPAnimatablePropertyWriteBlock)writeBlock;
        [Static]
        [Export("animationWithCustomPropertyReadBlock:writeBlock:")]
        POPPropertyAnimation AnimationWithCustomPropertyReadBlock(POPAnimatablePropertyReadBlock readBlock, POPAnimatablePropertyWriteBlock writeBlock);
    }

    // @interface POPSpringAnimation : POPPropertyAnimation
    [BaseType (typeof (POPPropertyAnimation))]
    interface POPSpringAnimation {

        // @property (copy, nonatomic) id velocity;
        [Export ("velocity", ArgumentSemantic.Copy)]
        NSObject Velocity { get; set; }

        // @property (assign, nonatomic) CGFloat springBounciness;
        [Export ("springBounciness", ArgumentSemantic.UnsafeUnretained)]
        nfloat SpringBounciness { get; set; }

        // @property (assign, nonatomic) CGFloat springSpeed;
        [Export ("springSpeed", ArgumentSemantic.UnsafeUnretained)]
        nfloat SpringSpeed { get; set; }

        // @property (assign, nonatomic) CGFloat dynamicsTension;
        [Export ("dynamicsTension", ArgumentSemantic.UnsafeUnretained)]
        nfloat DynamicsTension { get; set; }

        // @property (assign, nonatomic) CGFloat dynamicsFriction;
        [Export ("dynamicsFriction", ArgumentSemantic.UnsafeUnretained)]
        nfloat DynamicsFriction { get; set; }

        // @property (assign, nonatomic) CGFloat dynamicsMass;
        [Export ("dynamicsMass", ArgumentSemantic.UnsafeUnretained)]
        nfloat DynamicsMass { get; set; }

        // +(instancetype)animation;
        [Static, Export ("animation")]
        POPSpringAnimation Animation ();

        // +(instancetype)animationWithPropertyNamed:(NSString *)name;
        [Static, Export ("animationWithPropertyNamed:")]
        POPSpringAnimation AnimationWithPropertyNamed (string name);
    }

    //// @interface POPAnimationExtras (POPSpringAnimation)
    //[Category]
    //[BaseType(typeof(POPSpringAnimation))]
    //interface POPSpringAnimation_POPAnimationExtras
    //{
    //	// +(void)convertBounciness:(CGFloat)bounciness speed:(CGFloat)speed toTension:(CGFloat *)outTension friction:(CGFloat *)outFriction mass:(CGFloat *)outMass;
    //	[Static]
    //	[Export("convertBounciness:speed:toTension:friction:mass:")]
    //	unsafe void ConvertBounciness(nfloat bounciness, nfloat speed, nfloat* outTension, nfloat* outFriction, nfloat* outMass);

    //	// +(void)convertTension:(CGFloat)tension friction:(CGFloat)friction toBounciness:(CGFloat *)outBounciness speed:(CGFloat *)outSpeed;
    //	[Static]
    //	[Export("convertTension:friction:toBounciness:speed:")]
    //	unsafe void ConvertTension(nfloat tension, nfloat friction, nfloat* outBounciness, nfloat* outSpeed);
    //}

    // @interface POPAnimator : NSObject
    [BaseType (typeof (NSObject))]
    interface POPAnimator {

        // @property (nonatomic, weak) id<POPAnimatorDelegate> delegate;
        [Export ("delegate", ArgumentSemantic.Weak)]
        [NullAllowed]
        NSObject WeakDelegate { get; set; }

        // @property (nonatomic, weak) id<POPAnimatorDelegate> delegate;
        [Wrap ("WeakDelegate")]
        POPAnimatorDelegate Delegate { get; set; }

        // +(instancetype)sharedAnimator;
        [Static, Export ("sharedAnimator")]
        POPAnimator SharedAnimator ();

        // @property (readonly, nonatomic) CFTimeInterval refreshPeriod;
        [Export("refreshPeriod")]
        double RefreshPeriod { get; }
    }

    // @protocol POPAnimatorDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject))]
    interface POPAnimatorDelegate {

        // @required -(void)animatorWillAnimate:(POPAnimator *)animator;
        [Export ("animatorWillAnimate:")]
        [Abstract]
        void AnimatorWillAnimate (POPAnimator animator);

        // @required -(void)animatorDidAnimate:(POPAnimator *)animator;
        [Export ("animatorDidAnimate:")]
        [Abstract]
        void AnimatorDidAnimate (POPAnimator animator);
    }

    // @interface POPBasicAnimation : POPPropertyAnimation
    [BaseType (typeof (POPPropertyAnimation))]
    interface POPBasicAnimation {

        // @property (assign, nonatomic) CFTimeInterval duration;
        [Export ("duration", ArgumentSemantic.UnsafeUnretained)]
        double Duration { get; set; }

        // @property (nonatomic, strong) CAMediaTimingFunction * timingFunction;
        [Export ("timingFunction", ArgumentSemantic.Retain)]
        CAMediaTimingFunction TimingFunction { get; set; }

        // +(instancetype)animation;
        [Static, Export ("animation")]
        POPBasicAnimation Animation ();

        // +(instancetype)animationWithPropertyNamed:(NSString *)name;
        [Static, Export ("animationWithPropertyNamed:")]
        POPBasicAnimation AnimationWithPropertyNamed (string name);

        // +(instancetype)defaultAnimation;
        [Static, Export ("defaultAnimation")]
        POPBasicAnimation DefaultAnimation ();

        // +(instancetype)linearAnimation;
        [Static, Export ("linearAnimation")]
        POPBasicAnimation LinearAnimation ();

        // +(instancetype)easeInAnimation;
        [Static, Export ("easeInAnimation")]
        POPBasicAnimation EaseInAnimation ();

        // +(instancetype)easeOutAnimation;
        [Static, Export ("easeOutAnimation")]
        POPBasicAnimation EaseOutAnimation ();

        // +(instancetype)easeInEaseOutAnimation;
        [Static, Export ("easeInEaseOutAnimation")]
        POPBasicAnimation EaseInEaseOutAnimation ();
    }


    // typedef BOOL (^POPCustomAnimationBlock)(id, POPCustomAnimation *);
    delegate bool POPCustomAnimationBlock(NSObject obj, POPCustomAnimation anim);


    // @interface POPCustomAnimation : POPAnimation
    [BaseType (typeof (POPAnimation))]
    interface POPCustomAnimation {

        // @property (readonly, nonatomic) CFTimeInterval currentTime;
        [Export ("currentTime")]
        double CurrentTime { get; }

        // @property (readonly, nonatomic) CFTimeInterval elapsedTime;
        [Export ("elapsedTime")]
        double ElapsedTime { get; }

        // +(instancetype)animationWithBlock:(POPCustomAnimationBlock)block;
        [Static, Export ("animationWithBlock:")]
        POPCustomAnimation AnimationWithBlock (POPCustomAnimationBlock block);
    }

    // @interface POPDecayAnimation : POPPropertyAnimation
    [BaseType (typeof (POPPropertyAnimation))]
    interface POPDecayAnimation {

        // @property (copy, nonatomic) id velocity;
        [Export ("velocity", ArgumentSemantic.Copy)]
        NSObject Velocity { get; set; }

        // @property (readonly, copy, nonatomic) id originalVelocity;
        [Export ("originalVelocity", ArgumentSemantic.Copy)]
        NSObject OriginalVelocity { get; }

        // @property (assign, nonatomic) CGFloat deceleration;
        [Export ("deceleration", ArgumentSemantic.UnsafeUnretained)]
        nfloat Deceleration { get; set; }

        // @property (readonly, assign, nonatomic) CFTimeInterval duration;
        [Export ("duration", ArgumentSemantic.UnsafeUnretained)]
        double Duration { get; }

        // +(instancetype)animation;
        [Static, Export ("animation")]
        POPDecayAnimation Animation ();

        // +(instancetype)animationWithPropertyNamed:(NSString *)name;
        [Static, Export ("animationWithPropertyNamed:")]
        POPDecayAnimation AnimationWithPropertyNamed (string name);

        // -(id)reversedVelocity;
        [Export ("reversedVelocity")]
        NSObject ReversedVelocity ();
    }
}