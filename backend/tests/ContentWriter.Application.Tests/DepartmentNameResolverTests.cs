using ContentWriter.Application.Providers;
using ContentWriter.Application.Services.Export;

namespace ContentWriter.Application.Tests;

public class DepartmentNameResolverTests
{
    [Fact]
    public void Resolve_sanitizes_explicit_override()
    {
        var result = DepartmentNameResolver.Resolve("Human Resources");
        Assert.Equal("human-resources", result);
    }

    [Fact]
    public void Resolve_throws_when_no_override_provided()
    {
        Assert.Throws<ContentGenerationException>(() => DepartmentNameResolver.Resolve(null));
    }

    [Theory]
    [InlineData("Predictive Cash Flow Forecasting", "Predictive Cash Flow Forecasting")]
    [InlineData("  Smart Bank Reconciliation  ", "Smart Bank Reconciliation")]
    [InlineData("foo/bar:baz", "foobarbaz")]
    public void ResolveTopicFolder_uses_target_keyword(string keyword, string expected)
    {
        var result = DepartmentNameResolver.ResolveTopicFolder(keyword, "fallback-slug");
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ResolveTopicFolder_falls_back_to_slug_when_keyword_empty()
    {
        var result = DepartmentNameResolver.ResolveTopicFolder("   ", "smart-bank-reconciliation");
        Assert.Equal("smart bank reconciliation", result);
    }
}
