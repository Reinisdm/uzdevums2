using NLua;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace uzdevums2
{
    public partial class Form1 : Form
    {
        Lua lua;
        public Form1()
        {
            InitializeComponent();
            this.lua = new Lua();
            lua.DoString(@"
            sin = function(rad) return math.sin(math.rad(rad)) end
            cos = function(rad) return math.cos(math.rad(rad)) end
            sqrt = math.sqrt
            function solve(exp)
                status, res = pcall(load('return '..exp))
                res = tostring(res)
            end
            ");
        }

        private void addtext(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string txt = button.Text;
            if (txt == "cos"){ txt = "cos("; } 
            else if(txt == "sin") { txt = "sin("; }
            else if (txt == "x^y") { txt = "^"; }
            else if (txt == "√") { txt = "√("; }

            if (textBox1.Text.Length + button.Text.Length < 17)
            {
                textBox1.Text += txt;
            }
            textBox1.ForeColor = Color.Black;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string txt = textBox1.Text;
            txt = txt.Replace("x", "*");
            txt = txt.Replace("√", "sqrt");
            txt = txt.Replace(",", ".");
            lua.DoString("solve('" + txt + "')");
            bool success =(bool) lua["status"];
            string res = (string)lua["res"];
            if (success)
            {
                textBox1.Text = res;
            } else {
                textBox1.ForeColor = Color.Red;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0) {
                textBox1.Text = textBox1.Text.Substring(0,textBox1.Text.Length - 1);
            }
            textBox1.ForeColor = Color.Black;
        }
    }
}
