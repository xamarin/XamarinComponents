using System;
using UIKit;
using Foundation;
using JSQMessagesViewController;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinChat
{
	public class User
	{
		public string Id { get; set;}
		public string DisplayName { get; set;}
	}

	public class ChatViewController : MessagesViewController
	{
		MessagesBubbleImage outgoingBubbleImageData, incomingBubbleImageData;
		List<Message> messages = new List<Message> ();

		User sender = new User {Id = "2CC8343", DisplayName = "Your Name Here"};
		User friend = new User {Id = "BADB229", DisplayName = "Tom Anderson"};

		MessageFactory messageFactory = new MessageFactory ();

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Xamarin Chat";

			// You must set your senderId and display name
			SenderId = sender.Id;
			SenderDisplayName = sender.DisplayName;

			// These MessagesBubbleImages will be used in the GetMessageBubbleImageData override
			var bubbleFactory = new MessagesBubbleImageFactory ();
			outgoingBubbleImageData = bubbleFactory.CreateOutgoingMessagesBubbleImage (UIColorExtensions.MessageBubbleLightGrayColor);
			incomingBubbleImageData = bubbleFactory.CreateIncomingMessagesBubbleImage (UIColorExtensions.MessageBubbleBlueColor);

			// Remove the AccessoryButton as we will not be sending pics
			InputToolbar.ContentView.LeftBarButtonItem = null;

			// Remove the Avatars
			CollectionView.CollectionViewLayout.IncomingAvatarViewSize = CoreGraphics.CGSize.Empty;
			CollectionView.CollectionViewLayout.OutgoingAvatarViewSize = CoreGraphics.CGSize.Empty;

			// Load some messagees to start
			messages.Add (new Message (sender.Id, sender.DisplayName, NSDate.DistantPast, "Hi There"));
			messages.Add (new Message (friend.Id, friend.DisplayName, NSDate.DistantPast, "I'm sorry, my responses are limited. You must ask the right questions."));
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath) 
		{
			var cell =  base.GetCell (collectionView, indexPath) as MessagesCollectionViewCell;

			// Override GetCell to make modifications to the cell
			// In this case darken the text for the sender
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

		public override async void PressedSendButton (UIButton button, string text, string senderId, string senderDisplayName, NSDate date)
		{
			SystemSoundPlayer.PlayMessageSentSound ();

			var message = new Message (SenderId, SenderDisplayName, NSDate.Now, text);
			messages.Add (message);

			FinishSendingMessage (true);

			await Task.Delay (500);

			await SimulateDelayedMessageReceived ();
		}

		async Task SimulateDelayedMessageReceived ()
		{
			ShowTypingIndicator = true;

			ScrollToBottom (true);

			var delay = System.Threading.Tasks.Task.Delay (1500);
			var message = await messageFactory.CreateMessageAsync (friend);
			await delay;

			messages.Add (message);

			ScrollToBottom (true);

			SystemSoundPlayer.PlayMessageReceivedSound ();

			FinishReceivingMessage (true);
		}

	}
}

