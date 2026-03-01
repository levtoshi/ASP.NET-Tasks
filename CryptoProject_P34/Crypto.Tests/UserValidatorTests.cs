namespace Crypto.Tests
{
    public class UserValidatorTests
    {
        private readonly UserValidator _validator = new UserValidator();

        [Theory]
        [InlineData("User")]
        [InlineData("User_123")]
        [InlineData("abcDEF_456")]
        public void ValidateName_ValidNames_ReturnsTrue(string name)
        {
            var result = _validator.ValidateName(name);
            Assert.True(result);
        }

        [Theory]
        [InlineData("User!")]
        [InlineData("User@123")]
        [InlineData("User name")]
        [InlineData("User-123")]
        [InlineData("")]
        [InlineData(null)]
        public void ValidateName_InvalidNames_ReturnsFalse(string name)
        {
            var result = _validator.ValidateName(name);
            Assert.False(result);
        }


        [Fact]
        public void ValidatePassword_ValidPassword_ReturnsTrue()
        {
            string password = "Abcdef!IV♌";
            var result = _validator.ValidatePassword(password);
            Assert.True(result);
        }

        [Fact]
        public void ValidatePassword_TooShort_ReturnsFalse()
        {
            string password = "Ab!IV♌";
            var result = _validator.ValidatePassword(password);
            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_NoUpperCase_ReturnsFalse()
        {
            string password = "abcdef!♌";
            var result = _validator.ValidatePassword(password);
            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_NoLowerCase_ReturnsFalse()
        {
            string password = "ABCDEF!IV♌";
            var result = _validator.ValidatePassword(password);
            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_NoSpecialSymbol_ReturnsFalse()
        {
            string password = "AbcdefIV♌";
            var result = _validator.ValidatePassword(password);
            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_NoZodiacSign_ReturnsFalse()
        {
            string password = "Abcdef!IV";
            var result = _validator.ValidatePassword(password);
            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_NoRomanNumber_ReturnsFalse()
        {
            string password = "Abcdef!♌";
            var result = _validator.ValidatePassword(password);
            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_Empty_ReturnsFalse()
        {
            string password = "";
            var result = _validator.ValidatePassword(password);
            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_Null_ReturnsFalse()
        {
            string password = null;
            var result = _validator.ValidatePassword(password);
            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_WrongSpecialSymbol_ReturnsFalse()
        {
            string password = "Abcdef#IV♌";
            var result = _validator.ValidatePassword(password);
            Assert.False(result);
        }

        [Theory]
        [InlineData("Abcdef!I♈")]
        [InlineData("Password@X♓")]
        [InlineData("Secure%M♏")]
        public void ValidatePassword_MultipleValidPasswords_ReturnsTrue(string password)
        {
            var result = _validator.ValidatePassword(password);
            Assert.True(result);
        }
    }
}