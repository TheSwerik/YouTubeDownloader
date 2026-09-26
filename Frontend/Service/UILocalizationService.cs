using Frontend.Resources;
using Microsoft.Extensions.Localization;

namespace Frontend.Service;

public class UILocalizationService(IStringLocalizer<UI> localizer)
{
    public string this[string key] => Localizer[key];

    public string this[string key, params object[] parameters] => Localizer[key, parameters];
    private IStringLocalizer<UI> Localizer { get; } = localizer;
}