using Newtonsoft.Json;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Marathon.Models
{
	public class Message
	{
		[JsonProperty("message_id")]
		public int MessageId { get; set; }

		[JsonProperty("from")]
		public From From { get; set; }

		[JsonProperty("chat")]
		public Chat Chat { get; set; }

		[JsonProperty("date")]
		public long Date { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("reply_markup")]
		public InlineKeyboardMarkup ReplyMarkup { get; set; }

		[JsonProperty("reply_to_message")]
		public Message ReplyToMessage { get; set; }

		[JsonProperty("entities")]
		public IEnumerable<MessageEntity> Entities { get; set; }
	}
}
