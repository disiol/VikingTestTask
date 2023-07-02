using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Viking.Scripts.Game.GameManager.Model;
using Viking.Scripts.Game.GameManager.Presenter;
using Viking.Scripts.Game.GameManager.View;
using Viking.Scripts.Tests.TestsPlaymode;

namespace Viking.Tests.TestsPlaymode.Game.GameManager
{
    public class GameManagerViewTests
    {
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();

        private GameManagerView _view;
        private Slider _slider;
        private TextMeshProUGUI _textMeshProUGUI;
        private GameManagerPresenter _presenter;
        private GameDataModel _dataModel;

        [SetUp]
        public void Setup()
        {
            // Create a new GameObject and attach GameManagerView component
            GameObject gameObject = new GameObject();
            _view = gameObject.AddComponent<GameManagerView>();

            // Create Slider and TextMeshProUGUI components and assign to serialized fields
            _slider = gameObject.AddComponent<Slider>();
            _textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();

            _getAccessToPrivate.SetPrivateFieldValue(typeof(GameManagerView), _view,
                "sliderLifeCharacter", _slider);

            _getAccessToPrivate.SetPrivateFieldValue(typeof(GameManagerView), _view,
                "monstersKilledText", _textMeshProUGUI);

      
        }

        [UnityTest]
        public IEnumerator OnMonsterKilled_CallPresenterMethod()
        {
            // Arrange
            int dataModelMonstersKilled = 0;


            // Act
            _view.OnMonsterKilled();
            
            _presenter = (GameManagerPresenter)_getAccessToPrivate.GetPrivateFieldValue(typeof(GameManagerView), _view,
                "_presenter"); 
        
            _dataModel = (GameDataModel)_getAccessToPrivate.GetPrivateFieldValue(typeof(GameManagerPresenter), _presenter,
                "_model");
            // Assert
            int newDataModelMonstersKilled = _dataModel.MonstersKilled;

            Assert.Greater(newDataModelMonstersKilled,dataModelMonstersKilled);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UpdateProgressBar_SliderNotNull_UpdateSliderValue()
        {
            // Arrange
            int currentLives = 2;
            _slider.value = 0; // Set initial value to 0

            // Act
            _view.UpdatesSliderLife(currentLives);

            // Assert
            Assert.AreEqual(currentLives, _slider.value);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UpdateMonstersKilledText_TextMeshProNotNull_UpdateText()
        {
            // Arrange
            int monstersKilled = 5;
            string expectedText = "Monsters Killed: " + monstersKilled;
            _textMeshProUGUI.text = ""; // Set initial text to empty

            // Act
            _view.UpdateMonstersKilledText(monstersKilled);

            // Assert
            Assert.AreEqual(expectedText, _textMeshProUGUI.text);

            yield return null;
        }
    }
}