using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Linq.Expressions;

namespace MiaplazaTest.Pages
{
    public class ParentInformation : PageTest
    {
        private readonly IPage _page;
        public ParentInformation(IPage page) => _page = page;

        public async Task AddName(string firstName)
        {
            var inputBox = _page.GetByRole(AriaRole.Textbox, new() { Name = "Name First Name Required" });
            await inputBox.FillAsync(firstName);
        }

        public async Task AddSurname(string surname)
        {
            var inputBox = _page.GetByRole(AriaRole.Textbox, new() { Name = "Name Last Name Required" });
            await inputBox.FillAsync(surname);
        }

        public async Task AddEmail(string email)
        {
            var inputBox = _page.Locator("#Email-arialabel");
            await inputBox.FillAsync(email);
        }

        public async Task AddPhone(string phone)
        {
            var inputBox = _page.Locator("#PhoneNumber");
            await inputBox.FillAsync(phone);
        }

        public async Task SelectInformationAboutGuardian(bool isGuardian)
        {
            await _page.Locator("#select2-Dropdown-arialabel-container").ClickAsync();
            await _page.GetByRole(AriaRole.Treeitem, new() { Name = isGuardian ? "Yes" : "No" }).ClickAsync();
        }

        public async Task SelectHowDidYouHearAboutUs(List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                var checkbox = _page.Locator("label").Filter(new() { HasText = options[i] });
                await checkbox.ClickAsync();
                await Expect(checkbox).ToBeCheckedAsync();
            }
        }

        public async Task AddPreferredStartDate(string preferredStartDateMonth, string preferredStartDateDay)
        {
            var inputbox = _page.Locator("#Date-date");
            await inputbox.ClickAsync();
            await _page.GetByTitle("Jul").ClickAsync();
            switch (preferredStartDateMonth)
            {
                case "Sep":
                    preferredStartDateMonth = "Sep";
                    break;
                case "Aug":
                    preferredStartDateMonth = "Aug";
                    break;
                default:
                    preferredStartDateMonth = "Jul";
                    break;
            }
            await _page.GetByRole(AriaRole.Treeitem, new() { Name = preferredStartDateMonth }).ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = preferredStartDateDay }).ClickAsync();

        }
    }
}

