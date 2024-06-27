using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Text.RegularExpressions;

namespace MiaplazaTest.Pages
{
    public class HomePage : PageTest
    {
        private readonly IPage _page;
        public HomePage(IPage page) => _page = page;

        public async Task GoToHomePage(string baseUrl)
        {
            await _page!.GotoAsync(baseUrl);
            await Expect(_page.GetByText("Learning, Fun & Friends for")).ToBeVisibleAsync();
        }

        public async Task ClickOnOnlineSchoolLink()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "Online High School" }).ClickAsync();
            await Expect(_page).ToHaveURLAsync(new Regex("online-school"));
        }

    }
}

