using FluentAssertions;
using KedaiOnline.Domain.Constants;
using Xunit;

namespace KedaiOnline.Application.Users.Tests;

public class CurrentUserTests
{
    //TestMethod_Scenario_ExpectedResult

    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData("User")]
    public void IsInRole2_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        //arrange
        var currentUser = new CurrentUser("1", "test123@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        //act
        var isInRole = currentUser.IsInRole(roleName);

        //assert result
        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue()
    {
        //arrange
        var currentUser = new CurrentUser("1", "test123@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        //act
        var isInRole = currentUser.IsInRole(UserRoles.User);

        //assert result
        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        //arrange
        var currentUser = new CurrentUser("1", "test123@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        //act
        var isInRole = currentUser.IsInRole(UserRoles.Owner);

        //assert result
        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        //arrange
        var currentUser = new CurrentUser("1", "test123@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        //act
        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        //assert result
        isInRole.Should().BeFalse();
    }
}