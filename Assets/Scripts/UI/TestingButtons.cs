using UnityEngine;

using KOS.Events;
using KOS.GameplayTracking;

namespace KOS
{
    public class TestingButtons : MonoBehaviour
    {
        public Weapon shotgun, rifle;
        public GameplayTracker gameplayTracker;

        public void Button1()
        {
            Bank.Connect.Deposit(100);
        }

        public void Button2()
        {
            EventManager.Active.ToggleSpawning();
        }

        public void Button3()
        {
            EventManager.Active.ToggleThankYouScreen();
        }

        public void Button4()
        {
            gameplayTracker.SaveStats();
        }

    }
}
