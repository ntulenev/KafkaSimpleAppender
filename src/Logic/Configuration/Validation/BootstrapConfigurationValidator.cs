using System.Diagnostics;

using Microsoft.Extensions.Options;

namespace Logic.Configuration.Validation;

/// <summary>
/// Validator for <see cref="BootstrapConfiguration"/>.
/// </summary>
public class BootstrapConfigurationValidator : IValidateOptions<BootstrapConfiguration>
{
    /// <summary>
    /// Validates <see cref="KafkaServiceClientConfiguration"/>.
    /// </summary>
    public ValidateOptionsResult Validate(string name, BootstrapConfiguration options)
    {
        Debug.Assert(name is not null);
        Debug.Assert(options is not null);

        if (options.BootstrapServers is null)
        {
            return ValidateOptionsResult.Fail("BootstrapServers section is not set.");
        }

        if (!options.BootstrapServers.Any())
        {
            return ValidateOptionsResult.Fail("BootstrapServers section is empty.");
        }

        if (options.BootstrapServers.Any(x => String.IsNullOrEmpty(x)))
        {
            return ValidateOptionsResult.Fail("BootstrapServers section contains empty string.");
        }

        if (options.BootstrapServers.Any(x => String.IsNullOrWhiteSpace(x)))
        {
            return ValidateOptionsResult.Fail("BootstrapServers section contains empty string of whitespaces.");
        }

        if (options.MessageMaxBytes is not null && options.MessageMaxBytes <= 0)
        {
            return ValidateOptionsResult.Fail("MessageMaxBytes should be more that zero.");
        }

        return ValidateOptionsResult.Success;
    }
}
