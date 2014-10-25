using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using Redmine.Portable.Service;
using Redmine.Portable.Model;

namespace Redmine.Portable.Tests
{
    [TestClass]
    public class DataServiceTests
    {
        private const string TEST_ENDPOINT = "";
        private const string TEST_USER = "";
        private const string TEST_PASS = "";

        private DataService CreateTestDataService()
        {
            var dataService = new DataService(new HttpService(), null);
            var credential = new EndpointCredential() 
            {
                EndpointUrl = TEST_ENDPOINT,
                UserName = TEST_USER,
                Password = TEST_PASS
            };
            dataService.Initialize(credential);
            return dataService;
        }

        [TestMethod]
        public async Task GetIssues()
        {
            var dataService = CreateTestDataService();
            var result = await dataService.GetIssues();

            Assert.IsTrue(result.IsSuccessStatusCode);
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Issues);
            Assert.IsTrue(result.Result.Issues.Count() > 0);
        }

        [TestMethod]
        public async Task GetProjects()
        {
            var dataService = CreateTestDataService();
            var result = await dataService.GetProjects();

            Assert.IsTrue(result.IsSuccessStatusCode);
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Projects);
            Assert.IsTrue(result.Result.Projects.Count() > 0);
        }

        [TestMethod]
        public async Task GetUsers()
        {
            var dataService = CreateTestDataService();
            var result = await dataService.GetUsers();

            Assert.IsTrue(result.IsSuccessStatusCode);
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Users);
            Assert.IsTrue(result.Result.Users.Count() > 0);
        }

        [TestMethod]
        public async Task GetCurrentUser()
        {
            var dataService = CreateTestDataService();
            var result = await dataService.GetCurrentUser();

            Assert.IsTrue(result.IsSuccessStatusCode);
            Assert.IsNotNull(result.Result);
        }
    }
}
