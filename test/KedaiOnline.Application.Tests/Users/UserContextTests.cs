using Xunit;
using KedaiOnline.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using KedaiOnline.Domain.Constants;
using FluentAssertions;

namespace KedaiOnline.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            //arrange
            var dateOfBirth = new DateOnly(1990, 1, 1);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "test@test.com"),
                new Claim(ClaimTypes.Role, UserRoles.Admin),
                new Claim(ClaimTypes.Role, UserRoles.User),
                new Claim("Nationality", "Malaysian"),
                new Claim("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext { User = user });
            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act
            var currentUser = userContext.GetCurrentUser();

            //assert
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("2");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentUser.Nationality.Should().Be("Malaysian");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);


        }

        [Fact()]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowInvalidOperationException()
        {
            //arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);
            var userContext = new UserContext(httpContextAccessorMock.Object);
            //act
            Action action = () => userContext.GetCurrentUser();
            //assert
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("User context is not present");
        }
    }
}
