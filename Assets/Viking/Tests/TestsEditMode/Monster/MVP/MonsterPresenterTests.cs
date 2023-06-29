using NUnit.Framework;
using Viking.Scripts.Game.Monster.MVP;

namespace Viking.Tests.TestsEditMode.Monster.MVP
{
    public class MonsterPresenterTests
    {
        private MonsterView view;
        private MonsterModel model;
        private MonsterPresenter presenter;

        [SetUp]
        public void Setup()
        {
            view = new MonsterView();
            model = new MonsterModel();
            presenter = new MonsterPresenter(model, view);
        }

        [Test]
        public void MonsterHasDamage_CallsModelMonsterHasDamage()
        {
            // Arrange

            var expected = 0;


            // Act
            presenter.MonsterHasDamage();

            // Assert
            int actual = model.Lives;

            Assert.AreEqual(expected, actual,
                "MonsterHasDamage_CallsModelMonsterHasDamage  expected  " + expected + " byt newLives " + actual);
        }

        [Test]
        public void OnMonsterDeath_CallsModelIncreaseLives()
        {
            // Arrange


            int expected = 2;

            // Act
            presenter.OnMonsterDeath();

            // Assert

            int actual = model.Lives;
            Assert.AreEqual(expected, actual,
                "OnMonsterDeath_CallsModelIncreaseLives  expected  " + expected + " byt newLives " + actual);
        }
    }
}