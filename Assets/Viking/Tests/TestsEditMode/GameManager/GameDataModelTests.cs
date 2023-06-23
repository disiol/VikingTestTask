using NUnit.Framework;
using Viking.Scripts.Game.GameManager.Model;

namespace Viking.Tests.TestsEditMode.GameManager
{
    public class GameDataModelTests
    {
        private GameDataModel gameDataModel;

        [SetUp]
        public void Setup()
        {
            // Create a new instance of GameDataModel before each test
            gameDataModel = new GameDataModel();
        }

        [Test]
        public void MaxLives_SetValue_ShouldSetMaxLives()
        {
            // Arrange
            int expectedMaxLives = 5;

            // Act
            gameDataModel.MaxLives = expectedMaxLives;

            // Assert
            Assert.AreEqual(expectedMaxLives, gameDataModel.MaxLives);
        }

        [Test]
        public void CurrentLives_SetValue_ShouldSetCurrentLivesAndInvokeEvent()
        {
            // Arrange
            int expectedCurrentLives = 3;
            
            int eventInvocationCount = 0;

            // Subscribe to the event and increment the counter when invoked
            gameDataModel.OnCurrentLivesChanged += (lives) => eventInvocationCount++;

            // Act
            gameDataModel.CurrentLives = expectedCurrentLives;

            // Assert
            Assert.AreEqual(expectedCurrentLives, gameDataModel.CurrentLives);
            Assert.AreEqual(1, eventInvocationCount);
        }

        [Test]
        public void MonstersKilled_SetValue_ShouldSetMonstersKilledAndInvokeEvent()
        {
            // Arrange
            int expectedMonstersKilled = 10;
            int eventInvocationCount = 0;

            // Subscribe to the event and increment the counter when invoked
            gameDataModel.OnMonstersKilledChanged += (killed) => eventInvocationCount++;

            // Act
            gameDataModel.MonstersKilled = expectedMonstersKilled;

            // Assert
            Assert.AreEqual(expectedMonstersKilled, gameDataModel.MonstersKilled);
            Assert.AreEqual(1, eventInvocationCount);
        }
    }
}
