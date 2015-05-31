using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhoIsSpeaking
{
    public class MyListBox : ListBox
    {
        private Font boldFont;

        public MyListBox()
            : base()
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
            boldFont = new Font(this.Font, FontStyle.Bold);
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

         protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (this.Items.Count > 0)
            {
                e.DrawBackground(); 
                if (this.Items[e.Index].ToString().Contains("*"))
                    e.Graphics.DrawString(this.Items[e.Index].ToString(), boldFont, new SolidBrush(Color.DarkGreen), new PointF(e.Bounds.X, e.Bounds.Y));
                else if (this.Items[e.Index].ToString().Contains("     "))
                    e.Graphics.DrawString(this.Items[e.Index].ToString(), boldFont, new SolidBrush(this.ForeColor), new PointF(e.Bounds.X, e.Bounds.Y));
                else
                    e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, new SolidBrush(this.ForeColor), new PointF(e.Bounds.X, e.Bounds.Y));
            }
            base.OnDrawItem(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Region iRegion = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(new SolidBrush(this.BackColor), iRegion);
            if (this.Items.Count > 0)
            {
                for (int i = 0; i < this.Items.Count; ++i)
                {
                    System.Drawing.Rectangle irect = this.GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(irect))
                    {
                        if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i)
                        || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i))
                        || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Selected, this.ForeColor,
                                this.BackColor));
                        }
                        else
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Default, this.ForeColor,
                                this.BackColor));
                        }
                        iRegion.Complement(irect);
                    }
                }
            }
            base.OnPaint(e);
        }
    }    
}
