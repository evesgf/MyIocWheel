using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIocWheel
{
    /// <summary>
    /// 定义Ioc入口点
    /// </summary>
    public class IocAbstractBasics
    {
        public interface IIocKernel
        {
            /// <summary>
            /// 绑定抽象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            IIocKernel Bind<T>();

            /// <summary>
            /// 具体到关系维护对象
            /// </summary>
            /// <typeparam name="U"></typeparam>
            /// <returns></returns>
            IIocKernel To<U>() where U : class;

            /// <summary>
            /// 获取上层对象
            /// </summary>
            /// <typeparam name="V"></typeparam>
            /// <returns></returns>
            V GetValue<V>() where V : class;
        }
    }
}
