using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Viking.Scripts.Tests.TestsPlaymode;
using Viking.Scripts.UI.StartScreen;

namespace Viking.Tests.TestsPlaymode
{
    public class StartScreenViewTests
    {
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();

        private StartScreenView _startScreenView;
        private GameObject _gameObjectStartScreenView;
        private GameObject _game;

        [SetUp]
        public void SetUp()
        {
            // Create a new GameObject to hold the button
            Button playButton = new GameObject("playButton").AddComponent<Button>();
            Button exitButton = new GameObject("exitButton").AddComponent<Button>();

            // Set the parent of the button object (e.g., a canvas)

            _gameObjectStartScreenView = new GameObject();

            _startScreenView = _gameObjectStartScreenView.AddComponent<StartScreenView>();

            _getAccessToPrivate.SetPrivateFieldValue(typeof(StartScreenView), _startScreenView,
                "playButton", playButton);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(StartScreenView), _startScreenView,
                "exitButton", exitButton);
            
            
             _game = new GameObject();
            _game.name = "Game";
            
            _getAccessToPrivate.SetPrivateFieldValue(typeof(StartScreenView), _startScreenView,
                "game", _game);

        }


        [UnityTest]
        public IEnumerator PlayButton_Clicked_LoadsGameScene()
        {
            // Arrange
           
            _game.SetActive(false);


            // Act
            _getAccessToPrivate.GetPrivateMethod(typeof(StartScreenView),_startScreenView, "OnPlayButtonClick");

            // Wait for the scene to load
            yield return new WaitForSeconds(1f);

            // Assert
            Assert.IsTrue(_game.activeSelf, " is gameObjectGame active");
            Assert.IsFalse(_gameObjectStartScreenView.activeSelf, " is _gameObjectStartScreenView active");
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up after each test
            Object.Destroy(_gameObjectStartScreenView);
        }
      

        
     
    }
}