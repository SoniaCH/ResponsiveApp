using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ResponsiveApp
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource retSource = null;
            try
            {
                if (value != null)
                {
                    byte[] imageAsBytes = (byte[])value;
                    var stream = new MemoryStream(imageAsBytes);
                    retSource = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception e)
            {

                if (Debugger.IsAttached)
                {
                    Debug.WriteLine(e);
                }
                else
                {
                    Console.WriteLine(e);
                }
            }
            return retSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] data = null;

            if (value != null)
            {
                try
                {
                    StreamImageSource streamImageSource = (StreamImageSource)(ImageSource)value;
                    CancellationToken cancellationToken = CancellationToken.None;
                    Task<Stream> task = streamImageSource.Stream(cancellationToken);
                    Stream stream = Stream.Null;
                    //stream.InitializeLifetimeService();
                    stream = task.Result;
                    MemoryStream ms = new MemoryStream();
                    //ms.InitializeLifetimeService();
                    stream.CopyTo(ms);
                    data = ms.ToArray();
                    return data;
                }
                catch (Exception e)
                {
                    if (Debugger.IsAttached)
                    {
                        Debug.WriteLine(e);
                    }
                    else
                    {
                        Console.WriteLine(e);
                    }

                }
            }
            else
            {
                return null;
            }

            return data;
        }
    
    }
}
