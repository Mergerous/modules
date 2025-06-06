using System;
using UnityEngine;

namespace Modules.Common.Editor
{
    public static class EditorHelper
    {
        public static void DrawHorizontal(Rect rect, params Action<Rect>[] callbacks)
        {
            float width = rect.width;
            rect.width /= callbacks.Length;
            foreach (Action<Rect> callback in callbacks)
            {
                callback?.Invoke(rect);
                rect.x += width / callbacks.Length;
            }
        }
    }
}