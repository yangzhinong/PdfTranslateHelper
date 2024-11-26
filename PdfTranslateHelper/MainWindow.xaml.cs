using CefSharp;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PdfTranslateHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Browser.Address = "https://translate.google.com/?hl=zh-cn&sl=auto&tl=zh-CN&op=translate";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s= Clipboard.GetText();
            if (!String.IsNullOrWhiteSpace(s))
            {
                s = s.Replace("\r", " ")
                      .Replace("\n", " ")
                      .Replace("  ", " ");
                Clipboard.SetText(s);
                txt.Text= s;
                Browser.ExecuteScriptAsync("document.querySelector('textarea').value='" + s + "'");
                Browser.Focus();

                KeyboardToolkit.Keyboard.Type(Key.Enter);
            }
        }

        private void txt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) ){ 
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void txt_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                txt.Clear();
                var s= (string) e.Data.GetData(DataFormats.Text);
                s = s.Replace("\r", " ")
                     .Replace("\n", " ")
                     .Replace("  ", " ");
                Clipboard.SetText(s);
                
                txt.Text = s;

                Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(200);
                    this.Dispatcher.Invoke(() =>
                    {
                        Browser.ExecuteScriptAsync("document.querySelector('textarea').value='" + s + "'");
                        Browser.Focus();

                        KeyboardToolkit.Keyboard.Type(Key.Enter);
                    });
                });
            }
        }
        private void btnBaidu_Click(object sender, RoutedEventArgs e)
        {
            Browser.Address = "https://fanyi.baidu.com/mtpe-individual/multimodal";
        }

        private void btnGoogle_Click(object sender, RoutedEventArgs e)
        {
            Browser.Address = "https://translate.google.com/?hl=zh-cn&sl=auto&tl=zh-CN&op=translate";
        }
    }
}
