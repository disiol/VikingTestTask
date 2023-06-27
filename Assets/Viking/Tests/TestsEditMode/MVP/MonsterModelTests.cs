using NUnit.Framework;
using Viking.Scripts.Game.Monster.MVP;

namespace Viking.Tests.TestsEditMode.MVP
{
    public class MonsterModelTests
    {
        [Test]
        public void MonsterHasDamage_DecreasesLives()
        {
            MonsterModel monster = new MonsterModel();

            int initialLives = monster.Lives;
            monster.MonsterHasDamage();
            int newLives = monster.Lives;

            int expected = initialLives - 1;
            Assert.AreEqual(expected, newLives,
                "MonsterHasDamage_DecreasesLives  expected  " + expected + " byt newLives " + newLives);
        }

        [Test]
        public void MonsterHasDamage_DeathEventRaisedWhenLivesReachZero()
        {
            MonsterModel monster = new MonsterModel();
            bool deathEventRaised = false;

            monster.MonsterDeathEvent += () => { deathEventRaised = true; };

            // Inflict enough damage to kill the monster
            while (monster.Lives > 0)
            {
                monster.MonsterHasDamage();
            }

            Assert.IsTrue(deathEventRaised);
        }

        [Test]
        public void IncreaseLives_IncrementsLives()
        {
            MonsterModel monster = new MonsterModel();

            int initialLives = monster.Lives;
            monster.IncreaseLives();
            int newLives = monster.Lives;

            int expected = initialLives + 1;
            Assert.AreEqual(expected, newLives,
                "IncreaseLives_IncrementsLives  expected  " + expected + " byt newLives " + newLives);
        }
    }
}