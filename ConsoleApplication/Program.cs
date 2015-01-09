using EmitMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Gma.FenCi;
using System.IO;
namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
           

            DateTime dt3 = DateTime.Now;
            var chineseParse=new ChineseParse();
            Console.WriteLine("初使化时间：{0}",DateTime.Now - dt3);
            DateTime dt = DateTime.Now;
            string[] str = chineseParse.getParse(@"目前授信业务用户基础逐渐增大，但是用户授信风险控制来源于对用户数据及行为分析和监控。鉴于风控部门目前授信情况，特对控台系统提出以下需求，望参考并给与处理。谢谢！");
            foreach (var item in str)
            {
            }
            Console.WriteLine("分词使用时间：{0}", DateTime.Now - dt);

            DateTime dt1 = DateTime.Now;
            string[] str1 = chineseParse.getParse(@" 再来说一下make install下CMake是如何处理RPATH的。CMake为了方便用户的安装，默认在make install之后会自动remove删除掉相关的RPATH,这个时候你再去查看exe的RPATH，已经发现没有这个字段了。因此，当每次make install之后，我们进入到安装路径下执行相关exe的时候，就会发现此时的exe已经找不到相关的库路径了，因为它的RPATH已经被CMake给去除了。

    那么，如何让CMake能够在install的过程中写入相关RPATH并且该RPATH不能使当初build的时候的RPATH呢？答案就是CMAKE_INSTALL_RPATH这个全局变量和INSTALL_RPATH这个target属性。下面举一下简单的例子。

    大家都知道，CMake在安装的过程会有一个和configure一样的安装路径，CMAKE_INSTALL_PREFIX（configure下是--prefix,当然也可以用shell下的全局变量DESTDIR）,这个时候它会把你的安装文件安装到你prefix下的相对路径下，因此当我们希望在make install的时候，比如当前的share_lib在lib目录下，我们希望安装之后的RPATH可以自动找到它，我们就可以这么写再来说一下make install下CMake是如何处理RPATH的。CMake为了方便用户的安装，默认在make install之后会自动remove删除掉相关的RPATH,这个时候你再去查看exe的RPATH，已经发现没有这个字段了。因此，当每次make install之后，我们进入到安装路径下执行相关exe的时候，就会发现此时的exe已经找不到相关的库路径了，因为它的RPATH已经被CMake给去除了。

    那么，如何让CMake能够在install的过程中写入相关RPATH并且该RPATH不能使当初build的时候的RPATH呢？答案就是CMAKE_INSTALL_RPATH这个全局变量和INSTALL_RPATH这个target属性。下面举一下简单的例子。

    大家都知道，CMake在安装的过程会有一个和configure一样的安装路径，CMAKE_INSTALL_PREFIX（configure下是--prefix,当然也可以用shell下的全局变量DESTDIR）,这个时候它会把你的安装文件安装到你prefix下的相对路径下，因此当我们希望在make install的时候，比如当前的share_lib在lib目录下，我们希望安装之后的RPATH可以自动找到它，我们就可以这么写再来说一下make install下CMake是如何处理RPATH的。CMake为了方便用户的安装，默认在make install之后会自动remove删除掉相关的RPATH,这个时候你再去查看exe的RPATH，已经发现没有这个字段了。因此，当每次make install之后，我们进入到安装路径下执行相关exe的时候，就会发现此时的exe已经找不到相关的库路径了，因为它的RPATH已经被CMake给去除了。

    那么，如何让CMake能够在install的过程中写入相关RPATH并且该RPATH不能使当初build的时候的RPATH呢？答案就是CMAKE_INSTALL_RPATH这个全局变量和INSTALL_RPATH这个target属性。下面举一下简单的例子。

    大家都知道，CMake在安装的过程会有一个和configure一样的安装路径，CMAKE_INSTALL_PREFIX（configure下是--prefix,当然也可以用shell下的全局变量DESTDIR）,这个时候它会把你的安装文件安装到你prefix下的相对路径下，因此当我们希望在make install的时候，比如当前的share_lib在lib目录下，我们希望安装之后的RPATH可以自动找到它，我们就可以这么写再来说一下make install下CMake是如何处理RPATH的。CMake为了方便用户的安装，默认在make install之后会自动remove删除掉相关的RPATH,这个时候你再去查看exe的RPATH，已经发现没有这个字段了。因此，当每次make install之后，我们进入到安装路径下执行相关exe的时候，就会发现此时的exe已经找不到相关的库路径了，因为它的RPATH已经被CMake给去除了。

    那么，如何让CMake能够在install的过程中写入相关RPATH并且该RPATH不能使当初build的时候的RPATH呢？答案就是CMAKE_INSTALL_RPATH这个全局变量和INSTALL_RPATH这个target属性。下面举一下简单的例子。

    大家都知道，CMake在安装的过程会有一个和configure一样的安装路径，CMAKE_INSTALL_PREFIX（configure下是--prefix,当然也可以用shell下的全局变量DESTDIR）,这个时候它会把你的安装文件安装到你prefix下的相对路径下，因此当我们希望在make install的时候，比如当前的share_lib在lib目录下，我们希望安装之后的RPATH可以自动找到它，我们就可以这么写再来说一下make install下CMake是如何处理RPATH的。CMake为了方便用户的安装，默认在make install之后会自动remove删除掉相关的RPATH,这个时候你再去查看exe的RPATH，已经发现没有这个字段了。因此，当每次make install之后，我们进入到安装路径下执行相关exe的时候，就会发现此时的exe已经找不到相关的库路径了，因为它的RPATH已经被CMake给去除了。

    那么，如何让CMake能够在install的过程中写入相关RPATH并且该RPATH不能使当初build的时候的RPATH呢？答案就是CMAKE_INSTALL_RPATH这个全局变量和INSTALL_RPATH这个target属性。下面举一下简单的例子。

    大家都知道，CMake在安装的过程会有一个和configure一样的安装路径，CMAKE_INSTALL_PREFIX（configure下是--prefix,当然也可以用shell下的全局变量DESTDIR）,这个时候它会把你的安装文件安装到你prefix下的相对路径下，因此当我们希望在make install的时候，比如当前的share_lib在lib目录下，我们希望安装之后的RPATH可以自动找到它，我们就可以这么写再来说一下make install下CMake是如何处理RPATH的。CMake为了方便用户的安装，默认在make install之后会自动remove删除掉相关的RPATH,这个时候你再去查看exe的RPATH，已经发现没有这个字段了。因此，当每次make install之后，我们进入到安装路径下执行相关exe的时候，就会发现此时的exe已经找不到相关的库路径了，因为它的RPATH已经被CMake给去除了。

    那么，如何让CMake能够在install的过程中写入相关RPATH并且该RPATH不能使当初build的时候的RPATH呢？答案就是CMAKE_INSTALL_RPATH这个全局变量和INSTALL_RPATH这个target属性。下面举一下简单的例子。

    大家都知道，CMake在安装的过程会有一个和configure一样的安装路径，CMAKE_INSTALL_PREFIX（configure下是--prefix,当然也可以用shell下的全局变量DESTDIR）,这个时候它会把你的安装文件安装到你prefix下的相对路径下，因此当我们希望在make install的时候，比如当前的share_lib在lib目录下，我们希望安装之后的RPATH可以自动找到它，我们就可以这么写");
            foreach (var item in str1)
            {
            }
            Console.WriteLine("分词使用时间：{0}", DateTime.Now - dt1);


            Console.Read();


            using (DemoContext context = new DemoContext())
            {


                var dept = new Department { Id = Guid.NewGuid(), Name = "部门" };
                context.Departments.Add(dept);
                context.SaveChanges();
                context.Users.Add(new User { Id = Guid.NewGuid(), Name = "abc", Sex = '0', Dept = dept });
                context.SaveChanges();
                var user = context.Users.Include(o => o.Dept).FirstOrDefault();
                ObjectsMapper<User, UserDto> mapper = ObjectMapperManager.DefaultInstance.GetMapper<User, UserDto>();
                UserDto userDto = mapper.Map(user);
                Console.WriteLine();
            }


            
        }
    }
}
