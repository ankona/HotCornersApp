using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotCornersApp {
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window {
        public About() {
            InitializeComponent();
            browser.NavigateToString("&lt;html&gt;& lt; img src = &quot; https://www.google.com/chart?chc=sites&amp;amp;cht=d&amp;amp;chdp=sites&amp;amp;chl=%5B%5BGoogle+Gadget'%3D20'f%5Cv'a%5C%3D0'10'%3D159'0'dim'%5Cbox1'b%5CF6F6F6'fC%5CF6F6F6'eC%5C0'sk'%5C%5B%22Embed+gadget%22'%5D'a%5CV%5C%3D12'f%5C%5DV%5Cta%5C%3D10'%3D0'%3D160'%3D57'dim'%5C%3D10'%3D10'%3D160'%3D57'vdim'%5Cbox1'b%5Cva%5CF6F6F6'fC%5CC8C8C8'eC%5C'a%5C%5Do%5CLauto'f%5C&amp;amp;sig=t6grYG94BexfiXh0ivl49Ti7cUY&quot; data-igsrc=&quot;http://5.gmodules.com/ig/ifr?mid=5&amp;amp;synd=trogedit&amp;amp;url=http%3A%2F%2Fwww.gstatic.com%2Fsites-gadgets%2Fembed%2Fembed.xml&amp;amp;up_embed_snippet=%3Cform%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F1999%2Fxhtml%22%20action%3D%22https%3A%2F%2Fwww.paypal.com%2Fcgi-bin%2Fwebscr%22%20method%3D%22post%22%20target%3D%22_top%22%3E%0A%3Cinput%20name%3D%22cmd%22%20type%3D%22hidden%22%20value%3D%22_s-xclick%22%20%2F%3E%0A%3Cinput%20name%3D%22hosted_button_id%22%20type%3D%22hidden%22%20value%3D%22Z4RSWA4FAVK3A%22%20%2F%3E%0A%3Cinput%20alt%3D%22PayPal%20-%20The%20safer%2C%20easier%20way%20to%20pay%20online!%22%20border%3D%220%22%20name%3D%22submit%22%20src%3D%22https%3A%2F%2Fwww.paypalobjects.com%2Fen_US%2Fi%2Fbtn%2Fbtn_donateCC_LG.gif%22%20type%3D%22image%22%20%2F%3E%0A%3Cimg%20alt%3D%22%22%20border%3D%220%22%20height%3D%221%22%20src%3D%22https%3A%2F%2Fwww.paypalobjects.com%2Fen_US%2Fi%2Fscr%2Fpixel.gif%22%20width%3D%221%22%20%2F%3E%0A%3C%2Fform%3E&amp;amp;h=60&amp;amp;w=160&quot; data-type=&quot;ggs-gadget&quot; data-props=&quot;align:center;borderTitle:Embed gadget;height:60;igsrc:http#58//5.gmodules.com/ig/ifr?mid=5&amp;amp;synd=trogedit&amp;amp;url=http%3A%2F%2Fwww.gstatic.com%2Fsites-gadgets%2Fembed%2Fembed.xml&amp;amp;up_embed_snippet=%3Cform%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F1999%2Fxhtml%22%20action%3D%22https%3A%2F%2Fwww.paypal.com%2Fcgi-bin%2Fwebscr%22%20method%3D%22post%22%20target%3D%22_top%22%3E%0A%3Cinput%20name%3D%22cmd%22%20type%3D%22hidden%22%20value%3D%22_s-xclick%22%20%2F%3E%0A%3Cinput%20name%3D%22hosted_button_id%22%20type%3D%22hidden%22%20value%3D%22Z4RSWA4FAVK3A%22%20%2F%3E%0A%3Cinput%20alt%3D%22PayPal%20-%20The%20safer%2C%20easier%20way%20to%20pay%20online!%22%20border%3D%220%22%20name%3D%22submit%22%20src%3D%22https%3A%2F%2Fwww.paypalobjects.com%2Fen_US%2Fi%2Fbtn%2Fbtn_donateCC_LG.gif%22%20type%3D%22image%22%20%2F%3E%0A%3Cimg%20alt%3D%22%22%20border%3D%220%22%20height%3D%221%22%20src%3D%22https%3A%2F%2Fwww.paypalobjects.com%2Fen_US%2Fi%2Fscr%2Fpixel.gif%22%20width%3D%221%22%20%2F%3E%0A%3C%2Fform%3E&amp;amp;h=60&amp;amp;w=160;mid:5;scrolling:no;showBorder:false;showBorderTitle:null;spec:http#58//www.gstatic.com/sites-gadgets/embed/embed.xml;up_embed_snippet:&lt;form xmlns=&amp;quot;http#58//www.w3.org/1999/xhtml&amp;quot; action=&amp;quot;https#58//www.paypal.com/cgi-bin/webscr&amp;quot; method=&amp;quot;post&amp;quot; target=&amp;quot;_top&amp;quot;&gt; &lt;input name=&amp;quot;cmd&amp;quot; type=&amp;quot;hidden&amp;quot; value=&amp;quot;_s-xclick&amp;quot; /&gt; &lt;input name=&amp;quot;hosted_button_id&amp;quot; type=&amp;quot;hidden&amp;quot; value=&amp;quot;Z4RSWA4FAVK3A&amp;quot; /&gt; &lt;input alt=&amp;quot;PayPal - The safer, easier way to pay online!&amp;quot; border=&amp;quot;0&amp;quot; name=&amp;quot;submit&amp;quot; src=&amp;quot;https#58//www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif&amp;quot; type=&amp;quot;image&amp;quot; /&gt; &lt;img alt=&amp;quot;&amp;quot; border=&amp;quot;0&amp;quot; height=&amp;quot;1&amp;quot; src=&amp;quot;https#58//www.paypalobjects.com/en_US/i/scr/pixel.gif&amp;quot; width=&amp;quot;1&amp;quot; /&gt; &lt;/form&gt;; width: 160; wrap: false; &quot; width = &quot; 160 & quot; height = &quot; 60 & quot; style = &quot; display: block; margin: 5px auto; text - align:center; &quot; class=&quot;igm&quot;&gt;&lt;/html&gt;");
        }
        public void appClose(object sender, EventArgs e) {
            this.Close();
        }
    }
}
