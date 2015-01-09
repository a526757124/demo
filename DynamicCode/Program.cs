using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DynamicCode
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic dynamicObj = new ExpandoObject();
            dynamicObj.i = 100;
            dynamicObj.Increment = new Action(() => dynamicObj.i++);
            //dynamicObj.Incrmement();
            Console.WriteLine(dynamicObj.i);


            AssemblyName assemblyName = new AssemblyName("EmitTest.TestProvider");

            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MvcAdviceProvider");

            TypeBuilder typeBuilder = moduleBuilder.DefineType("EmitTest.TestClass", TypeAttributes.Public,
                                                            typeof(object), new Type[] { });
            
            PropertyBuilder properBuilder = typeBuilder.DefineProperty("Name", PropertyAttributes.HasDefault, typeof(string), new Type[] { typeof(string) });

            MethodBuilder methodBuilder = typeBuilder.DefineMethod("GetName", MethodAttributes.Public, typeof(string), new Type[] { });

            ILGenerator generator=methodBuilder.GetILGenerator();
            
            TestType(typeBuilder);

            Console.Read();
        }
        private static void TestType(TypeBuilder typeBuilder)
        {
            Console.WriteLine("FullName:" + typeBuilder.Assembly.FullName);
            Console.WriteLine("ModuleName:" + typeBuilder.Module.Name);
            Console.WriteLine("NamespaceName:" + typeBuilder.Namespace);
            Console.WriteLine("Name:" + typeBuilder.Name);
        }
    }
}
