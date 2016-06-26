using NUnit.Framework;
using System;
using System.Linq;
using Visualisation.Core.Responsitories;

namespace Visualisation.Tests.EntityTests
{
	//Should be able to make all of these test abstract. So if I did want to make another IRepository, say for Redis we wouldnt need to rewrite all tests
	public class BaseRepositoryTests
	{
		private MongoDbRepository<TestEntity> _testRepo;

		[SetUp]
		public void SetUp()
		{
			_testRepo = new MongoDbRepository<TestEntity>();
		}

		[OneTimeTearDown]
		public void TestTearDown()
		{
			_testRepo.DeleteAll();
		}

		[Test]
		public void CreateOrUpdate_WhereCreating()
		{
			//arrange
			var testEntity = new TestEntity
			{
				Thing1 = "Dave is the best"
			};

			//act
			_testRepo.CreateOrUpdate(testEntity);

			//assert
			Assert.That(testEntity, Is.Not.Null);
			Assert.That(testEntity.EntityId, Is.Not.EqualTo(Guid.Empty));
		}

		[Test]
		public void CreateOrUpdate_WhereUpdating()
		{
			//arrange
			var testEntity = new TestEntity();
			_testRepo.CreateOrUpdate(testEntity);
			var id = testEntity.EntityId;

			var now = DateTimeOffset.UtcNow;
			testEntity.Thing1 = "Updated Thing";
			testEntity.Integer = 1045;
			testEntity.Now = now;

			//act
			_testRepo.CreateOrUpdate(testEntity);

			//assert
			var actual = _testRepo.GetById(testEntity.EntityId);
			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.EntityId, Is.EqualTo(id), "Id should not have changed when we update the object");
			Assert.That(actual.Thing1, Is.EqualTo("Updated Thing"));
			Assert.That(actual.Integer, Is.EqualTo(1045));
			Assert.That(actual.Now, Is.EqualTo(now));
		}

		[Test]
		public void GetById()
		{
			//arrange
			var testEntity = new TestEntity
			{
				Thing1 = "GetById Thing"
			};

			_testRepo.CreateOrUpdate(testEntity);
			var id = testEntity.EntityId;

			//act
			var actual = _testRepo.GetById(id);

			//assert
			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.EntityId, Is.EqualTo(id));
			Assert.That(actual.Thing1, Is.EqualTo("GetById Thing"));
		}

		[Test]
		public void GetById_WhereEntityDoesntExist()
		{
			//arrange
			var testEntity = new TestEntity
			{
				Thing1 = "GetById Thing"
			};

			_testRepo.CreateOrUpdate(testEntity);

			//act
			var actual = _testRepo.GetById(Guid.NewGuid());

			//assert
			Assert.That(actual, Is.Null);
		}

		[Test]
		public void GetByFilter()
		{
			//arrange
			var now = DateTimeOffset.UtcNow;
			var testEntity = new TestEntity
			{
				Thing1 = "GetByFilter Thing",
				Integer = 24,
				Now = now
			};

			_testRepo.CreateOrUpdate(testEntity);
			var id = testEntity.EntityId;

			//act
			var actual = _testRepo.GetByFilter(x => x.Thing1 == "GetByFilter Thing");

			//assert
			Assert.That(actual, Is.Not.Empty);
			Assert.That(actual, Has.Count.EqualTo(1));

			var entity = actual.First();
			Assert.That(entity.EntityId, Is.EqualTo(id));
			Assert.That(entity.Thing1, Is.EqualTo("GetByFilter Thing"));
			Assert.That(entity.Integer, Is.EqualTo(24));
			Assert.That(entity.Now, Is.EqualTo(now));
		}

		[Test]
		public void Delete()
		{
			//arrange
			var testEntity = new TestEntity
			{
				Thing1 = "Dave is the best"
			};

			_testRepo.CreateOrUpdate(testEntity);
			var id = testEntity.EntityId;

			//act
			_testRepo.Delete(testEntity);

			//assert
			var entity = _testRepo.GetById(id);
			Assert.That(entity, Is.Null);
		}
	}


	public class TestEntity : EntityBase
	{
		public string Thing1 { get; set; }
		public int Integer { get; set; }
		public DateTimeOffset Now { get; set; }

		public TestEntity()
		{
			Integer = new Random().Next(0, 100);
			Now = DateTimeOffset.UtcNow;
		}
	}
}
