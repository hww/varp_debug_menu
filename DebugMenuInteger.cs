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
    public class DebugMenuInteger : DebugMenuItem
    {
        private readonly Func<int> getter;
        private readonly Action<int> setter;
        private readonly string format;
        private readonly int increment;
        private int defaultValue;
        
        public DebugMenuInteger(string path, Func<int> getter, Action<int> setter  = null, int order = 0,
            int increment = 1, string format = null)
            : base(path, order)
        {
            this.getter = getter;
            this.setter = setter;
            this.format = format ?? "0";
            this.increment = increment;
            this.defaultValue = getter();
            Render();
        }
        
        public override void OnEvent(DebugMenuC sender, EvenTag tag)
        {
            switch (tag)
            {
                case EvenTag.Render:
                    Render();
                    break;
                case EvenTag.Dec:
                    setter?.Invoke(getter() + increment);
                    Render();
                    break;
                case EvenTag.Inc:
                    setter?.Invoke(getter() - increment);
                    Render();
                    break;
                case EvenTag.Reset:
                    setter?.Invoke(defaultValue);
                    Render();
                    break;
            }
        }

        private void Render()
        {
            var val = getter();
            var def = val == defaultValue;
            value = val.ToString(format);
            valueColor = def ? Colors.ValueDefault : Colors.ValueModified;
            labelColor = def ? Colors.LabelDefault : Colors.LabelModified;
        }
    }
}