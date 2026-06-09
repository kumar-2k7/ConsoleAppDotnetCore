namespace ConsoleAppDotnetCore.EventDemo
{
    /// <summary>
    /// In C#, an event is a mechanism that enables a class 
    /// (the Publisher) to notify other classes (the Subscribers)
    /// when something important happens.
    /// </summary>
    internal class EventDemoRun
    {
        public static void Run()
        {
            var encoder = new VideoEncoder(); // Publisher
            var mailService = new MailService(); // Subscriber

            // Subscribe to the event using the += operator
            encoder.VideoEncoded += mailService.OnVideoEncoded;

            // Start the process
            encoder.Encode("C# Tutorial Video");
        }

        /// <summary>
        /// Step 1: Define a class to hold custom event data inheriting from EventArgs
        /// </summary>
        internal class VideoEventArgs : EventArgs
        {
            public string Title { get; set; } = string.Empty;
        }

        /// <summary>
        /// Step 2: Create the Publisher class
        /// </summary>
        internal class VideoEncoder
        {
            // Declare the event using the standard generic EventHandler
            public event EventHandler<VideoEventArgs>? VideoEncoded;

            public void Encode(string videoTitle)
            {
                Console.WriteLine($"Encoding video: {videoTitle}...");
                System.Threading.Thread.Sleep(5000); // Simulate work

                // Raise the event when the work is finished
                OnVideoEncoded(videoTitle);
            }

            protected virtual void OnVideoEncoded(string title)
            {
                // The ?. operator ensures it only triggers if there are subscribers
                VideoEncoded?.Invoke(this, new VideoEventArgs { Title = title });
            }
        }

        /// <summary>
        /// Step 3: Create the Subscriber class
        /// </summary>
        internal class MailService
        {
            /// <summary>
            /// This is the Event Handler method (matches the EventHandler signature)
            /// </summary>
            /// <param name="source"></param>
            /// <param name="e"></param>
            public void OnVideoEncoded(object? source, VideoEventArgs e)
            {
                Console.WriteLine($"MailService: Sending email notification for '{e.Title}'...");
            }
        }
    }
}
