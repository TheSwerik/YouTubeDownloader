using Microsoft.Extensions.Localization;
using Shared.Exception;
using ExceptionResource = Frontend.Resources.Exception;

namespace Frontend.Service;

public class ExceptionLocalizationService
{
    public ExceptionLocalizationService(IStringLocalizer<ExceptionResource> localizer) { Localizer = localizer; }
    public string this[ExceptionType type] => Localizer[type.ToString()];

    public string this[ExceptionType type, params object?[] parameters] => Localizer[type.ToString(), parameters];
    private IStringLocalizer<ExceptionResource> Localizer { get; }
}