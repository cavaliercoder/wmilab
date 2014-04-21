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
    using System.Collections;
    using System.Collections.Generic;

    public class ToolStripItemSorter : IComparer<ToolStripItem>
    {
        /// <summary>
        /// Sorts a ToolStripItemCollection by the Text property of each ToolStripItem.
        /// Sorting is alphanumeric and case insensitive.
        /// </summary>
        /// <param name="items">The System.Windows.Forms.ToolStripItemCollection to sort.</param>
        public static void Sort(ToolStripItemCollection items)
        {
            List<ToolStripMenuItem> itemList = new List<ToolStripMenuItem>(items.Count);
            ToolStripItem[] itemArray = new ToolStripItem[items.Count];
            items.CopyTo(itemArray, 0);

            Array.Sort<ToolStripItem>(itemArray, new ToolStripItemSorter());
            items.Clear();
            items.AddRange(itemArray);
        }

        /// <summary>
        /// Performs a case-insensitive comparison of the Text property of two ToolStripItems
        /// and returns a value indicating whether one is less than, equal to, or greater
        /// than the other.
        /// </summary>
        /// <param name="x">The first System.Windows.Forms.ToolStripItem to compare.</param>
        /// <param name="y">The second System.Windows.Forms.ToolStripItem to compare.</param>
        /// <returns>
        /// Value Condition Less than zero a is less than b, with casing ignored. Zero
        /// a equals b, with casing ignored. Greater than zero a is greater than b, with
        /// casing ignored.
        /// </returns>
        public int Compare(ToolStripItem x, ToolStripItem y)
        {
            CaseInsensitiveComparer comparer = new CaseInsensitiveComparer();
            return comparer.Compare(x.Text, y.Text);
        }
    }

    public static class ToolStripItemCollectionExtentions
    {
        /// <summary>Sorts a ToolStripItemCollection by the Text property of each ToolStripItem.</summary>
        /// <param name="items">The System.Windows.Forms.ToolStripItemCollection to sort.</param>
        /// <remarks>Sorting is alphanumeric and case insensitive.</remarks>
        public static void Sort(this ToolStripItemCollection items)
        {
            ToolStripItemSorter.Sort(items);
        }
    }
}
