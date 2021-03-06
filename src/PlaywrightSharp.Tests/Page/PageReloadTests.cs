using System.Threading.Tasks;
using PlaywrightSharp.Tests.BaseTests;
using PlaywrightSharp.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace PlaywrightSharp.Tests.Page
{
    ///<playwright-file>navigation.spec.js</playwright-file>
    ///<playwright-describe>Page.reload</playwright-describe>
    [Collection(TestConstants.TestFixtureBrowserCollectionName)]
    public class PageReloadTests : PlaywrightSharpPageBaseTest
    {
        /// <inheritdoc/>
        public PageReloadTests(ITestOutputHelper output) : base(output)
        {
        }

        [PlaywrightTest("navigation.spec.js", "Page.reload", "should work")]
        [Fact(Timeout = TestConstants.DefaultTestTimeout)]
        public async Task ShouldWork()
        {
            await Page.GoToAsync(TestConstants.EmptyPage);
            await Page.EvaluateAsync("() => window._foo = 10");
            await Page.ReloadAsync();
            Assert.Null(await Page.EvaluateAsync("() => window._foo"));
        }
    }
}
