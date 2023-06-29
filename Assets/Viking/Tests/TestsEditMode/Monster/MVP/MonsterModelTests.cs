using NUnit.Framework;
using Viking.Scripts.Game.Monster.MVP;

namespace Viking.Tests.TestsEditMode.Monster.MVP
{
    public class MonsterModelTests
    {
        [Test]
        public void MonsterHasDamage_DecreasesLives()
        {
            // Arrange
            MonsterModel monster = new MonsterModel();

            int initialLives = monster.Lives;
            
            // Act
            monster.MonsterHasDamage();
            int newLives = monster.Lives;

            // Assert
            int expected = initialLives - 1;
            Assert.AreEqual(expected, newLives,
                "MonsterHasDamage_DecreasesLives  expected  " + expected + " byt newLives " + newLives);
        }

        [Test]
        public void MonsterHasDamage_DeathEventRaisedWhenLivesReachZero()
        {
            // Arrange
            MonsterModel monster = new MonsterModel();
            bool deathEventRaised = false;

            monster.MonsterDeathEvent += () => { deathEventRaised = true; };

            // Act
            // Inflict enough damage to kill the monster
            while (monster.Lives > 0)
            {
                monster.MonsterHasDamage();
            }

            // Assert
            Assert.IsTrue(deathEventRaised);
        }

        [Test]
        public void IncreaseLives_IncrementsLives()
        {
            // Arrange
            MonsterModel monster = new MonsterModel();
            int initialLives = monster.Lives;

            // Act
            monster.IncreaseLives();
            
            // Assert
            int newLives = monster.Lives;

            int expected = initialLives + 1;
            Assert.AreEqual(expected, newLives,
                "IncreaseLives_IncrementsLives  expected  " + expected + " byt newLives " + newLives);
        }
    }
}