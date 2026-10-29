namespace ConsoleAppDotnetCore.DelegateDemo
{
    internal class DelegateDemo2
    {
        internal static void Run()
        {
            var photoProcessor = new PhotoProcessor();
            var filters = new PhotoFilter();
            PhotoProcessor.PhotoFilterHandler filterHandler = filters.ApplyBrightness;
            filterHandler += filters.Resize;
            filterHandler += ApplyRedColorFilter;

            photoProcessor.PhotoProcess("photo.jpg", filterHandler);
        }

        private static void ApplyRedColorFilter(Photo photo)
        {
            Console.WriteLine("Red Color Filter Applied");
        }

        internal class PhotoProcessor
        {
            public delegate void PhotoFilterHandler(Photo photo);
            internal void PhotoProcess(string path, PhotoFilterHandler filterHandler)
            {
                Photo photo = Photo.Load(path);
                filterHandler(photo);
                photo.Save();
            }
        }


        internal class PhotoFilter
        {
            internal void ApplyBrightness(Photo photo)
            {
                Console.WriteLine("Brightness Applied");
            }

            internal void ApplyContrast(Photo photo)
            {
                Console.WriteLine("Contrast Applied");
            }

            internal void Resize(Photo photo)
            {
                Console.WriteLine("Resize Applied");
            }
        }

        internal class Photo
        {
            internal static Photo Load(string path)
            {
                return new Photo();
            }

            internal void Save()
            {
                Console.WriteLine("Photo Saved");
            }
        }
    }
}
