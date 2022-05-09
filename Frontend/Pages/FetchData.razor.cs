using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public partial class FetchData
{
    [Inject] private TestClass Test { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Test.test();

        await Task.CompletedTask;
    }
}