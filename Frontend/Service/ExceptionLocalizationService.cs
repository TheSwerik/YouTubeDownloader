using Microsoft.Extensions.Localization;
using Shared.Exception;
using Exception = Frontend.Resources.Exception;

namespace Frontend.Service;

public class ExceptionLocalizationService
{
    internal ExceptionLocalizationService(IStringLocalizer<Exception> localizer) { Localizer = localizer; }
    private IStringLocalizer<Exception> Localizer { get; }

    public string Get(ExceptionType type)
    {
        Console.WriteLine(ExceptionType.DEFAULT.ToString());
        Console.WriteLine(Localizer[ExceptionType.DEFAULT.ToString()]);
        Console.WriteLine(type.ToString());
        Console.WriteLine(Localizer[type.ToString()]);
        return Localizer[type.ToString()];
    }
}