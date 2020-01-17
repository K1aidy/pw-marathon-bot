namespace Marathon.Models.Dto
{
	/// <summary>
	/// Результат добавления нового аккаунта.
	/// </summary>
	public class AddAccountResult
	{
		/// <summary>
		/// Нужно ли отправлять номер из СМС.
		/// </summary>
		public bool UseTwoFactorAuthentication { get; set; }

		/// <summary>
		/// Id аккаунта, если добавлен.
		/// </summary>
		public int? AccountId { get; set; }

		/// <summary>
		/// Id запроса, к которому нужно приложить код из СМС.
		/// </summary>
		public int? RequestId { get; set; }

		/// <summary>
		/// Наличие ошибок.
		/// </summary>
		public bool HasError { get; set; }

		/// <summary>
		/// Описание ошибок.
		/// </summary>
		public string ErrorDescription { get; set; }
	}
}
