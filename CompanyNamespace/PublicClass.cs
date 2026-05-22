namespace CompanyNamespace
{
    public class PublicClass
    {
        public void PublicMethod()
        {
            Console.WriteLine("Public method can be accessed from anywhere.");
        }

        private void PrivateMethod()
        {
            Console.WriteLine("Private method can only be accessed within the same class.");
        }

        protected void ProtectedMethod()
        {
            Console.WriteLine("Protected method can be accessed within the same class and by derived classes.");
        }

        internal void InternalMethod()
        {
            Console.WriteLine("Internal method can be accessed from any code in the same assembly.");
        }
    }
}
