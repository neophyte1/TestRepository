using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Interview.Tests
{
    [TestFixture]
    class InMemoryRepositoryTests
    {
        private IRepository<User> _repository;

        public InMemoryRepositoryTests()
        {
           _repository  = new InMemoryRepository<User>();
        }

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
       [ExpectedException(typeof(NotImplementedException))]
        public void WhenSavedIsCalledShouldNewItem()
        {
            var user = new User()
            {
                Id = 1,
                Name = "Shaik",
                EmailAddress = "test@testmail.com"
            };

            _repository.Save(user);

            var result = _repository.All();
            Assert.IsTrue(((IEnumerable<User>)result).Contains(user));

        }
    }

    public  class User : IStoreable
    {
        public IComparable Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }
    }
}
