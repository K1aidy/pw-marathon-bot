using Newtonsoft.Json;
using Telegram.Bot.Types.Enums;

namespace Marathon.Models
{
	public class UpdateModel
	{
		[JsonProperty("update_id")]
		public long UpdateId { get; set; }

		[JsonProperty("message")]
		public Message Message { get; set; }

		[JsonProperty("callback_query")]
		public CallBack CallBack { get; set; }

		public UpdateType Type
		{
			get
			{
				if (Message != null) return UpdateType.Message;
				if (CallBack != null) return UpdateType.CallbackQuery;
				return UpdateType.Unknown;
			}
		}
	}
}
