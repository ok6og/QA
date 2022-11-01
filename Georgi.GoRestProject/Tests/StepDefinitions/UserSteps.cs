using System.Net;
using System.Text;
using Georgi.GoRestProject.Core.Config;
using Georgi.GoRestProject.Core.ContextContainers;
using Georgi.GoRestProject.Core.Support;
using Newtonsoft.Json;

namespace Georgi.GoRestProject.StepDefinitions
{
    [Binding]
    public sealed class UserSteps
    {
        private readonly BaseConfig _baseConfig;
        private TestContextContainer _context;
        private HttpResponseMessage _response;
        private StringContent _content;
        private string _url;

        public UserSteps(BaseConfig baseConfig, TestContextContainer context)
        {
            _baseConfig = baseConfig;
            _context = context;
        }

        [Given(@"I prepare a request for (.*) GET")]
        public void GivenIPrepareARequestForUsersGET(string endpoint)
        {
            _url = _baseConfig.HttpClientConfig.BaseUrl + endpoint;
        }

        [When(@"I get all users from the users endpoint")]
        public void WhenIGetAllUsersFromTheUsersEndpoint()
        {
            _response = _context.HttpClient.GetAsync(_url).Result;
        }

        [Then(@"The response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBeOK(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"the response should contain a list of users")]
        public void ThenTheResponseShouldContainAListOfUsers()
        {
            if (_response.StatusCode.ToString() == "NotFound")
            {
                return;
            }
            var content = _response.Content.ReadAsStringAsync().Result;
            var expectedResponse = JsonConvert.DeserializeObject<List<GoRestUser>>(content);
            expectedResponse.Should().NotBeEmpty();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//




        [When(@"I send a request to the (.*) (.*)")]
        public void WhenISendARequestToTheUsersEndpoint(string email,string endpoint)
        {
            _context.GoRestUser.Email += email;
            var json = JsonConvert.SerializeObject(_context.GoRestUser);
            _content = new StringContent(json, Encoding.UTF8, "application/json");
            _response = _context.HttpClient.PostAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}", _content).Result;
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBeCreated(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);

        }

        [Then(@"The user should be created successfully")]
        public void ThenTheUserShouldBeCreatedSuccessfully()
        {
            if ((int)_response.StatusCode/100 == 4)
            {
                return;
            }
            var content = _response.Content.ReadAsStringAsync().Result;
            var expectedResponse = JsonConvert.DeserializeObject<GoRestUser>(content);
            expectedResponse.Should().NotBeNull();
        }



        //--------------------------------------------------------------------------------------------------------------------------//


        [When(@"Update I send a request to the (.*)  update with (.*)")]

        public void WhenISendARequestToTheUsersEndpointUpdate(string endpoint,string email)
        {
            var updateUser = new GoRestRequestUser
            {
                Name = "avsdssddfdfsdram",
                Email = "maonsdasdfsdfsdfiHAHAH@abv.bg" + email,
                Gender = "male",
                Status = "active"
            };

            var json = JsonConvert.SerializeObject(updateUser);
            _content = new StringContent(json, Encoding.UTF8, "application/json");
            _response = _context.HttpClient.PostAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}", _content).Result;

            if ((int)_response.StatusCode / 100 == 4)
            {
                return;
            }
            var userToUpdate = JsonConvert.DeserializeObject<GoRestUser>(_response.Content.ReadAsStringAsync().Result);
            _context.IdToDelete = userToUpdate.Id;

            
        }

        [Then(@"The response status code for update should be (.*)")]
        public void ThenTheResponseStatusCodeForUpdateShouldBeOK(string statusCode)
        {
            var requestBody = new StringContent(JsonConvert.SerializeObject(_context.GoRestUser), Encoding.UTF8, "application/json");
            _response = _context.HttpClient.PutAsync($"{_baseConfig.HttpClientConfig.BaseUrl}users/{_context.IdToDelete}", requestBody).Result;
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"The user should be updaates successfully")]
        public void ThenTheUserShouldBeUpdaatesSuccessfully()
        {
            if ((int)_response.StatusCode / 100 == 4)
            {
                return;
            }
            var content = _response.Content.ReadAsStringAsync().Result;
            var updateUser = JsonConvert.DeserializeObject<GoRestUser>(content);
            updateUser.Name.Should().NotBeEmpty();
            updateUser.Should().NotBeNull();
        }
    }
}