using NSubstitute;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Viking.Scripts.Game.GameManager.Model;
using Viking.Scripts.Game.GameManager.Presenter;
using Viking.Scripts.Game.GameManager.View;
using Viking.Scripts.Tests.TestsPlaymode;

namespace Viking.Tests.TestsEditMode.GameManager
{
    public class GameManagerPresenterTests
    {  
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();

        private Slider slider;
        private TextMeshProUGUI textMeshPro;
        
        private GameManagerPresenter presenter;
        private GameManagerView view;
        private GameDataModel model;

        [SetUp]
        public void Setup()
        {
            GameObject gameObject = new GameObject();
            view = Substitute.For<GameManagerView>();
            slider = gameObject.AddComponent<Slider>();
            textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();

            _getAccessToPrivate.SetPrivateFieldValue(typeof(GameManagerView), view,
                "sliderLifeCharacter", slider);

            _getAccessToPrivate.SetPrivateFieldValue(typeof(GameManagerView), view,
                "monstersKilledText", textMeshPro);

            model = new GameDataModel();
            presenter = new GameManagerPresenter(view);
            presenter.Initialize(5, 3);
        }

        [Test]
        public void OnMonsterKilled_UpdateModelAndUI()
        {
            // Arrange
            int expectedMonstersKilled = 1;

            // Act
            presenter.OnMonsterKilled();

            // Assert
            Assert.AreEqual(expectedMonstersKilled, model.MonstersKilled);
           
            view.Received(1).UpdateMonstersKilledText(expectedMonstersKilled);
        }

        [Test]
        public void OnCurrentLivesChanged_UpdateUI()
        {
            // Arrange
            int expectedCurrentLives = 2;

            // Act
            model.CurrentLives = expectedCurrentLives;

            // Assert
            view.Received(1).UpdateProgressBar(expectedCurrentLives);
        }
    }
}