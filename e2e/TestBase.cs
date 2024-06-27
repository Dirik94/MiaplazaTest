using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Text.Json;



namespace MiaplazaTest.e2e
{
    public abstract class TestBase
    {
        protected IBrowser? _browser;
        protected IPage? _page;
        protected IPlaywright? _playwright;
        public string _baseUrl;

        [SetUp]
        public async Task Setup()
        {
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("C:\\Users\\Greg\\source\\repos\\MiaplazaTest\\Utils\\appsettings.json"));
            _baseUrl = config!["baseUrl"];
            string browserName = config["browser"];

            _playwright = await Playwright.CreateAsync();

            _browser = browserName.ToLower() switch
            {
                "firefox" => await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }),
                "webkit" => await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }),
                _ => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false  })
            };

            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();

            // Start tracing before executing the test
            await context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            // Ensure _page is not null
            Assert.NotNull(_page);
        }

        [TearDown]
        public async Task Teardown()
        {
            string testName = TestContext.CurrentContext.Test.Name;

            if (_page != null)
            {
                // Stop tracing and export it into a zip archive
                await _page.Context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = $"{testName}_trace.zip"
                });
            }

            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
            _playwright?.Dispose();
        }
    }
}
