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

namespace Viking.Tests.TestsPlaymode.GameManager
{
    public class GameManagerViewTests
    {
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();

        private GameManagerView view;
        private Slider slider;
        private TextMeshProUGUI textMeshPro;
        private GameManagerPresenter presenter;
        private GameDataModel _dataModel;

        [SetUp]
        public void Setup()
        {
            // Create a new GameObject and attach GameManagerView component
            GameObject gameObject = new GameObject();
            view = gameObject.AddComponent<GameManagerView>();

            // Create Slider and TextMeshProUGUI components and assign to serialized fields
            slider = gameObject.AddComponent<Slider>();
            textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();

            _getAccessToPrivate.SetPrivateFieldValue(typeof(GameManagerView), view,
                "sliderLifeCharacter", slider);

            _getAccessToPrivate.SetPrivateFieldValue(typeof(GameManagerView), view,
                "monstersKilledText", textMeshPro);

      
        }

        [UnityTest]
        public IEnumerator OnMonsterKilled_CallPresenterMethod()
        {
            // Arrange
            int dataModelMonstersKilled = 0;


            // Act
            view.OnMonsterKilled();
            
            presenter = (GameManagerPresenter)_getAccessToPrivate.GetPrivateFieldValue(typeof(GameManagerView), view,
                "_presenter"); 
        
            _dataModel = (GameDataModel)_getAccessToPrivate.GetPrivateFieldValue(typeof(GameManagerPresenter), presenter,
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
            slider.value = 0; // Set initial value to 0

            // Act
            view.UpdateProgressBar(currentLives);

            // Assert
            Assert.AreEqual(currentLives, slider.value);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UpdateMonstersKilledText_TextMeshProNotNull_UpdateText()
        {
            // Arrange
            int monstersKilled = 5;
            string expectedText = "Monsters Killed: " + monstersKilled;
            textMeshPro.text = ""; // Set initial text to empty

            // Act
            view.UpdateMonstersKilledText(monstersKilled);

            // Assert
            Assert.AreEqual(expectedText, textMeshPro.text);

            yield return null;
        }
    }
}