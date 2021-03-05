namespace Marathon.Extensions
{
	public static class StringExtensions
	{
		public static int ToInt(this string source)
		{
			if (int.TryParse(source, out var number))
			{
				return number;
			}

			return -1;
		}
	}
}
