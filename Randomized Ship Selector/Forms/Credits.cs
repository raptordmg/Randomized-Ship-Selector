using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Randomized_Ship_Selector
{
    public partial class Credits : Form
    {
        public Credits()
        {
            InitializeComponent();
        }

        private void llbl_panzerschiffer_forum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.llbl_panzerschiffer_forum.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start(@"https://forum.worldofwarships.com/topic/68159-0702-historical-ensigns-contour-icons/");
        }
    }
}
