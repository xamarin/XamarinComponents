using System;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using UIKit;
using GLKit;
using Metal;
using CoreML;
using MapKit;
using Photos;
using ModelIO;
using SceneKit;
using Contacts;
using Security;
using Messages;
using AudioUnit;
using CoreVideo;
using CoreMedia;
using QuickLook;
using CoreImage;
using SpriteKit;
using Foundation;
using CoreMotion;
using ObjCRuntime;
using AddressBook;
using MediaPlayer;
using GameplayKit;
using CoreGraphics;
using CoreLocation;
using AVFoundation;
using NewsstandKit;
using FileProvider;
using CoreAnimation;
using CoreFoundation;
using NetworkExtension;

namespace MaterialComponents {
	[Register("MDCContainerScheme", true)]
	public unsafe partial class ContainerScheme : NSObject, IContainerScheming {
		
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		static readonly IntPtr class_ptr = Class.GetHandle ("MDCContainerScheme");
		
		public override IntPtr ClassHandle { get { return class_ptr; } }
		
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("init")]
		public ContainerScheme () : base (NSObjectFlag.Empty)
		{
			IsDirectBinding = GetType ().Assembly == global::ApiDefinition.Messaging.this_assembly;
			if (IsDirectBinding) {
				InitializeHandle (global::ApiDefinition.Messaging.IntPtr_objc_msgSend (this.Handle, global::ObjCRuntime.Selector.GetHandle ("init")), "init");
			} else {
				InitializeHandle (global::ApiDefinition.Messaging.IntPtr_objc_msgSendSuper (this.SuperHandle, global::ObjCRuntime.Selector.GetHandle ("init")), "init");
			}
		}

		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		protected ContainerScheme (NSObjectFlag t) : base (t)
		{
			IsDirectBinding = GetType ().Assembly == global::ApiDefinition.Messaging.this_assembly;
		}

		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		protected internal ContainerScheme (IntPtr handle) : base (handle)
		{
			IsDirectBinding = GetType ().Assembly == global::ApiDefinition.Messaging.this_assembly;
		}


		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		object __mt_ColorScheme_var;
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual SemanticColorScheme ColorScheme
		{
			[Export("colorScheme", ArgumentSemantic.UnsafeUnretained)]
			get
			{
				SemanticColorScheme ret;
				if (IsDirectBinding)
				{
					ret = Runtime.GetNSObject<SemanticColorScheme>(global::ApiDefinition.Messaging.IntPtr_objc_msgSend(this.Handle, Selector.GetHandle("colorScheme")));
				}
				else
				{
					ret = Runtime.GetNSObject<SemanticColorScheme>(global::ApiDefinition.Messaging.IntPtr_objc_msgSendSuper(this.SuperHandle, Selector.GetHandle("colorScheme")));
				}
				MarkDirty();
				__mt_ColorScheme_var = ret;
				return ret;
			}

			[Export("setColorScheme:", ArgumentSemantic.UnsafeUnretained)]
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (IsDirectBinding)
				{
					global::ApiDefinition.Messaging.void_objc_msgSend_IntPtr(this.Handle, Selector.GetHandle("setColorScheme:"), value.Handle);
				}
				else
				{
					global::ApiDefinition.Messaging.void_objc_msgSendSuper_IntPtr(this.SuperHandle, Selector.GetHandle("setColorScheme:"), value.Handle);
				}
				MarkDirty();
				__mt_ColorScheme_var = value;
			}
		}

		ColorScheming IContainerScheming.ColorScheme => Runtime.GetNSObject<ColorScheming>(this.ColorScheme.Handle, false);

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		object __mt_ShapeScheme_var;
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual ShapeScheme ShapeScheme
		{
			[Export("shapeScheme", ArgumentSemantic.UnsafeUnretained)]
			get
			{
				ShapeScheme ret;
				if (IsDirectBinding)
				{
					ret = Runtime.GetNSObject<ShapeScheme>(global::ApiDefinition.Messaging.IntPtr_objc_msgSend(this.Handle, Selector.GetHandle("shapeScheme")));
				}
				else
				{
					ret = Runtime.GetNSObject<ShapeScheme>(global::ApiDefinition.Messaging.IntPtr_objc_msgSendSuper(this.SuperHandle, Selector.GetHandle("shapeScheme")));
				}
				MarkDirty();
				__mt_ShapeScheme_var = ret;
				return ret;
			}

			[Export("setShapeScheme:", ArgumentSemantic.UnsafeUnretained)]
			set
			{
				if (IsDirectBinding)
				{
					global::ApiDefinition.Messaging.void_objc_msgSend_IntPtr(this.Handle, Selector.GetHandle("setShapeScheme:"), value == null ? IntPtr.Zero : value.Handle);
				}
				else
				{
					global::ApiDefinition.Messaging.void_objc_msgSendSuper_IntPtr(this.SuperHandle, Selector.GetHandle("setShapeScheme:"), value == null ? IntPtr.Zero : value.Handle);
				}
				MarkDirty();
				__mt_ShapeScheme_var = value;
			}
		}

		ShapeScheming IContainerScheming.ShapeScheme => Runtime.GetNSObject<ShapeScheming>(this.ShapeScheme.Handle, false);


		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		object __mt_TypographyScheme_var;
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual TypographyScheme TypographyScheme
		{
			[Export("typographyScheme", ArgumentSemantic.UnsafeUnretained)]
			get
			{
				TypographyScheme ret;
				if (IsDirectBinding)
				{
					ret = Runtime.GetNSObject<TypographyScheme>(global::ApiDefinition.Messaging.IntPtr_objc_msgSend(this.Handle, Selector.GetHandle("typographyScheme")));
				}
				else
				{
					ret = Runtime.GetNSObject<TypographyScheme>(global::ApiDefinition.Messaging.IntPtr_objc_msgSendSuper(this.SuperHandle, Selector.GetHandle("typographyScheme")));
				}
				MarkDirty();
				__mt_TypographyScheme_var = ret;
				return ret;
			}

			[Export("setTypographyScheme:", ArgumentSemantic.UnsafeUnretained)]
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (IsDirectBinding)
				{
					global::ApiDefinition.Messaging.void_objc_msgSend_IntPtr(this.Handle, Selector.GetHandle("setTypographyScheme:"), value.Handle);
				}
				else
				{
					global::ApiDefinition.Messaging.void_objc_msgSendSuper_IntPtr(this.SuperHandle, Selector.GetHandle("setTypographyScheme:"), value.Handle);
				}
				MarkDirty();
				__mt_TypographyScheme_var = value;
			}
		}

		TypographyScheming IContainerScheming.TypographyScheme => Runtime.GetNSObject<TypographyScheming>(this.TypographyScheme.Handle, false);

	} /* class ContainerScheme */
}
