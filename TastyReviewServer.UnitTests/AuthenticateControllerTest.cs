
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TastyReviewsServer.Controllers;
using static TastyReviewServer.UnitTests.Constants.Users;

//https://www.reddit.com/r/learncsharp/comments/b41azf/usermanager_not_creating_user_in_unit_tests/
namespace TastyReviewServer.UnitTests
{
    public class AuthenticateControllerTest : TestBaseSetup
    {
        [Fact]
        public Task Registration_Should_Return_Success()
        {
            //Arrange
            var userName = Constants.Users.NonAdmin.UserName;
            var email = Constants.Users.Admin.Email;
            var password = Constants.Users.Passwords.Default;

            //Act
            var response = RegisterUser(userName, email, password);
#pragma warning disable xUnit1030 // Do not call ConfigureAwait(false) in test method
#pragma warning disable xUnit1031 // Do not use blocking task operations in test method
            var result = _userManager.FindByNameAsync(userName).ConfigureAwait(false).GetAwaiter().GetResult();
#pragma warning restore xUnit1031 // Do not use blocking task operations in test method
#pragma warning restore xUnit1030 // Do not call ConfigureAwait(false) in test method           

            //Assert            
            Assert.True(result.Email == Constants.Users.NonAdmin.Email);
            Assert.False(((ObjectResult)response.Result).StatusCode == 500, "Registration Not Completed");
            return Task.CompletedTask;
        }

        [Fact]
        public Task Registration_Should_Return_UserName_Exist()
        {
            //Arrange
            var userName = Constants.Users.NonAdmin.UserName;
            //Act
#pragma warning disable xUnit1030 // Do not call ConfigureAwait(false) in test method
#pragma warning disable xUnit1031 // Do not use blocking task operations in test method
            var result = _userManager.FindByNameAsync(userName).ConfigureAwait(false).GetAwaiter().GetResult();
#pragma warning restore xUnit1031 // Do not use blocking task operations in test method
#pragma warning restore xUnit1030 // Do not call ConfigureAwait(false) in test method

            //Assert
            Assert.True(result.UserName == Constants.Users.NonAdmin.UserName);
            return Task.CompletedTask;
        }

        [Fact]
        public Task Registration_Admin_Should_Return_Success()
        {
            //Arrange
            var userName = Constants.Users.Admin.UserName;
            var email = Constants.Users.Admin.Email;
            var password = Constants.Users.Passwords.Default;
            //Act
            var response = InsertAdminUser(userName,email,password);
#pragma warning disable xUnit1030 // Do not call ConfigureAwait(false) in test method
#pragma warning disable xUnit1031 // Do not use blocking task operations in test method
            var result = _userManager.FindByNameAsync(userName).ConfigureAwait(false).GetAwaiter().GetResult();
#pragma warning restore xUnit1031 // Do not use blocking task operations in test method
#pragma warning restore xUnit1030 // Do not call ConfigureAwait(false) in test method           

            //Assert            
            Assert.True(result.Email == Constants.Users.Admin.Email);
            Assert.False(((ObjectResult)response.Result).StatusCode == 500, "Registration Not Completed");
            return Task.CompletedTask;
        }

        [Fact]
        public Task Login_Success()
        {
            //Arrange
            var userName = Constants.Users.NonAdmin.UserName;
            var password = Constants.Users.Passwords.Default;

            //Act
            var response = Login(userName,password);

            //Assert
            Assert.True(((ObjectResult)response.Result).StatusCode == 200, "Login Success");
            Assert.True(((ObjectResult)response.Result).Value != "", "Login Success");
            return Task.CompletedTask;

        }
    }
}