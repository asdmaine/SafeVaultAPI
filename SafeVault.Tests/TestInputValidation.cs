using NUnit.Framework;
using SafeVault.Web.Security;

[TestFixture]
public class TestInputValidation
{
    [Test]
    public void TestForXSS()
    {
        var malicious = "<script>alert('xss')</script>hello";

        var result = InputSanitizer.Sanitize(malicious);

        Assert.AreEqual("hello", result);
    }
    [Test]
public void TestForSQLInjection()
{
    var attack = "'; DROP TABLE Users; --";

    Assert.DoesNotThrow(() =>
    {
        var result = InputSanitizer.Sanitize(attack);
        Assert.IsNotNull(result);
    });
}
}