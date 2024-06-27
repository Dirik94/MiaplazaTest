using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace MiaplazaTest.Utils;
public class TestComponents : PageTest
{
    private readonly IPage _page;
    public TestComponents(IPage page) => _page = page;
    public async Task ClickNextButton()
    {
        await _page.GetByRole(AriaRole.Button, new() { Name = Buttons.Next }).ClickAsync();
    }
}