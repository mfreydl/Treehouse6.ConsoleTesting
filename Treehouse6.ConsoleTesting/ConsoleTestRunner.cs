using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Treehouse6.ConsoleTesting
{
    public class ConsoleTestRunner
    {
        /// <summary>
        /// Runs the test console for all tests in the calling assembly.
        /// </summary>
        public static void RunTests()
        {
            RunTests(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Runs the test console for all tests in the assembly containing the given test class.
        /// </summary>
        public static void RunTests(Type testClass)
        {
            RunTests(testClass.Assembly);
        }

        /// <summary>
        /// Runs the test console for all tests in the given testSource assembly.
        /// </summary>
        public static void RunTests(Assembly testSource)
        {
            var runner = new ConsoleTestRunner(testSource);
            runner.Run();
        }


        private ConsoleTestRunner(Assembly sourceAssembly)
        {
            SourceAssembly = sourceAssembly;
            _testMethods = new List<MethodInfo>();
        }


        private Assembly SourceAssembly { get; set; }
        private IList<MethodInfo> _testMethods;

        public void Run()
        {
            _testMethods = GetTestMethods();

            WriteHeader();
            Console.Write("Enter the number of a test to run (\"0\" for all, [enter] to exit):");
            var input = Console.ReadKey().KeyChar;

            while (input != '\r')
            {
                Console.Clear();

                Int32 ordinal;
                if (Int32.TryParse(input.ToString(), out ordinal))
                {
                    if (ordinal == 0)  // Run ALL tests
                    {
                        ExecuteAllTests();
                    }
                    else if (ordinal <= _testMethods.Count)  // Run selected test
                    {
                        ExecuteTest(_testMethods[ordinal - 1]);
                    }
                    else
                    {
                        Console.WriteLine("\"{0}\" is an invalid keystroke.  Try again...", input);
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                WriteHeader();
                Console.Write("Enter the number of a test to run (or enter to exit):");
                input = Console.ReadKey().KeyChar;
            }
        }



        private void ExecuteAllTests()
        {
            foreach (var testClass in _testMethods.Select(m => m.DeclaringType).Distinct())
            {
                object instance = null;
                foreach (var method in _testMethods.Where(m => m.DeclaringType == testClass))
                {
                    if (instance == null)
                        instance = ExecuteTest(method);
                    else
                        ExecuteTest(method, instance);
                }
            }
        }


        /// <summary>
        /// Executes the given test method and returns a new instance of the test class that contains it.
        /// NOTE: This method instantiates the test class and executes the test harness setup method.
        /// </summary>
        /// <param name="testMethod"></param>
        /// <returns></returns>
        private object ExecuteTest(MethodInfo testMethod)
        {
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Test Class: {0}", testMethod.DeclaringType.Name);
            var instance = Activator.CreateInstance(testMethod.DeclaringType);
            ExecuteHarnessSetup(instance);
            ExecuteTest(testMethod, instance);
            return instance;
        }



        private void ExecuteHarnessSetup(object testClassInstance)
        {
            // find the method with attribute: [ConsoleHarnessSetup]
            var harnessSetupMethod = testClassInstance.GetType().GetMethods()
                .FirstOrDefault(
                    m => m.CustomAttributes.Any(a => a.AttributeType == typeof(ConsoleTestHarnessSetupAttribute))
                );
            // Execute it
            if (harnessSetupMethod != null)
                harnessSetupMethod.Invoke(testClassInstance, null);
        }




        private void ExecuteTest(MethodInfo testMethod, object testClassInstance)
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Test: {0}", testMethod.Name);
            try
            {
                testMethod.Invoke(testClassInstance, null);
                WriteSuccess();
            }
            catch (TargetInvocationException ex)  // since we're invoking the test method, our exception is gonna be wrapped up in one of these.
            {
                if (ex.InnerException != null && ex.InnerException.GetType() == typeof(AssertionFailureException))
                    WriteAssertFailure(ex.InnerException as AssertionFailureException);
                else
                {
                    WriteFailure(ex.InnerException);
                    Console.WriteLine("Exception encountered:");
                    Console.WriteLine(ex.InnerException.ToString());
                }
            }
            catch (Exception ex)
            {
                WriteFailure(ex);
                Console.WriteLine("Exception encountered:");
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine();
        }

        private void WriteHeader()
        {
            Console.WriteLine("///////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("/// CONSOLE TESTS:                                                          ///");

            string className = null;
            for (var ictr = 0; ictr < _testMethods.Count; ictr++)
            {
                var method = _testMethods[ictr];
                if (className != method.DeclaringType.Name)
                {
                    className = method.DeclaringType.Name;
                    Console.WriteLine(String.Format("/// {0}", className).PadRight(76).Substring(0, 76) + "///");
                }

                Console.Write("/// ");
                
                // Highlight the test ordinal in yellow
                var originalFG = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write((ictr + 1).ToString().PadLeft(2, ' '));
                Console.ForegroundColor = originalFG;

                var restOfLine = String.Format(") {0}", method.Name).PadRight(70).Substring(0, 70) + "///";
                Console.WriteLine(restOfLine);
            }

            Console.WriteLine("///////////////////////////////////////////////////////////////////////////////");
        }

        private IList<MethodInfo> GetTestMethods()
        {
            var classes = SourceAssembly.GetExportedTypes()
                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(ConsoleTestClassAttribute)))
                .ToList();
            var methods = classes.SelectMany(t => t.GetMethods().Where(m => m.CustomAttributes.Any(a => a.AttributeType == typeof(ConsoleTestMethodAttribute))))
                .ToList();

            return methods;
        }



        private const string FAILURE_PREFIX = "TEST FAILED: ";
        private const string SUCCESS_PREFIX = "PASSED!";


        private void WriteSuccess()
        {
            var originalFG = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(SUCCESS_PREFIX);
            Console.ForegroundColor = originalFG;
        }

        private void WriteFailure(Exception ex)
        {
            // Render the innermost exception message
            var message = String.Format("{0} threw {1}: {2}.", ex.Source, ex.GetType().Name, ex.Message);
            var innerEx = ex.InnerException;
            while (innerEx != null)
            {
                message = String.Format("{0} threw {1}: {2}.", innerEx.Source, innerEx.GetType().Name, innerEx.Message);
                innerEx = innerEx.InnerException;
            }

            var originalFG = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(FAILURE_PREFIX);
            Console.ForegroundColor = originalFG;
            Console.WriteLine(message);
            Console.WriteLine("DETAILS:");
            Console.WriteLine(ex.ToString());
        }

        private void WriteAssertFailure(AssertionFailureException ex)
        {
            var originalFG = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(FAILURE_PREFIX);
            Console.ForegroundColor = originalFG;
            Console.WriteLine(ex.Message);
            if (ex.Actual != null && ex.Expected != null)
            {
                Console.WriteLine(ShowActualVsExpected(ex.Actual.ToString(), ex.Expected.ToString()));
            }
        }

        private static string ShowActualVsExpected(string actual, string expected)
        {
            return String.Format("Actual: \"{0}\"\nExpected: \"{1}\"", actual, expected);
        }

    }
}
