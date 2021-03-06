using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlaywrightSharp.Tests.Attributes;
using PlaywrightSharp.Tests.BaseTests;
using PlaywrightSharp.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace PlaywrightSharp.Tests.Chromium
{
    ///<playwright-file>chromium/chromium.spec.js</playwright-file>
    ///<playwright-describe>resetOnNavigation</playwright-describe>
    [Collection(TestConstants.TestFixtureBrowserCollectionName)]
    public class JSResetOnNavigationTests : PlaywrightSharpPageBaseTest
    {
        /// <inheritdoc/>
        public JSResetOnNavigationTests(ITestOutputHelper output) : base(output)
        {
        }

        [PlaywrightTest("chromium/chromium.spec.js", "resetOnNavigation", "should report scripts across navigations when disabled")]
        [SkipBrowserAndPlatformFact(skipFirefox: true, skipWebkit: true)]
        public async Task ShouldReportScriptsAcrossNavigationsWhenDisabled()
        {
            await Page.Coverage.StartJSCoverageAsync(false);
            await Page.GoToAsync(TestConstants.ServerUrl + "/jscoverage/multiple.html");
            await Page.GoToAsync(TestConstants.EmptyPage);
            var coverage = await Page.Coverage.StopJSCoverageAsync();
            Assert.Equal(2, coverage.Length);
        }

        [PlaywrightTest("chromium/chromium.spec.js", "resetOnNavigation", "should NOT report scripts across navigations when enabled")]
        [SkipBrowserAndPlatformFact(skipFirefox: true, skipWebkit: true)]
        public async Task ShouldNotReportScriptsAcrossNavigationsWhenEnabled()
        {
            await Page.Coverage.StartJSCoverageAsync();
            await Page.GoToAsync(TestConstants.ServerUrl + "/jscoverage/multiple.html");
            await Page.GoToAsync(TestConstants.EmptyPage);
            var coverage = await Page.Coverage.StopJSCoverageAsync();
            Assert.Empty(coverage);
        }

        [PlaywrightTest("chromium/chromium.spec.js", "resetOnNavigation", "should not hang when there is a debugger statement")]
        [SkipBrowserAndPlatformFact(skipFirefox: true, skipWebkit: true)]
        public async Task ShouldNotHangWhenThereIsADebuggerStatement()
        {
            await Page.Coverage.StartJSCoverageAsync();
            await Page.GoToAsync(TestConstants.EmptyPage);
            await Page.EvaluateAsync(@"() => {
                debugger; // eslint-disable-line no-debugger
            }");
            await Page.Coverage.StopJSCoverageAsync();
        }
    }
}
