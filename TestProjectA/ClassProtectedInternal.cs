namespace TestProjectA
{
    internal class ClassProtectedInternal
    {
        internal void PrintA()
        {
            Console.WriteLine("TestProjectA -> ClassProtectedInternal -> PrintA");
        }

        protected internal void PrintB()
        {
            Console.WriteLine("TestProjectA -> ClassProtectedInternal -> PrintB");
        }
    }
}
