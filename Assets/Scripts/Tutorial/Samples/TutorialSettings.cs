using System.Collections.Generic;
using UnityEngine;

namespace Modules.Tutorial
{
    [CreateAssetMenu]
    public sealed class TutorialSettings : ScriptableObject
    {
        [SerializeField] private TutorialStep[] steps;

        public IReadOnlyList<TutorialStep> Steps => steps;
    }
}