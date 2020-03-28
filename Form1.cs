using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class Form1 : Form
    {
        WMPLib.IWMPPlaylist playlist;

        public Form1()
        {
            InitializeComponent();
            playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myplaylist");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = true;
            od.Filter = "(mp3, wav, mp4, mov, wmv, 3gp, mpg, flv) |*.mp3;*wav;*.mp4;*.mov;*.wmv;*3pg;*mpg;*flv | all files | *.*";
            if (od.ShowDialog() == DialogResult.OK)
            {
                if (listBox1.Items.Count > 0)
                {
                    listBox1.Items.Clear();
                    axWindowsMediaPlayer1.currentPlaylist.clear();
                }
                for (int i = 0; i < od.SafeFileNames.Length; i++)
                {

                    listBox1.Items.Add(od.SafeFileNames[i]);
                    WMPLib.IWMPMedia media = axWindowsMediaPlayer1.newMedia(od.FileNames[i]);        
                    
                    playlist.appendItem(media);
                }

            }
            axWindowsMediaPlayer1.currentPlaylist = playlist;
            axWindowsMediaPlayer1.Ctlcontrols.play();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                WMPLib.IWMPMedia media = axWindowsMediaPlayer1.currentPlaylist.get_Item(listBox1.SelectedIndex);
                axWindowsMediaPlayer1.Ctlcontrols.playItem(media);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = true;
            od.Filter = "(mp3, wav, mp4, mov, wmv, 3gp, mpg, flv) |*.mp3;*wav;*.mp4;*.mov;*.wmv;*3pg;*mpg;*flv | all files | *.*";
            if (od.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < od.SafeFileNames.Length; i++)
                {

                    listBox1.Items.Add(od.SafeFileNames[i]);
                    WMPLib.IWMPMedia media = axWindowsMediaPlayer1.newMedia(od.FileNames[i]);

                   axWindowsMediaPlayer1.currentPlaylist.appendItem(media);
                }

            }
        }
        private void axWindowsMediaPlayer1_PlayStateChange(object sender,AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e )
        {
            if (axWindowsMediaPlayer1.currentMedia != null && listBox1.Items.Count <= 0)
            {
                return;
            }
            for(int i =0; i<axWindowsMediaPlayer1.currentPlaylist.count; i++)
            {
                if(axWindowsMediaPlayer1.currentMedia.get_isIdentical(axWindowsMediaPlayer1.currentPlaylist.Item[i]))
                {
                    listBox1.SelectedIndex = i;

                }
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
           Double selected = listBox1.SelectedIndex;
            
            WMPLib.IWMPMedia media = axWindowsMediaPlayer1.currentPlaylist.get_Item(Convert.ToInt32(selected));
           
            axWindowsMediaPlayer1.currentPlaylist.removeItem(media);
            listBox1.Items.RemoveAt(Convert.ToInt32(selected));     
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            
        }
    }
}
