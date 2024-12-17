﻿using Cliente.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TESTS
{
    public class ValidatorTests
    {
        private readonly Validator _validator;

        public ValidatorTests()
        {
            _validator = new Validator();
        }


        [Theory]
        [InlineData("a@b.com")]
        [InlineData("user.name+tag+sorting@example.com")]
        public void ValidateEmail_ValidEmails_ReturnsEmpty(string email)
        {
            
            var result = _validator.ValidateEmail(email);

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ValidateEmail_EmptyOrWhitespace_ReturnsEmptyError(string email)
        {
            
            var result = _validator.ValidateEmail(email);

            Assert.Equal("lblErrEmailEmpty", result);
        }

        [Theory]
        [InlineData("a@b.com ")]
        [InlineData(" a@b.com")]
        [InlineData("user @example.com")]
        public void ValidateEmail_EmailContainsWhitespace_ReturnsWhitespaceError(string email)
        {

            var result = _validator.ValidateEmail(email);

            Assert.Equal("lblErrEmailContainsWhiteSpace", result);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("user@.com")]
        [InlineData("user@com..com")]
        [InlineData("user@com_com.com")]
        [InlineData("user@com,com.com")]
        public void ValidateEmail_InvalidFormat_ReturnsInvalidError(string email)
        {

            var result = _validator.ValidateEmail(email);

            Assert.Equal("lblErrEmailInvalid", result);
        }

        [Fact]
        public void ValidateEmail_EmailTooLong_ReturnsTooLongError()
        {

            var longEmail = new string('a', 256 - "@example.com".Length) + "@example.com";

            var result = _validator.ValidateEmail(longEmail);

            Assert.Equal("lblErrEmailTooLong", result);
        }


        [Theory]
        [InlineData("username123")]
        [InlineData("user_name")]
        [InlineData("USERNAME")]
        [InlineData("user123_NAME")]
        public void ValidateUsername_ValidUsernames_ReturnsEmpty(string username)
        {

            var result = _validator.ValidateUsername(username);

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ValidateUsername_EmptyOrWhitespace_ReturnsEmptyError(string username)
        {

            var result = _validator.ValidateUsername(username);

            Assert.Equal("lblErrUsernameEmpty", result);
        }

        [Theory]
        [InlineData("user name")]
        [InlineData("user-name")]
        [InlineData("user.name")]
        [InlineData("user@name")]
        [InlineData("user#name")]
        public void ValidateUsername_InvalidCharacters_ReturnsInvalidCharactersError(string username)
        {

            var result = _validator.ValidateUsername(username);

            Assert.Equal("lblErrUsernameInvalid", result);
        }

        [Fact]
        public void ValidateUsername_UsernameTooLong_ReturnsTooLongError()
        {

            var longUsername = new string('a', 256);

            var result = _validator.ValidateUsername(longUsername);

            Assert.Equal("lblErrUsernameTooLong", result);
        }



        [Theory]
        [InlineData("StrongP@ssw0rd!")]
        [InlineData("Another$trong1")]
        [InlineData("ValidPass#1234")]
        public void ValidatePassword_ValidPasswords_ReturnsEmpty(string password)
        {

            var result = _validator.ValidatePassword(password);

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ValidatePassword_EmptyOrWhitespace_ReturnsEmptyError(string password)
        {

            var result = _validator.ValidatePassword(password);

            Assert.Equal("lblErrPasswordEmpty", result);
        }

        [Theory]
        [InlineData("Short1!")]
        [InlineData("NoSpecialChar123")]
        [InlineData("nouppercase1!")]
        public void ValidatePassword_ShortOrWeakPasswords_ReturnsAppropriateError(string password)
        {
            if (password.Length < 12)
            {
                var result = _validator.ValidatePassword(password);

                Assert.Equal("lblErrShortPassword", result);
            }
            else
            {
                var result = _validator.ValidatePassword(password);

                Assert.Equal("lblErrWeakPassword", result);
            }
        }

        [Theory]
        [InlineData("Strong Pass1!")]
        [InlineData("Another Strong1!")]
        public void ValidatePassword_PasswordContainsWhitespace_ReturnsWhitespaceError(string password)
        {

            var result = _validator.ValidatePassword(password);

            Assert.Equal("lblErrPasswordContainsWhiteSpace", result);
        }

        [Fact]
        public void ValidatePassword_PasswordTooLongButValid_ReturnsEmpty()
        {

            var longPassword = new string('A', 12) + "!a1";

            var result = _validator.ValidatePassword(longPassword);

            Assert.Equal(string.Empty, result);
        }


        [Theory]
        [InlineData("Password123!", "Password123!")]
        [InlineData("AnotherP@ssw0rd", "AnotherP@ssw0rd")]
        public void ValidateConfirmPassword_MatchingPasswords_ReturnsEmpty(string password, string confirmPassword)
        {

            var result = _validator.ValidateConfirmPassword(password, confirmPassword);

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData("Password123!", "Password123")]
        [InlineData("AnotherP@ssw0rd", "AnotherP@ssw0rD")]
        public void ValidateConfirmPassword_NonMatchingPasswords_ReturnsDifferentPasswordError(string password, string confirmPassword)
        {

            var result = _validator.ValidateConfirmPassword(password, confirmPassword);

            Assert.Equal("lblErrDiferentPassword", result);
        }

        [Theory]
        [InlineData("Password123!", "")]
        [InlineData("Password123!", "   ")]
        [InlineData("Password123!", null)]
        public void ValidateConfirmPassword_EmptyOrWhitespace_ReturnsEmptyError(string password, string confirmPassword)
        {

            var result = _validator.ValidateConfirmPassword(password, confirmPassword);

            Assert.Equal("lblErrPasswordEmpty", result);
        }
        [Theory]
        [InlineData("123456")]
        [InlineData("111111")]
        public void IsTokenValidFormat_ValidTokens_ReturnsTrue(string code)
        {

            var result = _validator.IsTokenValidFormat(code);

            Assert.True(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("12345")]
        [InlineData("1234567")]
        [InlineData("1a2b3")]
        [InlineData("1a2b3c4")]
        public void IsTokenValidFormat_InvalidTokens_ReturnsFalse(string code)
        {

            var result = _validator.IsTokenValidFormat(code);

            Assert.False(result);
        }

    }
}
