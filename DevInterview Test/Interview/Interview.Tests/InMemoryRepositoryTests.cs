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
       public void WhenSavedIsCalledShouldAddNewItem()
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

        [Test]

        public  void WhenSavedIsCalledWithUserNullShouldThrowNullException()
        {
            User user = null;

            Assert.Throws(typeof(ArgumentNullException),
                 () => _repository.Save(user));
           
        }

        [Test]
        public void WhenSavedIsCalledWithDuplicateItemsItShouldAddOnlyOneItem()
        {
            var user = new User()
            {
                Id = 1,
                Name = "Shaik",
                EmailAddress = "test@testmail.com"
            };

            _repository.Save(user);
            //Calling to twice to add duplicate item
            _repository.Save(user);


            var result = _repository.All();
            Assert.AreEqual(1,((IEnumerable<User>)result).Count());

        }
    }

    public  class User : IStoreable
    {
        public IComparable Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }
    }
}
