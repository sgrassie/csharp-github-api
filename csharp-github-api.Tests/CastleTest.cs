#if NUNIT
using NUnit.Framework;
#else
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using Fact = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using TestFixtureSetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using SetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

using System;
using System.Reflection;
using Castle.DynamicProxy;
using FluentAssertions;


namespace GitHubAPI.Tests
{
    public abstract class AbstractBaseClass
    {
        public virtual string MethodA()
        {
            return "Method A";
        }

        protected virtual string MethodB()
        {
            return "Method B";
        }
    }

    public class SomeClass : AbstractBaseClass
    {
        public virtual string MethodC()
        {
            return "Method C";
        }
    }

    public static class MyInterceptor<TTarget>
        where TTarget : AbstractBaseClass
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        public static TTarget Create(TTarget target, string message)
        {
            return ProxyGenerator.CreateClassProxyWithTarget(target,
                                                                new ProxyGenerationOptions(
                                                                    new ProxyGenerationHook()),
                                                                    new Interceptor(message));
        }

        private class Interceptor : IInterceptor
        {
            private readonly string _message;

            public Interceptor(string message)
            {
                _message = message;
            }

            public void Intercept(IInvocation invocation)
            {
                invocation.Proceed(); 
                var currentString = (string)invocation.ReturnValue;
                currentString = _message;
                invocation.ReturnValue = currentString;
            }
        }
    }

    public class ProxyGenerationHook : IProxyGenerationHook
    {
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            return true;
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {   
        }

        public void MethodsInspected()
        {
        }
    }

    [TestFixture]
    public class CastleTests
    {
        [Test]
        public void Should_Intercept_MethodA()
        {
            var someClass = new SomeClass();
            var proxy = MyInterceptor<AbstractBaseClass>.Create(someClass, "Intercepted!");
            var text = proxy.MethodA();
            text.Should().Be("Intercepted!");
        }
    }
}
