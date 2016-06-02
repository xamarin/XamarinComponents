To get started with a simple message view displaying only text you need to create a class that is a subclass of `MessagesViewController`, override 4 methods and set the `SenderId` and `SenderDisplayName` properties.

In the ViewDidLoad method the `SenderId` and `SenderDisplayName` properties should be set to determine which messages are incoming or outgoing.  Also create MessagesBubbleImages to be used in GetMessageBubbleImageData.  Finally in our example we will not display avatars therefore the `AvatarViewSize` properties are set to `Empty`.

``` csharp
public override void ViewDidLoad ()
{
	base.ViewDidLoad ();

	// You must set your senderId and display name
	SenderId = sender.Id;
	SenderDisplayName = sender.DisplayName;

	// These MessagesBubbleImages will be used in the GetMessageBubbleImageData override
	var bubbleFactory = new MessagesBubbleImageFactory ();
	outgoingBubbleImageData = bubbleFactory.CreateOutgoingMessagesBubbleImage (UIColorExtensions.MessageBubbleLightGrayColor);
	incomingBubbleImageData = bubbleFactory.CreateIncomingMessagesBubbleImage (UIColorExtensions.MessageBubbleBlueColor);

	// Remove the Avatars
	CollectionView.CollectionViewLayout.IncomingAvatarViewSize = CoreGraphics.CGSize.Empty;
	CollectionView.CollectionViewLayout.OutgoingAvatarViewSize = CoreGraphics.CGSize.Empty;
}

```

To specify the number of total cells to display override IUICollectionViewDataSource.GetItemsCount`.

``` csharp
public override nint GetItemsCount (UICollectionView collectionView, nint section)
{
	return 2;
}
```

To populate the view with messages, the message data that corresponds to the specified item at indexPath must be returned from `IMessagesCollectionViewDataSource.GetMessageData`.

``` csharp
public override IMessageData GetMessageData (MessagesCollectionView collectionView, NSIndexPath indexPath)
{
	if (indexPath.Row == 0)
		return Message.Create ("123", "Me", "Ping");

	return Message.Create ("456", "You", "Pong");
}
```

The `IMessagesCollectionViewDataSource.GetMessageBubbleImageData` method asks the data source for the message bubble image data that corresponds to the specified message data item at indexPath in the view.

``` csharp
public override IMessageBubbleImageDataSource GetMessageBubbleImageData (MessagesCollectionView collectionView, NSIndexPath indexPath)
{
	if (indexPath.Row == 0)
		return outgoingBubbleImageData;
	
	return incomingBubbleImageData;
}
```

Although the example does not display avatars, it is required to implement `IMessagesCollectionViewDataSource.GetAvatarImageData`.  Simply return null.

``` csharp
public override IMessageAvatarImageDataSource GetAvatarImageData (MessagesCollectionView collectionView, NSIndexPath indexPath)
{
	return null;
}
```

At this point the sample will run displaying the two messages, however the text for the sender is white on a light background.  To change text color override `IUICollectionViewDataSource.GetCell` and call the base class to get the MessagesCollectionViewCell.  Then change the `TextView.TextColor` to Black.

``` csharp
public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath) 
{
	var cell =  base.GetCell (collectionView, indexPath) as MessagesCollectionViewCell;

	// Override GetCell to make modifications to the cell
	// In this case darken the text for the sender
	if (indexPath.Row == 0)
		cell.TextView.TextColor = UIColor.Black;

	return cell;
}
```

For more information on the native library please visit JSQMessagesViewController's [website][1] or [GitHub repository.][2]
 
 [1]: http://www.jessesquires.com/JSQMessagesViewController/
 [2]: https://github.com/jessesquires/JSQMessagesViewController
