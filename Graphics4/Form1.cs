using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            GameWindow gameWindow = new GameWindow();
            Game game = new Game(gameWindow); 
            
        }
    }
}
