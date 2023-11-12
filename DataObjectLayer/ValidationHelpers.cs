namespace DataObjects
{
	public static class ValidationHelpers
	{
		// validate email endwith .com and @ symbol
		//email length greater than 13 and less than 100
		public static bool IsValidEmail(this string email)
		{
			bool isValid = false;
			// email has at least 13 characters
			if (email.Length >= 13)
			{
				// email has at most 100 characters
				if (email.Length <= 100)
				{
					// email contains @ symbol
					if (email.Contains("@"))
					{
						// email ends with .com
						if (email.EndsWith(".com"))
						{
							isValid = true;
						}
					}
				}
			}
			return isValid;
		}
		// validate password
		public static bool IsValidPassword(this string password)
		{
			bool isValid = false;
			// password has at least 8 characters
			if (password.Length >= 8)
			{
				isValid = true;
			}
			return isValid;

		}
	}
}
