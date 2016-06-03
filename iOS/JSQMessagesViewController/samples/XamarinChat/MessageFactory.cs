using System;
using System.Collections.Generic;
using JSQMessagesViewController;
using System.Threading.Tasks;
using System.Net;
using UIKit;
using Foundation;
using CoreGraphics;
using ImageIO;

namespace XamarinChat
{
	public class MessageFactory
	{
		List<string> messages = new List<string> (new [] {
			"An American monkey, after getting drunk on brandy, would never touch it again, and thus is much wiser than most men.",
			"Just cause you got the monkey off your back doesn't mean the circus has left town.",
			"Never hold discussions with the monkey when the organ grinder is in the room.",
			"If you had a million Shakespeares, could they write like a monkey?",
			"I learned the way a monkey learns - by watching its parents.",
			"If you pay peanuts, you get monkeys.",
			"When you're dealing with monkeys, you've got to expect some wrenches.",
		});

		int messageIndex = 0;

		public async Task <Message> CreateMessageAsync (User user)
		{
			var rnd = new Random ();
			var choice = rnd.Next () % 3;

			if (choice == 0)
				return CreateMonkeyMessage (user);

			return await CreateOverflowMessageAsync (choice == 2 ? "dogoverflow.com" : "catoverflow.com", user);
		}

		Message CreateMonkeyMessage (User user)
		{
			var msg = messages [messageIndex++];

			if (messageIndex >= messages.Count)
				messageIndex = 0;

			return Message.Create (user.Id, user.DisplayName, msg);
		}

		async Task<Message> CreateOverflowMessageAsync (string source, User user)
		{
			WebClient client = new WebClient ();
			var catUrl = await client.DownloadStringTaskAsync (
				new Uri (string.Format ("http://{0}/api/query?limit=1&order=random", source)));
			
			var data = await client.DownloadDataTaskAsync (catUrl);

			var image = CreateAnimatedImage (data);


			var photoItem = new PhotoMediaItem (image);
			photoItem.AppliesMediaViewMaskAsOutgoing = false;
			return Message.Create (user.Id, user.DisplayName, photoItem);
		}
			
		UIImage CreateAnimatedImage (byte [] data)
		{
			var imagesOut = new List<UIImage> ();

			var image = CGImageSource.FromData (NSData.FromArray (data));
			var numImages = image.ImageCount;

			for (int i = 0; i < numImages; i++) {
				imagesOut.Add (new UIImage (image.CreateImage (i, null)));
			}

			return UIImage.CreateAnimatedImage (imagesOut.ToArray (), numImages * .1);
		}
	}
}

