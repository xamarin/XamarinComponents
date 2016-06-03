using System;
using UIKit;
using Foundation;
using JSQMessagesViewController;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatriotConversation
{
	public class DemoMessagesViewController : MessagesViewController
	{
		List<Message> messages = new List<Message> ();
		PatriotMessages patriotMessages = new PatriotMessages ();

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var bubbleFactory = new MessagesBubbleImageFactory ();

			outgoingBubbleImageData = bubbleFactory.CreateOutgoingMessagesBubbleImage (UIColor.FromRGB (0xE9, 0xE4, 0xD9));
			incomingBubbleImageData = bubbleFactory.CreateIncomingMessagesBubbleImage (UIColor.FromRGB (0x7B, 0xBA, 0xE4));

			messages.Add (new Message (DemoConstants.DemoAvatarIdJFK, DemoConstants.DemoAvatarDisplayNameJFK, NSDate.DistantPast, "Is this line secure?"));
			messages.Add (new Message (DemoConstants.DemoAvatarIdPatton, DemoConstants.DemoAvatarDisplayNamePatton, NSDate.DistantPast, "Sure Thing!"));

			Title = @"Patriot Conversation";


		    // You MUST set your senderId and display name
			SenderId = DemoConstants.DemoAvatarIdJFK;
			SenderDisplayName = DemoConstants.DemoAvatarDisplayNameJFK;



		    // Load up our fake data for the demo
			CollectionView.CollectionViewLayout.IncomingAvatarViewSize = CoreGraphics.CGSize.Empty;
			CollectionView.CollectionViewLayout.OutgoingAvatarViewSize = CoreGraphics.CGSize.Empty;

			ShowLoadEarlierMessagesHeader = false;

			NavigationItem.RightBarButtonItem = new UIBarButtonItem (
				bubbleImageFromBundleWithName ("typing"), UIBarButtonItemStyle.Bordered, ReceiveMessagePressed);
		}

		MessagesBubbleImage outgoingBubbleImageData, incomingBubbleImageData;

		async void ReceiveMessagePressed (object sender, EventArgs args)
		{
			await SimulateDelayedMessageReceived (patriotMessages.Pop ());
		}

		async Task SimulateDelayedMessageReceived (string message)
		{
			ShowTypingIndicator = true;

			ScrollToBottom (true);

			await System.Threading.Tasks.Task.Delay (1500);

			messages.Add (new Message (DemoConstants.DemoAvatarIdPatton, DemoConstants.DemoAvatarDisplayNamePatton, NSDate.DistantPast, message));

			ScrollToBottom (true);

			SystemSoundPlayer.PlayMessageReceivedSound ();

			FinishReceivingMessage (true);
		}

		public override async void PressedSendButton (UIButton button, string text, string senderId, string senderDisplayName, NSDate date)
		{
			SystemSoundPlayer.PlayMessageSentSound ();

			var message = new Message (SenderId, SenderDisplayName, NSDate.Now, text);
			messages.Add (message);

			FinishSendingMessage (true);

			await System.Threading.Tasks.Task.Delay (500);

			await SimulateDelayedMessageReceived (patriotMessages.Pop ());
		}

		UIImage bubbleImageFromBundleWithName (string name)
		{
			var bundle = NSBundle.FromClass (new ObjCRuntime.Class (typeof(MessagesViewController)));

			string bundleResourcePath = bundle.ResourcePath;
			string assetPath = System.IO.Path.Combine (bundleResourcePath, "JSQMessagesAssets.bundle");
			var messagesAssetBundle = NSBundle.FromPath (assetPath);


			var path = messagesAssetBundle.PathForResource (name, "png", "Images");
			return UIImage.FromFile (path);
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath) 
		{
			var cell =  base.GetCell (collectionView, indexPath) as MessagesCollectionViewCell;

			var message = messages [indexPath.Row];
			if (message.SenderId == SenderId)
				cell.TextView.TextColor = UIColor.Black;

			return cell;
		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return messages.Count;
		}

		public override IMessageData GetMessageData (MessagesCollectionView collectionView, NSIndexPath indexPath)
		{
			return messages [indexPath.Row];

		}

		public override IMessageBubbleImageDataSource GetMessageBubbleImageData (MessagesCollectionView collectionView, NSIndexPath indexPath)
		{
			var message = messages [indexPath.Row];
			if (message.SenderId == SenderId)
				return outgoingBubbleImageData;
			return incomingBubbleImageData;

		}

		public override IMessageAvatarImageDataSource GetAvatarImageData (MessagesCollectionView collectionView, NSIndexPath indexPath)
		{
			return null;
		}

	}
}

