namespace ConsoleAppDotnetCore.AdvanceTopics.TemplateMethodDemo
{
    internal class TemplateMethodDemoRun
    {
        internal static void Run()
        {
            Console.WriteLine("Excel Data Export");
            DataExporter data = new ExcelExporter();
            data.ExportData();

            Console.WriteLine("\n\nJSON Data Export");
            data = new JsonExporter();
            data.ExportData();
        }

        public abstract class DataExporter
        {
            // The Template Method: Final to prevent subclasses from changing the execution order
            public void ExportData()
            {
                ReadData();
                FormatData();
                SaveData();
            }

            // Concrete step: Same for all subclasses
            private void ReadData() => Console.WriteLine("Reading data from central source...");

            // Abstract steps: Subclasses MUST implement these variations
            protected abstract void FormatData();
            protected abstract void SaveData();
        }

        public class ExcelExporter : DataExporter
        {
            protected override void FormatData() => Console.WriteLine("Formatting data into Excel rows and columns.");
            protected override void SaveData() => Console.WriteLine("Saving file to .xlsx format.");
        }

        public class JsonExporter : DataExporter
        {
            protected override void FormatData() => Console.WriteLine("Formatting data into JSON key-value strings.");
            protected override void SaveData() => Console.WriteLine("Saving file to .json format.");
        }

    }
}
