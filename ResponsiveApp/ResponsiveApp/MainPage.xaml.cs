using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ResponsiveApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
           
			InitializeComponent();
            ByteArrayToImageSourceConverter converter = new ByteArrayToImageSourceConverter();

            byte[] imgB = System.IO.File.ReadAllBytes("checkedimg.png");
            ImageSource imgS= (ImageSource)converter.Convert(imgB, typeof(ImageSource), null, null);

            imgSource.Source = imgS;
            SizeChanged += MainPageSizeChanged;

        }
        void MainPageSizeChanged(object sender, EventArgs e)
        {
            imgSource.WidthRequest =this.Width/5;
            bool isPortrait = this.Height < this.Width;
            One.Orientation = (isPortrait ? StackOrientation.Horizontal : StackOrientation.Vertical);
        }
    }
}
