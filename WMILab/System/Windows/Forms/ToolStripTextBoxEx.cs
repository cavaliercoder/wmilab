/*
 * Copyright (c) 2014 Ryan Armstrong (www.cavaliercoder.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * The Software shall be used for Good, not Evil.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */
namespace System.Windows.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;

    /// <summary>
    /// Represents a text box in a System.Windows.Forms.ToolStrip with custom extensions that allows the user to enter text.
    /// </summary>
    public class ToolStripTextBoxEx : ToolStripTextBox
    {
        /// <param name="constrainingSize">The custom-sized area for a control.</param>
        /// <returns>An ordered pair of type System.Drawing.Size representing the width and height of a rectangle.</returns>
        public override Size GetPreferredSize(Drawing.Size constrainingSize)
        {
            if(!this.Stretch || this.IsOnOverflow || Owner.Orientation == Orientation.Vertical)
                return base.GetPreferredSize(constrainingSize);

            Int32 width = this.Owner.DisplayRectangle.Width;
            Int32 stretchControlCount = 0;

            if (this.Owner.OverflowButton.Visible)
                width -= this.Owner.OverflowButton.Width + this.Owner.OverflowButton.Margin.Horizontal;

            foreach (ToolStripItem item in this.Owner.Items)
            {
                if (item.IsOnOverflow)
                    continue;

                if (item is ToolStripTextBoxEx)
                {
                    stretchControlCount++;
                    width -= item.Margin.Horizontal;
                }

                else
                {
                    width -= item.Width + item.Margin.Horizontal;
                }
            }

            if (stretchControlCount > 1)
                width /= stretchControlCount;

            if (width < this.DefaultSize.Width)
                width = this.DefaultSize.Width;

            var baseSize = base.GetPreferredSize(constrainingSize);
            baseSize.Width = width;

            return baseSize;
        }

        /// <summary>Gets or set whether a control will automatically size itself to fill its parent container.</summary>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("Specifies whether a control will automatically size itself to fill its parent container.")]
        public Boolean Stretch
        {
            get;
            set;
        }
    }
}
