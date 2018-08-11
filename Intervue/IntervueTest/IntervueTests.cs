using Intervue.Models;
using Intervue.Models.ViewModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntervueTest
{
    public class IntervueTests
    {
        [Fact]
        public void CanCheckForLogin()
        {
            LoginViewModel lvm = new LoginViewModel();
            lvm.Email = "doge@doggy.com";

            Assert.NotNull(lvm.Email);
        }

        [Fact]
        public void CanRegisterUser()
        {
            RegistrationViewModel rvm = new RegistrationViewModel();
            rvm.Email = "doge@doggy.com";

            Assert.NotNull(rvm.Email);
        }

        [Fact]
        public void CanCheckForPromptMessage()
        {
            SpeechViewModel svm = new SpeechViewModel();
            svm.PromptMessage = "Speak.";

            Assert.Equal("Speak.", svm.PromptMessage);
        }

        [Fact]
        public void CanCheckForError()
        {
            ErrorViewModel evm = new ErrorViewModel();
            evm.RequestId = "1";

            Assert.Equal("1", evm.RequestId);
        }
    }
}
