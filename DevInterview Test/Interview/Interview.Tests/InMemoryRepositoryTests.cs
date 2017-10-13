﻿using System;
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
            _repository = null;
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

        public void WhenSavedIsCalledWithUserNullShouldThrowNullException()
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
            Assert.AreEqual(1, ((IEnumerable<User>)result).Count());

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
        public void WhenFindByIdCalledWithListIsEmptyOrNullShouldThrowException()
        {
            var user = new User
            {
                Id = 123,
                Name = "Abdul",
                EmailAddress = "abdul@testemail.com"
            };

            CreateInMemoryRepositoryInstance();

            var exception = Assert.Throws<Exception>(
                () => _repository.FindById(user.Id));

            Assert.That(exception.Message, Is.EqualTo("Items required in the List."));
        }

        [Test]
        public void WhenDeleteIsCalledShouldRemoveExistingItemFromTheStore()
        {
            CreateInMemoryRepositoryInstance();
            var user = new User
            {
                Id = 3,
                Name = "Abdul",
                EmailAddress = "TestEmail@test.com"
            };

            AddItemInMemoryRepository(user);

            _repository.Delete(user.Id);
            var result = (IEnumerable<User>)GetAllUsers();

            Assert.IsFalse(result.Contains(user));
        }

        [Test]
        public void WhenDeleteIsCalledWithListIsEmptyOrNullShouldThrowException()
        {
            var user = new User
            {
                Id = 456,
                Name = "Abdul",
                EmailAddress = "abdul@testemail.com"
            };

            CreateInMemoryRepositoryInstance();

            var exception = Assert.Throws<Exception>(
                () => _repository.Delete(user.Id));

            Assert.That(exception.Message, Is.EqualTo("Items should exist in the list before you invoke Delete()."));
        }

        [Test]
        [Ignore("Ignore this test, this should be run with uninitialised or null List")]
        public void WhenAllIsCalledShouldThrowAnExceptionIfListIsNull()
        {
            CreateInMemoryRepositoryInstance();
            Assert.Throws<NullReferenceException>(() => _repository.All());
        }

        [Test]
        public void WhenAllIsCalledWithEmptyListShouldReturn0()
        {
            CreateInMemoryRepositoryInstance();

            var result = _repository.All();

            Assert.AreEqual(result.Count(), 0);
        }

        private void CreateInMemoryRepositoryInstance()
        {
            _repository = new InMemoryRepository<User>();
        }

        private void AddItemInMemoryRepository(User user)
        {
            _repository.Save(user);
        }


        private IEnumerable<IStoreable> GetAllUsers()
        {
            return _repository.All();
        }

    }

    public class User : IStoreable
    {
        public IComparable Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }
    }
}
