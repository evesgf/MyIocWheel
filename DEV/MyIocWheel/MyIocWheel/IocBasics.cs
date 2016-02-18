using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIocWheel
{
    public class IocBasics
    {
    }

    public class IocKernel : IocAbstractBasics.IIocKernel
    {
        private Type _BaseType;

        public IocKernel()
        {
            IocContext.Context.DiTypeInfoManage = new DITypeInfoManage();
        }

        public IocAbstractBasics.IIocKernel Bind<T>()
        {
            _BaseType = typeof (T);
            return this;
        }

        public IocAbstractBasics.IIocKernel To<U>() where U : class
        {
            Type achieveType = typeof (U);
            if (achieveType.BaseType == _BaseType || achieveType.GetInterface(_BaseType.Name) != null)
            {
                IocContext.Context.DiTypeInfoManage.AddTypeInfo(_BaseType, achieveType);
            }
            return this;
        }

        public V GetValue<V>() where V : class
        {
            return IocContext.Context.DITypeAnalyticalProvider.CreateDITypeAnalaytical().GetValue<V>();
        }
    }

    /// <summary>
    /// DI类型关系信息管理
    /// 维护抽象、具体类型关系信息
    /// 实际上是实现了键值对
    /// </summary>
    public class DITypeInfoManage
    {
        private Dictionary<Type, Type> _DITypeInfo;

        public DITypeInfoManage()
        {
            _DITypeInfo=new Dictionary<Type, Type>();
        }

        /// <summary>
        /// 添加DI类型关系
        /// </summary>
        /// <param name="key">抽象类型</param>
        /// <param name="value">实现类型</param>
        public void AddTypeInfo(Type key, Type value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (_DITypeInfo.ContainsKey(key))
            {
                return;
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            _DITypeInfo.Add(key,value);
        }

        /// <summary>
        /// 获取DI类型关系的实现模型
        /// </summary>
        /// <param name="key">抽象模型</param>
        /// <returns></returns>
        public Type GetTypeInfo(Type key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (_DITypeInfo.ContainsKey(key))
            {
                return _DITypeInfo[key];
            }
            return null;
        }

        public bool ContainsKey(Type key)
        {
            if (key==null)
            {
                throw new ArgumentNullException("key");
            }
            return _DITypeInfo.ContainsKey(key);
        }
    }

    public class IocContext
    {
        private IocContext() { }

        private static IocContext _Context;

        public static IocContext Context
        {
            get
            {
                if (_Context == null)
                {
                    _Context=new IocContext();
                }
                return _Context;
            }
        }

        private IDITypeAnalyticalProvider _DITypeAnalyticalProvider;

        public IDITypeAnalyticalProvider DITypeAnalyticalProvider
        {
            get
            {
                if (_DITypeAnalyticalProvider == null)
                {
                    _DITypeAnalyticalProvider = new DITypeAnalyticalProvider();
                }
                return _DITypeAnalyticalProvider;
            }
            set { _DITypeAnalyticalProvider = value; }
        }

        private DITypeInfoManage _DITypeInfoManage;
        public DITypeInfoManage DiTypeInfoManage
        {
            get { return _DITypeInfoManage; }
            set { _DITypeInfoManage = value; }
        }
    }

    public interface IDITypeAnalyticalProvider
    {
        IDITypeAnalyticalProvider CreateDITypeAnalaytical();
    }

    public class DefualtDITypeAnalyticalProivder:IDITypeAnalyticalProvider
    {
        public IDITypeAnalyticalProvider CreateDITypeAnalaytical()
        {
            return new DITypeAnalytical();
        }
    }

    public interface IDITypeAnalytical
    {
        T GetValue<T>();
    }

    public class DITypeAnalytical: IDITypeAnalytical
    {
        public T GetValue<T>()
        {
            Type type = typeof (T);
            return (T) TypeAnalytical(type);
        }

        private object TypeAnalytical(Type type)
        {
            return null;    //todo
        }
    }
}
