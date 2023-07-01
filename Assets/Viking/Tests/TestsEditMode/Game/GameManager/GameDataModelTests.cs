using NUnit.Framework;
using Viking.Scripts.Game.GameManager.Model;

namespace Viking.Tests.TestsEditMode.Game.GameManager
{
    public class GameDataModelTests
    {
        private GameDataModel _gameDataModel;

        [SetUp]
        public void Setup()
        {
            // Create a new instance of GameDataModel before each test
            _gameDataModel = new GameDataModel();
        }

        [Test]
        public void MaxLives_SetValue_ShouldSetMaxLives()
        {
            // Arrange
            int expectedMaxLives = 5;

            // Act
            _gameDataModel.MaxLives = expectedMaxLives;

            // Assert
            Assert.AreEqual(expectedMaxLives, _gameDataModel.MaxLives);
        }

        [Test]
        public void CurrentLives_SetValue_ShouldSetCurrentLivesAndInvokeEvent()
        {
            // Arrange
            int expectedCurrentLives = 3;
            
            int eventInvocationCount = 0;

            // Subscribe to the event and increment the counter when invoked
            _gameDataModel.OnCurrentLivesChanged += (lives) => eventInvocationCount++;

            // Act
            _gameDataModel.CurrentLives = expectedCurrentLives;

            // Assert
            Assert.AreEqual(expectedCurrentLives, _gameDataModel.CurrentLives);
            Assert.AreEqual(1, eventInvocationCount);
        }

        [Test]
        public void MonstersKilled_SetValue_ShouldSetMonstersKilledAndInvokeEvent()
        {
            // Arrange
            int expectedMonstersKilled = 10;
            int eventInvocationCount = 0;

            // Subscribe to the event and increment the counter when invoked
            _gameDataModel.OnMonstersKilledChanged += (killed) => eventInvocationCount++;

            // Act
            _gameDataModel.MonstersKilled = expectedMonstersKilled;

            // Assert
            Assert.AreEqual(expectedMonstersKilled, _gameDataModel.MonstersKilled);
            Assert.AreEqual(1, eventInvocationCount);
        }
    }
}
