namespace test
{
    public class UsefulService
    {
        /*public void VideoEncodedProcessor(object sender, EventArgs eventArgs)
        {
            System.Console.WriteLine(" processing completion notification");
        }*/

        public void VideoEncodedProcessor(Video video)
        {
            System.Console.WriteLine(video.Title + " processing completion notification");
        }
    }
}