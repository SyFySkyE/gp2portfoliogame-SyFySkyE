using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GridTestScript
    {
        UnityGrid unityGridPrefab;
        Grid dataGrid;
        GameTimer gameTimer;

        public GridTestScript()
        {
            unityGridPrefab = MonoBehaviour.Instantiate(Resources.Load<UnityGrid>("Prefabs/Player One Panel"));
            gameTimer = unityGridPrefab.GetComponentInChildren<GameTimer>();            
        }

        [Test]
        public void ConceptualUnityGridTest()
        {
            // Arrange            
            unityGridPrefab.TestStart();

            // Act


            // Assert

        }

        // A Test behaves as an ordinary method
        [Test]
        public void GridTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions

            // Arrange            

            // Act            

            // Assert
            
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GridTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
