﻿namespace HotelContracts.BindingModels
{
	public class MailSendInfoBindingModel
	{
		public string MailAddress { get; set; } = string.Empty;
		public string Subject { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
	}
}
