using System;
using System.Text;
using Modules.Common.Extensions;
using Modules.Loop.Interfaces;

namespace Modules.Loop.Structures
{
    public class Typer : IUpdatable
    {
        private int letterIndex;
        private bool isGenerating;
        private bool tick = true;
        private float interval;
        private float time;
        private int cycle;
        private string text;
        private int loops;
        private int startIndex;
        private Action<string> callback;
        private StringBuilder builder = new StringBuilder();

        public Typer(string text, float interval, Action<string> callback, int loops = 1, string substringKeyword = "<sub>")
        {
            this.loops = loops;
            this.text = text;
            this.interval = interval;
            this.callback = callback;
            if (text.Contains(substringKeyword))
            {
                letterIndex = startIndex = text.IndexOf(substringKeyword, StringComparison.InvariantCulture) + substringKeyword.Length;
                builder.Append(text.Substring(0, startIndex));
            }
        }

        // TODO ADD ALPHA METHOD
        public void Update(float deltaTime)
        {
            if (text.IsNullOrEmpty() || letterIndex >= text.Length)
            {
                letterIndex = startIndex;
                builder.Clear();
                builder.Append(text.Substring(0, startIndex));
                if (++cycle >= loops && loops >= 0)
                {
                    cycle = 0;
                    this.Stop();
                    return;
                }
            }

            if (tick)
            {
                builder.Append(text[letterIndex]);
                callback?.Invoke(builder.ToString());
            }
        
            if (time >= interval)
            {
                letterIndex++;
                time = 0;
                tick = true;
            }
            else
            {
                time += deltaTime;
                tick = false;
            }
        }
    }
}
