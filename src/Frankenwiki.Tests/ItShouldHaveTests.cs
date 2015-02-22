using Shouldly;
using Xunit;

namespace Frankenwiki.Tests
{
    public class ItShouldHaveTests
    {
        [Fact]
        public void BecauseTestsAreGreat()
        {
            "Yes, yes they are.".ShouldNotBeEmpty();
        }
    }
}