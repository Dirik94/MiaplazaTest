using Microsoft.Playwright;
using System.ComponentModel;

namespace MiaplazaTest.Pages
{
    public class StudentInformation : ParentInformation
    {
        private readonly IPage _page;

        public StudentInformation(IPage page) : base(page)
        {
            _page = page;
        }

        public async Task AddEmail(string email)
        {
            var inputBox = _page.Locator("#Email2-arialabel");
            await inputBox.FillAsync(email);
        }
        public async Task AddPhone(string phone)
        {
            var inputBox = _page.Locator("#PhoneNumber2");
            await inputBox.FillAsync(phone);
            await _page.Locator(".frmTitle").First.ClickAsync(); // click on Title to defocus input
        }
        public async Task SelectConsentContactinChild(bool consent)
        {
            await _page.Locator("div:below(:text(\"I consent to have my child contacted at this phone number by the MiaPrep Online High School staff\"))").GetByLabel("-Select-").First.ClickAsync();
            await _page.GetByRole(AriaRole.Treeitem, new() { Name = consent ? "Yes" : "No" }).ClickAsync();
        }
        public async Task VerifyStudentInfromationPageIsVsible()
        {
            await Expect(_page.GetByText("How many students would you like to enroll?")).ToBeVisibleAsync();
        }
        public async Task SelectHowManyStudentsWantToEntroll(bool oneChild)
        {
            await _page.Locator("#select2-Dropdown1-arialabel-container").ClickAsync();
            await _page.GetByRole(AriaRole.Treeitem, new() { Name = oneChild ? "One" : "Two" }).ClickAsync();
        }
        public async Task AddPreferredName(string preferedName)
        {
            var inputBox = _page.GetByRole(AriaRole.Textbox, new() { Name = "Preferred Name" });
            await inputBox.FillAsync(preferedName);
        }
        public async Task AddDateOfBirth(string dateOfBirth)
        {
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Date of Birth Required" }).FillAsync(dateOfBirth);
        }
        public async Task SelectGender(string gender)
        {
            await _page.Locator("#Dropdown3-li").GetByLabel("-Select-").ClickAsync();
            await _page.GetByRole(AriaRole.Treeitem, new() { Name = gender, Exact = true }).ClickAsync();
        }
        public async Task SelectMiaPrepAccount(bool haveAccount)
        {
            await _page.Locator("#Dropdown4-li").ClickAsync();
            await _page.GetByRole(AriaRole.Treeitem, new() { Name = haveAccount ? "Yes" : "No" }).ClickAsync();
        }
        public async Task AddSchoolingType(string schoolingType)
        {
            await _page.Locator("#Dropdown5-li").GetByLabel("-Select-").ClickAsync();
            await _page.GetByRole(AriaRole.Treeitem, new() { Name = schoolingType }).ClickAsync();
        }
        public async Task SelectMathSubjects(List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                var checkbox = _page.Locator("label").Filter(new() { HasText = options[i] }).First;
                await checkbox.ClickAsync();
                await Expect(checkbox).ToBeCheckedAsync();
            }
        }
        public async Task SelectEnglishLevels(List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                var checkbox = _page.Locator("label").Filter(new() { HasText = options[i] }).First;
                await checkbox.ClickAsync();
                await Expect(checkbox).ToBeCheckedAsync();
            }
        }
        public async Task SelectScienceSubjects(List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                var checkbox = _page.Locator("label").Filter(new() { HasText = options[i] }).First;
                await checkbox.ClickAsync();
                await Expect(checkbox).ToBeCheckedAsync();
            }
        }
        public async Task AddSchoolElectives(string text)
        {
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "List any high school" }).FillAsync("Test completed electives");
        }
        public async Task SelectStudentNeeds(bool haveNeeds)
        {
            await _page.Locator("#select2-Dropdown13-arialabel-container").ClickAsync();
            await _page.GetByRole(AriaRole.Treeitem, new() { Name = haveNeeds ? "Yes" : "No" }).ClickAsync();
        }
        public async Task SelectTypeOfMembership(string membership)
        {
            var inputbox = _page.Locator("#Dropdown10-li").GetByLabel("-Select-");
            await inputbox.ClickAsync();
            switch (membership)
            {
                case "Monthly":
                    membership = "Monthly";
                    break;
                case "Annual":
                    membership = "Annual";
                    break;
                default:
                    membership = "Lifetime";
                    break;
            }
            await _page.GetByRole(AriaRole.Treeitem, new() { Name = membership }).ClickAsync();

        }
    }
}

