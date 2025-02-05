using System.Collections;
using Taster.DataLoaders;
using UnityEngine;
using UnityEngine.UI;

namespace Taster.Gameplay.Rules
{
    public class HungerTimer : StomachRule
    {
        [SerializeField] int SecondsToEat;
        [SerializeField] Text Label;
        int seconds;
        int Seconds
        {
            get { return seconds; }
            set
            {
                seconds = value;
                Label.text = "" + seconds;
            }
        }

        Eater eater;
        public override void EatenIngredientsChanged(Stomach stomach) => Seconds = SecondsToEat;

        private void Start()
        {
            Seconds = SecondsToEat;
            eater = ServiceLocator.Get<Eater>();

            StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(1f);

                if (eater.CanEatThis)
                {
                    Seconds--;
                    if (Seconds < 0)
                    {
                        Seconds = SecondsToEat;
                        eater.EatSelectedDish();
                    }
                }
            }
        }
    }
}