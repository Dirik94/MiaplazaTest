using Microsoft.Playwright;
using System.Text.Json;



namespace MiaplazaTest.e2e
{
    public abstract class TestBase
    {
        protected IBrowser? _browser;
        protected IPage? _page;
        protected IPlaywright? _playwright;
        public string _baseUrl;
        public string projectDirectory;

        [SetUp]
        public async Task Setup()
        {
            var baseDirectory = AppContext.BaseDirectory;
            projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;
            var configFilePath = Path.Combine(projectDirectory, ".\\Utils\\appsettings.json");
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(configFilePath));

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
