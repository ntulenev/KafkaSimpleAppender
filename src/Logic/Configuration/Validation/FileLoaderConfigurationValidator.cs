using Microsoft.Extensions.Options;

using System.Diagnostics;

namespace Logic.Configuration.Validation
{
    public class FileLoaderConfigurationValidator : IValidateOptions<FileLoaderConfiguration>
    {
        public ValidateOptionsResult Validate(string name, FileLoaderConfiguration options)
        {
            Debug.Assert(name is not null);
            Debug.Assert(options is not null);

            if (string.IsNullOrWhiteSpace(options.FileValueField))
            {
                return ValidateOptionsResult.Fail("FileValueField is not set.");
            }

            if (string.IsNullOrWhiteSpace(options.FileKeyField))
            {
                return ValidateOptionsResult.Fail("FileKeyField is not set.");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
