using System;

namespace Ordersystem
{
    /// <summary>
    /// This is an example class. It is responsible for nothing.
    /// Because of this, it is only documented to show how to document code.
    /// </summary>
    public class PublicClass
    {
        public PublicClass(Object parameterName) //PublicConstructors
        {
            PublicProperty = parameterName;
            _privateProperty = parameterName;
        }

        public event Action PublicEvent; //PublicEvents

        public interface IExamplable //IInterfaces
        {
            Object Example();
        }

        public Object PublicProperty { get; private set; } //PublicProperties
        private Object _privateProperty { get; set; } //_privateProperties

        // Do a triple / to automatically get a "fill in the blanks" documention. This should be done to all public members

        /// <summary>
        /// PublicMethod is an example method
        /// </summary>
        /// <param name="parameterName">An example parameter</param>
        /// <returns></returns>
        public Object PublicMethod(Object parameterName) //PublicMethods
        {
            if (parameterName == null) //Control flow = A space before (conditions)
            {
                //Foo
            }
            else
            {
                //Bar
            }
            return new Object();
        }

        private void _privateMethod() //_privateMethods
        {
            /*
             * PublicProperty.StuffA       //At atleast 3 "member chains", seperate into multiple lines. (Often using LINQ)
             *               .StuffB
             *               .StuffC();
             */
        }

        private class _privateClass //_privateClasses
        {

        }
    }
}