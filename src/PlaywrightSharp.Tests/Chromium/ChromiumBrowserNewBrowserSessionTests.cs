using System.Threading.Tasks;
using PlaywrightSharp.Chromium;
using PlaywrightSharp.Tests.Attributes;
using PlaywrightSharp.Tests.BaseTests;
using PlaywrightSharp.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace PlaywrightSharp.Tests.Chromium
{
    ///<playwright-file>chromium/session.spec.js</playwright-file>
    ///<playwright-describe>ChromiumBrowser.newBrowserCDPSession</playwright-describe>
    [Collection(TestConstants.TestFixtureBrowserCollectionName)]
    public class ChromiumBrowserNewBrowserSessionTests : PlaywrightSharpBrowserBaseTest
    {
        /// <inheritdoc/>
        public ChromiumBrowserNewBrowserSessionTests(ITestOutputHelper output) : base(output)
        {
        }

        [PlaywrightTest("chromium/session.spec.js", "ChromiumBrowser.newBrowserCDPSession", "should work")]
        [SkipBrowserAndPlatformFact(skipFirefox: true, skipWebkit: true)]
        public async Task ShouldWork()
        {
            var session = await ((IChromiumBrowser)Browser).NewBrowserCDPSessionAsync();
            Assert.NotNull(await session.SendAsync<object>("Browser.getVersion"));

            bool gotEvent = false;
            session.MessageReceived += (sender, e) =>
            {
                if (e.Method == "Target.targetCreated")
                {
                    gotEvent = true;
                }
            };

            await session.SendAsync("Target.setDiscoverTargets", new { discover = true });
            Assert.True(gotEvent);
            await session.DetachAsync();
        }
    }
}
