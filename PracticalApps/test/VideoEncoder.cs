namespace test
{
    public class VideoEncoder
    {
        //public delegate void VideoEncodedEventHandler(object sender, EventArgs eventArgs);
        //public event VideoEncodedEventHandler VideoEncoded;
        //instead you can use EventHandler (built-in in .net)
        //public event EventHandler VideoEncoded;
        public event Action<Video> VideoEncoded;
        public void Encode(Video video)
        {
            System.Console.WriteLine("Starting encoding...");
            Thread.Sleep(3000);
            System.Console.WriteLine("Completed encoding!");
            OnVideoEncoded(video);
        }

        protected virtual void OnVideoEncoded(Video video)
        {
            if (VideoEncoded is not null)
            {
                //VideoEncoded(this, EventArgs.Empty);
                VideoEncoded(video);
            }
        }
    }
}