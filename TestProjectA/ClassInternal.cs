namespace TestProjectA
{
    internal class ClassInternal
    {
        internal void PrintA()
        {
            Console.WriteLine("TestProjectA -> ClassInternal -> PrintA");
        }

        protected internal void PrintB()
        {
            Console.WriteLine("TestProjectA -> ClassInternal -> PrintB");
        }
    }
}
