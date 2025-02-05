using System;
using System.Collections.Generic;

using UnityEngine;
namespace Taster.Gameplay.Rules
{
	public class GameRule : MonoBehaviour
	{
		public static List<GameRule> All = new();
		private void Awake() => All.Add(this);
        private void OnDestroy() => All.Remove(this);
    }
}