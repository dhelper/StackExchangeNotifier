using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using WindowsPhoneNotifier.ViewModels;
using WindowsPhoneNotifierTests.Fakes;
using StackExchangeClient;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace WindowsPhoneNotifierTests
{


    [TestClass]
    public class UserDetailsViewModelTests
    {
        private User CreateUser(int reputation)
        {
            return new User
            {
                ImageUrl = "http://dummy.jpg",
                Reputation = reputation
            };
        }

        [TestMethod]
        public async Task LoadUser_ReputationStaysTheSame_ReputationTrendSame()
        {
            var user1 = CreateUser(reputation: 10);
            var user2 = CreateUser(reputation: 10);

            var fakeUserRepository = new FakeUserRepository(new[] { user1, user2 });
            var viewModel = await Deployment.Current.Dispatcher.InvokeAsync(() => new UserDetailsViewModel(fakeUserRepository));

            await viewModel.LoadUser();
            await viewModel.LoadUser();

            var result = await Deployment.Current.Dispatcher.InvokeAsync(() => ((SolidColorBrush)viewModel.ReputationTrend).Color);
            Assert.AreEqual(Colors.White, result);
        }

        [TestMethod]
        public async Task LoadUser_ReputationIncrease_ReputationTrendIncrease()
        {
            var user1 = CreateUser(reputation: 10);
            var user2 = CreateUser(reputation: 20);

            var fakeUserRepository = new FakeUserRepository(new[] { user1, user2 });

            await Deployment.Current.Dispatcher.InvokeAsync(async () =>
                {
                    var viewModel = new UserDetailsViewModel(fakeUserRepository);

                    await viewModel.LoadUser();
                    await viewModel.LoadUser();

                    var result = ((SolidColorBrush)viewModel.ReputationTrend).Color;
                    Assert.AreEqual(Colors.Green, result);
                });
        }

        [TestMethod]
        public async Task LoadUser_ReputationDecrease_ReputationTrendDecrease()
        {
            var user1 = CreateUser(reputation: 20);
            var user2 = CreateUser(reputation: 10);

            var fakeUserRepository = new FakeUserRepository(new[] { user1, user2 });

            await Deployment.Current.Dispatcher.InvokeAsync(async () =>
               {
                   var viewModel = await Deployment.Current.Dispatcher.InvokeAsync(() => new UserDetailsViewModel(fakeUserRepository));

                   await viewModel.LoadUser();
                   await viewModel.LoadUser();

                   var result = await Deployment.Current.Dispatcher.InvokeAsync(() => ((SolidColorBrush)viewModel.ReputationTrend).Color);
                   Assert.AreEqual(Colors.Red, result);
               });
        }
    }
}
