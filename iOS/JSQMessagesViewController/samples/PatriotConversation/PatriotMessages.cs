using System;
using System.Collections.Generic;

namespace PatriotConversation
{
	class PatriotMessages
	{
		List<string> messages = new List<string> (new [] {
			"Let every nation know, whether it wishes us well or ill, that we shall pay any price, bear any burden, meet any hardship, support any friend, oppose any foe to assure the survival and the success of liberty.",
			"The tree of liberty must be refreshed from time to time with the blood of patriots and tyrants.",
			"America was not built on fear. America was built on courage, on imagination and an unbeatable determination to do the job at hand.",
			"If we ever forget that we are One Nation Under God, then we will be a nation gone under.",
			"The patriot volunteer, fighting for country and his rights, makes the most reliable soldier on earth.",
			"Wars may be fought with weapons, but they are won by men. It is the spirit of men who follow and of the man who leads that gains the victory.",
		});
		int messageIndex = 0;

		public string Pop ()
		{
			var msg = messages [messageIndex++];

			if (messageIndex >= messages.Count)
				messageIndex = 0;

			return msg;
		}
	}
}

