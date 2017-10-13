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
           _repository  = null;
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
            CreateInMemoryRepositoryInstance();

            _repository.Save(user);

            var result = _repository.All();
            Assert.IsTrue(((IEnumerable<User>)result).Contains(user));

        }

        [Test]

        public  void WhenSavedIsCalledWithUserNullShouldThrowNullException()
        {
            User user = null;
            CreateInMemoryRepositoryInstance();

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
            CreateInMemoryRepositoryInstance();

            _repository.Save(user);
            //Calling to twice to add duplicate item
            _repository.Save(user);
            
            var result = _repository.All();
            Assert.AreEqual(1,((IEnumerable<User>)result).Count());

        }


        [Test]
        public void WhenFindByIdCalledShouldReturnsCorrectItem()
        {
            
            var userOne = new User()
            {
                Id = 1,
                Name = "Martin",
                EmailAddress = "martin@testmail.com"
            };

            var userTwo = new User()
            {
                Id = 3,
                Name = "John",
                EmailAddress = "john@testmail.com"
            };

            CreateInMemoryRepositoryInstance();
            AddItemInMemoryRepository(userOne);
            AddItemInMemoryRepository(userTwo);

            var result = _repository.FindById(userTwo.Id);
            Assert.AreEqual(result, userTwo);
        }

        [Test]
        public void WhenFindByIdCalledWithEmptyListShouldThrowNullException()
        {
            var expectedUser = new User
            {
                Id = 123,
                Name = "Abdul",
                EmailAddress = "abdul@testemail.com"
            };

            Assert.Throws(typeof(NullReferenceException),
                () => _repository.FindById(123));

        }

        private void CreateInMemoryRepositoryInstance()
        {
            _repository = new InMemoryRepository<User>();
        }
        
        private void AddItemInMemoryRepository(User user)
        {
            _repository.Save(user);
        }

    }

    public  class User : IStoreable
    {
        public IComparable Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }
    }
}
