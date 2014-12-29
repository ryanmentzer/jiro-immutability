namespace Jiro.CodeAnalysis
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    [DebuggerStepThrough]
    internal static class Guard
    {
        internal static T NotNull<T>(T value, string parameterName) where T : class
        {
            NotEmpty(parameterName, nameof(parameterName));

            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        internal static TResult NotNull<T, TResult>(T value, string parameterName, Func<TResult> continuation) where T : class
        {
            NotEmpty(parameterName, nameof(parameterName));
            NotNull(continuation, nameof(continuation));

            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return continuation();
        }

        internal static string NotEmpty(string value, string parameterName)
        {
            var message = "'{0}' must not be null, empty, or whitespace.";

            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentUICulture, message, nameof(parameterName)),
                    nameof(parameterName));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentUICulture, message, parameterName), 
                    parameterName);
            }

            return value;
        }
    }
}