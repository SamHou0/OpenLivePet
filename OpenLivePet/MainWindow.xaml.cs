using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Live2DCSharpSDK.WPF;
using Microsoft.Win32;
using OpenLivePet.Tools;

namespace OpenLivePet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer mouseTimer = new();
        string path = "";
        Live2DWPFModel model;
        public MainWindow()
        {
            InitializeComponent();
            LoadModelPath();
            model = new Live2DWPFModel(path);
            LiveDisplay.Child = model.GLControl;
            model.Start();

            mouseTimer.Tick += MouseTimer_Tick;
            mouseTimer.Interval = new TimeSpan(500);
            mouseTimer.Start();
        }

        private void LoadModelPath()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.Live2DModelPath))
            {
                OpenFileDialog dialog = new();
                dialog.Filter = "moc3|*.moc3";
                dialog.ShowDialog();
                path = dialog.FileName;
                if (path == "")
                {
                    Environment.Exit(1);
                }
                else
                {
                    Properties.Settings.Default.Live2DModelPath = path;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                path = Properties.Settings.Default.Live2DModelPath;
            }
        }

        private void MouseTimer_Tick(object? sender, EventArgs e)
        {
            Point screenPoint = Tools.Mouse.GetCursorPosition();
            Point relativePoint = LiveDisplay.PointFromScreen(screenPoint);
            model.LModel.SetDragging((float)(relativePoint.X / LiveDisplay.ActualWidth * 2 - 1),
                -(float)(relativePoint.Y / LiveDisplay.ActualHeight * 2 - 0.5));
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            mouseTimer.Stop();
            Close();
        }
    }
}