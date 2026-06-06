namespace ConsoleAppDotnetCore.TypeSystem
{
    internal class ReferenceTypeDemo
    {
        internal static void Run()
        {
            Calculator calculator = new Calculator();

            Console.WriteLine("Primitive Types Demo");
            Console.WriteLine("---------------");
            //Console.WriteLine($"Point3D: {sizeof(Point3D)} bytes");
            //Console.WriteLine($"Point3DStruct: {sizeof(Point3DStruct)} bytes");
            //Console.WriteLine($"Point3DClass: {sizeof(Point3DClass)} bytes");

            Console.WriteLine("\nDefault Values:");
            Console.WriteLine($"Point3D: {default(Point3D)}");
            Console.WriteLine($"Point3DStruct: {default(Point3DStruct)}");
            Console.WriteLine($"Point3DClass: {default(Point3DClass)}");

            Console.WriteLine("\nRange of Values:");
            Console.WriteLine($"Calculator: {Colors.Red} to {Colors.Blue}");

            Console.WriteLine("\nType Codes:");
            Console.WriteLine($"Point3D: {Type.GetTypeCode(typeof(Point3D))}");
            Console.WriteLine($"Point3DStruct: {Type.GetTypeCode(typeof(Point3DStruct))}");
            Console.WriteLine($"Point3DClass: {Type.GetTypeCode(typeof(Point3DClass))}");
        }

        private class Calculator
        {
            public int Number1 { get; set; } = default!;
            public int Number2 { get; set; } = default!;
        }

        struct Point
        {
            private int _X { get; set; }
            private int _Y { get; set; }
            private int _Z { get; set; }

            public Point(int x, int y, int z)
            {
                _X = x;
                _Y = y;
                _Z = z;
            }

            public int X { get { return _X; } }
            public int Y { get { return _Y; } }
            public int Z { get { return _Z; } }
        }

        enum Colors
        {
            Red,
            Green,
            Blue
        }

        record Point3D(int X, float Y, long Z);
        record struct Point3DStruct(int X, float Y, long Z);
        record class Point3DClass(int X, float Y, long Z);

        interface IEnumerable
        {
            IEnumerable GetEnumerator();
        }

        interface IEnumerator
        {
            bool MoveNext();
            object Current { get; }
            void Reset();
        }


    }
}
