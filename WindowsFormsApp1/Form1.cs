using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        
        private int _opacity = 50;
        
        public Form1()
        {
            InitializeComponent();
            
            
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      
            /* this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;*/
       
            /*SetStyle(ControlStyles.SupportsTransparentBackColor, true);
              this.BackColor = Color.Transparent;
            //this.BackColor = Color.FromArgb(0, 255, 255, 255);*/
       
            //this.BackColor = Color.LimeGreen;
            // this.TransparencyKey = Color.LimeGreen;

            this.Opacity = (_opacity / 100.0);
            this.TopMost =true;
            this.BackColor = Color.Black;
            this.Location = Point.Empty;
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            
            /*
            Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //Location = new Point(0, 0);
            StartPosition = FormStartPosition.CenterScreen;
            //TopMost = true;
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);*/
        }
        
        [DllImport("user32.dll")] 
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
     
        
        
        protected override CreateParams CreateParams
    {
        get
        {
            const int WS_EX_LAYERED = 0x80000;
            const int WS_EX_TRANSPARENT = 0x20;
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= WS_EX_LAYERED;
            cp.ExStyle |= WS_EX_TRANSPARENT; 
            return cp;
        }
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        /* Ignore */
        e.Graphics.FillRectangle(Brushes.Black, e.ClipRectangle);
        
        Graphics formGraphics = e.Graphics;
        string drawString = _opacity + "%"; //THE TEXT
        Font drawFont = new Font("Arial", 10);
        Color customColor = Color.FromArgb(50, Color.White);
        SolidBrush shadowBrush = new SolidBrush(customColor);
        float x = 20.0F;
        float y = 20.0F;
        StringFormat drawFormat = new StringFormat();
        formGraphics.DrawString(drawString, drawFont, shadowBrush, x, y, drawFormat);
        drawFont.Dispose();
        shadowBrush.Dispose();
        formGraphics.Dispose();
      
    }
    
    

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        //Keys keycode = e.KeyCode;
        //MessageBox.Show(e.KeyCode.ToString());
        //MessageBox.Show(e.KeyValue.ToString());
        switch (e.KeyValue)
        {
            case 107: //add key
               // MessageBox.Show("add");
               //.Show(_opacity + "");
                if (_opacity == 80)
                    return;
                else
                    _opacity += 10;
               this.Opacity = OpacityValue();
                //Application.DoEvents();
                break;
            case 109: //substract key
               // MessageBox.Show("substract");
                if (_opacity == 10 || _opacity < 10)
                    return;
                else
                    _opacity -= 10;
               this.Opacity = OpacityValue();
                break;
            case 0:
                break;
        }

        if (e.KeyCode == Keys.W && e.Modifiers == Keys.Control )
        {
            Application.Exit();
        }
        
    }
    
    
     double OpacityValue()
    {
        Invalidate();
        return (_opacity / 100.0);
    }
        
        
    }
}