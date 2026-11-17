namespace Lab14_Task1
{
	internal class TechChannel
	{
		public string ChannelName { get; private set; } = null!;
		public event Action<string, string>? OnVideoUploaded;

		public TechChannel(string channelName)
		{
			ChannelName = channelName;
		}

		public void UploadVideo(string title)
		{
			Console.WriteLine($"\nКанал «{ChannelName}» опубликовал видео: «{title}»");
			OnVideoUploaded?.Invoke(ChannelName, title);
		}
	}
}