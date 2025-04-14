using System;
using JetBrains.Annotations;
using Modules.Debugging;
using UnityEngine.UIElements;
using VContainer.Unity;
using Button = UnityEngine.UIElements.Button;

namespace Modules.Time
{
    [UsedImplicitly]
    public sealed class TimeDebug : IDebuggable, ITickable
    {
        private readonly ITimeProcessor timeProcessor;
        private readonly ITimeContent timeContent;
        private readonly Label label = new();

        public TimeDebug(ITimeProcessor timeProcessor, ITimeContent timeContent)
        {
            this.timeProcessor = timeProcessor;
            this.timeContent = timeContent;
        }

        public VisualElement CreateDebugElement()
        {
            VisualElement root = new Box();
            Label nameLabel = new Label("Time");
            VisualElement layout = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                }
            };

            IntegerField secondsField = new IntegerField
            {
                label = "Seconds",
                style = { flexDirection = FlexDirection.Column}
            };
            
            IntegerField minutesField = new IntegerField
            {
                label = "Minutes",
                style = { flexDirection = FlexDirection.Column}
            };
            
            IntegerField hoursField = new IntegerField
            {
                label = "Hours",
                style = { flexDirection = FlexDirection.Column}
            };
            
            IntegerField daysField = new IntegerField
            {
                label = "Days",
                style = { flexDirection = FlexDirection.Column}
            };
            Button addButton = new Button();
            Button removeButton = new Button();

            addButton.text = "Add";
            addButton.clicked += () =>
            {
                timeProcessor.AddShift(new TimeSpan(daysField.value, hoursField.value, minutesField.value, secondsField.value));
            };

            removeButton.text = "Remove";
            removeButton.clicked += () =>
            {
                timeProcessor.RemoveShift(new TimeSpan(daysField.value, hoursField.value, minutesField.value, secondsField.value));
            };

            root.Add(nameLabel);
            layout.Add(secondsField);
            layout.Add(minutesField);
            layout.Add(hoursField);
            layout.Add(daysField);
            layout.Add(addButton);
            layout.Add(removeButton);
            root.Add(layout);
            root.Add(label);

            return root;
        }
        
        public void Tick()
        {
            label.text = $"{timeContent.Now:MM/dd/yy H:mm:ss}";
        }
    }
}
