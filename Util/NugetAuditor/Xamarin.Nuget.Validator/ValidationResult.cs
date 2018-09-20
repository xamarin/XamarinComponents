using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Nuget.Validator
{
    public class ValidationResult
    {
        public bool Success { get; set; }

        public List<string> Errors { get; set; }

        public List<string> Warnings { get; set; }

        public ValidationResult()
        {
            Success = true;
            Errors = new List<string>();
            Warnings = new List<string>();

        }

        public string ErrorMessages
        {
            get
            {
                var msg = string.Empty;

                foreach (var aErr in Errors)
                    msg += aErr + Environment.NewLine;

                return msg;
            }
        }
    }
}
