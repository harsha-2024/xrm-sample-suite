using System;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Xunit;
using XrmSample.Plugins.Plugins;

namespace XrmSample.Tests
{
    public class AccountCreateValidationPluginTests
    {
        [Fact]
        public void Throws_When_Duplicate_Email_Found()
        {
            var ctx = new XrmFakedContext();
            var existing = new Entity("account") { Id = Guid.NewGuid() };
            existing["emailaddress1"] = "dup@contoso.com";
            ctx.Initialize(new[] { existing });

            var target = new Entity("account") { ["emailaddress1"] = "dup@contoso.com", ["name"] = "Dup" };

            var ex = Assert.Throws<InvalidPluginExecutionException>(() =>
                ctx.ExecutePluginWithTarget<AccountCreateValidationPlugin>(target)
            );

            Assert.Contains("already exists", ex.Message);
        }

        [Fact]
        public void Sets_AccountNumber_When_Missing()
        {
            var ctx = new XrmFakedContext();
            var target = new Entity("account") { ["name"] = "NoNumber" };

            ctx.ExecutePluginWithTarget<AccountCreateValidationPlugin>(target);
            var created = ctx.CreateQuery("account");
            // In FakeXrmEasy, pre-operation modifications reflect on target; we assert attribute changed
            Assert.True(target.Attributes.ContainsKey("accountnumber"));
        }
    }
}
