using NUnit.Framework;
using TMPro;
using UnityEngine;
using Viking.Scripts.Game.Monster.MVP;

namespace Viking.Tests.TestsEditMode.Monster.MVP
{
    public class MonsterViewTests
    {
        [Test]
        public void UpdateLivesIndicator_ShouldUpdateText()
        {
            // Arrange
            GameObject gameObject = new GameObject();
            TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
            MonsterView monsterView = gameObject.AddComponent<MonsterView>();
          
            int modelLives = 5;

            // Act
            monsterView.UpdateLivesIndicator(modelLives);

            // Assert
            Assert.AreEqual(modelLives.ToString(), textMeshPro.text);
        }
    }
}