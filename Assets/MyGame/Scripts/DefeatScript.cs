using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using NUnit.Framework.Interfaces;

public class DefeatScript : MonoBehaviour
{
   public void Restart()
    {
        HappinessManager.Instance.Restart();
    }
}
