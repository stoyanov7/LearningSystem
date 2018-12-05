namespace LearningSystem.Utilities.Common
{
    using System;

    public static class CoreValidator
    {
        public static void EnsureNotNull(object obj, string message = null)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        public static void EnsureStringIsNotNullOrEmpty(string str, string message = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException(message);
            }
        }
    }
}