// =============================================================================
// MIT License
// 
// Copyright (c) 2018 Valeriya Pudova (hww.github.io)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// =============================================================================

using System;

namespace VARP.DebugMenus
{
    public class DebugMenuAction : DebugMenuItem
    {
        private readonly Action<DebugMenuAction, EvenTag> action;

        public DebugMenuAction(string path, Action<DebugMenuAction, EvenTag> action, int order = 0)
            : base(path, order)
        {
            this.action = action;
            value = null;    // do not have value, wil display it by color
        }
        
        public DebugMenuAction(DebugMenu parentMenu, string label, Action<DebugMenuAction, EvenTag> action = null, int order = 0)
            : base(parentMenu, label, order)
        {
            this.action = action;
            value = null;    // do not have value, wil display it by color
        }

        
        public override void OnEvent(EvenTag tag)
        {
            switch (tag)
            {
                case EvenTag.Render:
                    Render();
                    break;
                case EvenTag.Left:
                    action(this, tag);
                    break;
                case EvenTag.Right:
                    action(this, tag);
                    break;
                case EvenTag.Reset:
                    action(this, tag);
                    break;
            }
        }

        private void Render()
        {
            labelColor = action != null ? Colors.ToggleLabelDisabled : Colors.LabelModified;
        }
    }
    
}