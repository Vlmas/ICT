using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class FormApp : Form
    {
        readonly Brain brain;

        public FormApp()
        {
            InitializeComponent();
            brain = new Brain(new DisplayMessage(SetDisplayMessage));
        }

        public void SetDisplayMessage(string text)
        {
            displayArea.Text = text;
        }

        public void ButtonClicked(object sender, EventArgs args) 
        {
            Button button = sender as Button;
            brain.ProcessSignal(button.Text);
        }
    }
}
