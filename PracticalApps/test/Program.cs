using test;

Video video1 = new() { Title = "Test Video 1" };
VideoEncoder ve = new();
UsefulService service = new();
ve.VideoEncoded += service.VideoEncodedProcessor;
ve.Encode(video1);
System.Console.ReadLine();