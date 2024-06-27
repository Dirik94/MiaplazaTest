using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Text.RegularExpressions;

namespace MiaplazaTest.Pages
{
    public class OnlineSchool : PageTest
    {
        private readonly IPage _page;
        public OnlineSchool(IPage page) => _page = page;

        public async Task ClickOnApplyLink()
        {
            await _page.Locator(".wp-block-button").GetByRole(AriaRole.Link, new() { Name = "Apply to Our School" }).First.ClickAsync();
            await Expect(_page).ToHaveURLAsync(new Regex("forms.zohopublic.com"));
        }
    }
}

