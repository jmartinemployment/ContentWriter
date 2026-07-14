using ContentWriter.Application.Services.Export;

namespace ContentWriter.Application.Tests;

public class DepartmentNameResolverTests
{
    [Theory]
    [InlineData("https://www.geekatyourspot.com/use-cases/accounting/smart-bank-reconciliation", "accounting")]
    [InlineData("https://www.geekatyourspot.com/use-cases/marketing", "marketing")]
    public void Resolve_extracts_department_from_use_cases_url(string url, string expected)
    {
        var result = DepartmentNameResolver.Resolve(url, null, null, null, null);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Resolve_prefers_explicit_override()
    {
        var result = DepartmentNameResolver.Resolve(
            "https://www.geekatyourspot.com/use-cases/accounting/foo",
            null,
            null,
            "Sales Project",
            "human-resources");

        Assert.Equal("human-resources", result);
    }

    [Fact]
    public void Resolve_uses_project_name_prefix_when_no_url_match()
    {
        var result = DepartmentNameResolver.Resolve(null, null, null, "Accounting - Smart Bank Recon", null);
        Assert.Equal("accounting", result);
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
