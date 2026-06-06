namespace ConsoleAppDotnetCore.TypeSystem
{
    internal class PrimitiveTypesDemo
    {
        internal static void Run()
        {
            Console.WriteLine("Primitive Types Demo");
            Console.WriteLine("---------------");
            Console.WriteLine($"bool: {sizeof(bool)} bytes");
            Console.WriteLine($"byte: {sizeof(byte)} bytes");
            Console.WriteLine($"sbyte: {sizeof(sbyte)} bytes");
            Console.WriteLine($"short: {sizeof(short)} bytes");
            Console.WriteLine($"ushort: {sizeof(ushort)} bytes");
            Console.WriteLine($"int: {sizeof(int)} bytes");
            Console.WriteLine($"float: {sizeof(float)} bytes");
            Console.WriteLine($"long: {sizeof(long)} bytes");
            Console.WriteLine($"ulong: {sizeof(ulong)} bytes");
            Console.WriteLine($"double: {sizeof(double)} bytes");
            Console.WriteLine($"decimal: {sizeof(decimal)} bytes");
            Console.WriteLine($"char: {sizeof(char)} bytes");
            //Console.WriteLine($"string: {sizeof(string)} string");

            Console.WriteLine("\nDefault Values:");
            Console.WriteLine($"bool: {default(bool)}");
            Console.WriteLine($"byte: {default(byte)}");
            Console.WriteLine($"sbyte: {default(sbyte)}");
            Console.WriteLine($"short: {default(short)}");
            Console.WriteLine($"ushort: {default(ushort)}");
            Console.WriteLine($"int: {default(int)}");
            Console.WriteLine($"float: {default(float)}");
            Console.WriteLine($"long: {default(long)}");
            Console.WriteLine($"ulong: {default(ulong)}");
            Console.WriteLine($"double: {default(double)}");
            Console.WriteLine($"decimal: {default(decimal)}");
            Console.WriteLine($"string: '{default(string)}'");

            Console.WriteLine("\nRange of Values:");
            Console.WriteLine($"bool: {false} to {true}");
            Console.WriteLine($"byte: {byte.MinValue} to {byte.MaxValue}");
            Console.WriteLine($"sbyte: {sbyte.MinValue} to {sbyte.MaxValue}");
            Console.WriteLine($"short: {short.MinValue} to {short.MaxValue}");
            Console.WriteLine($"ushort: {ushort.MinValue} to {ushort.MaxValue}");
            Console.WriteLine($"int: {int.MinValue} to {int.MaxValue}");
            Console.WriteLine($"float: {float.MinValue} to {float.MaxValue}");
            Console.WriteLine($"long: {long.MinValue} to {long.MaxValue}");
            Console.WriteLine($"ulong: {ulong.MinValue} to {ulong.MaxValue}");
            Console.WriteLine($"double: {double.MinValue} to {double.MaxValue}");
            Console.WriteLine($"decimal: {decimal.MinValue} to {decimal.MaxValue}");
            Console.WriteLine($"char: '\\u0000' to '\\uffff'");
            //Console.WriteLine($"string: {string.MinValue} to {string.MaxValue}");

            Console.WriteLine("\nType Codes:");
            Console.WriteLine($"bool: {Type.GetTypeCode(typeof(bool))}");
            Console.WriteLine($"byte: {Type.GetTypeCode(typeof(byte))}");
            Console.WriteLine($"sbyte: {Type.GetTypeCode(typeof(sbyte))}");
            Console.WriteLine($"short: {Type.GetTypeCode(typeof(short))}");
            Console.WriteLine($"ushort: {Type.GetTypeCode(typeof(ushort))}");
            Console.WriteLine($"int: {Type.GetTypeCode(typeof(int))}");
            Console.WriteLine($"float: {Type.GetTypeCode(typeof(float))}");
            Console.WriteLine($"long: {Type.GetTypeCode(typeof(long))}");
            Console.WriteLine($"ulong: {Type.GetTypeCode(typeof(ulong))}");
            Console.WriteLine($"double: {Type.GetTypeCode(typeof(double))}");
            Console.WriteLine($"decimal: {Type.GetTypeCode(typeof(decimal))}");
            Console.WriteLine($"char: {Type.GetTypeCode(typeof(char))}");
            Console.WriteLine($"string: {Type.GetTypeCode(typeof(string))}");
        }
    }
}
