Masonry is a light-weight layout framework which wraps AutoLayout with a nicer syntax.

Masonry has its own layout DSL which provides a chainable way of describing your
`NSLayoutConstraints` which results in layout code which is more concise and readable.

Under the hood Auto Layout is a powerful and flexible way of organising and laying out your views. 
However creating constraints from code is verbose and not very descriptive. 

## Masonry vs NSLayoutConstraints

Imagine a simple example in which you want to have a view fill its superview but inset by 
10 pixels on every side.

### Masonry

This is easy to do using basic syntax:

    UIEdgeInsets padding = new UIEdgeInsets(10, 10, 10, 10);
    
    view1.MakeConstraints(make => {
        make.Top.EqualTo(superview.Top()).Offset(padding.Top);
        make.Left.EqualTo(superview.Left()).Offset(padding.Left);
        make.Bottom.EqualTo(superview.Bottom()).Offset(-padding.Bottom);
        make.Right.EqualTo(superview.Right()).Offset(-padding.Right);
    });

There is also a shortcut / condensed version:

    UIEdgeInsets padding = new UIEdgeInsets(10, 10, 10, 10);
    
    view1.MakeConstraints(make => {
        make.Edges.EqualTo(superview).Insets(padding);
    });

### NSLayoutConstraints

However, the same set of constraints with `NSLayoutConstraints` is much more verbose:

    superview.AddConstraints(new [] {
        NSLayoutConstraint.Create(
			view1, NSLayoutAttribute.Top, 
			NSLayoutRelation.Equal, 
			superview, NSLayoutAttribute.Top,
			1.0f,
			padding),
        NSLayoutConstraint.Create(
			view1, NSLayoutAttribute.Top, 
			NSLayoutRelation.Equal, 
			superview, NSLayoutAttribute.Top,
			1.0f,
			padding),
        NSLayoutConstraint.Create(
			view1, NSLayoutAttribute.Top, 
			NSLayoutRelation.Equal, 
			superview, NSLayoutAttribute.Top,
			1.0f,
			padding),
        NSLayoutConstraint.Create(
			view1, NSLayoutAttribute.Top, 
			NSLayoutRelation.Equal, 
			superview, NSLayoutAttribute.Top,
			1.0f,
			padding),
    });

Even with such a simple example the code needed is quite verbose and quickly becomes unreadable when 
you have more than 2 or 3 views. Another option is to use Visual Format Language (VFL), which is a 
bit less long winded. However the ASCII type syntax has its own pitfalls and its also a bit harder 
to animate.

## References

You can hold on to a reference of a particular constraint by assigning the result of a constraint make
expression to a local variable or a class property. You could also reference multiple constraints by 
storing them away in an array:

	Constraint[] constraints;
	Constraint top;
	Constraint bottom;
	Constraint left;
	Constraint right;
	
    constraints = view1.MakeConstraints(make => {
        top = make.Top.EqualTo(superview.Top());
        left = make.Left.EqualTo(superview.Left());
        bottom = make.Bottom.EqualTo(superview.Bottom());
        right = make.Right.EqualTo(superview.Right());
    });
