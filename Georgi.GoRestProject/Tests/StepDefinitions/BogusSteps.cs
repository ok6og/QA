using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Georgi.GoRestProject.Core.ContextContainers;
using Georgi.GoRestProject.Core.Support;

namespace Georgi.GoRestProject.Core.Helpers
{
    [Binding]
    public class BogusSteps
    {
        private TestContextContainer _context;

        public BogusSteps(TestContextContainer context)
        {
            _context = context;
        }

        [Given(@"I want to create new user request body")]
        public void GivenIWantToCreateNewUserRequestBody()
        {
            var fakerUser = new Faker<GoRestRequestUserBogus>()
                .RuleFor(u => u.Gender, f => f.PickRandom<Bogus.DataSets.Name.Gender>())
                .RuleFor(u => u.Name, (f, u) => f.Name.FullName(u.Gender))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email())
                .RuleFor(u => u.Status, f => f.PickRandom<Status>().ToString());

            var faker = fakerUser.Generate();
            GoRestRequestUser normalGender = new GoRestRequestUser
            {
                Email = faker.Email,
                Gender = faker.Gender.ToString(),
                Name = faker.Name,
                Status = faker.Status
            };
            _context.GoRestUser = normalGender;
        }

    }
}
