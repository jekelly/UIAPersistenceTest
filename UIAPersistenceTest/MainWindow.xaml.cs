namespace UIAPersistenceTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int AutomationPropertyCreatedCount {
            get { return (int)GetValue(AutomationPropertyCreatedCountProperty); }
            set { SetValue(AutomationPropertyCreatedCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutomationPropertyCreatedCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutomationPropertyCreatedCountProperty =
            DependencyProperty.Register("AutomationPropertyCreatedCount", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            this.AutomationPropertyCreatedCount++;
            return base.OnCreateAutomationPeer();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var wrapper = new WindowInteropHelper(Application.Current.MainWindow);

            SendMessage(wrapper.Handle, 0x003D, 0, null); // WM_GETOBJECT

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, StringBuilder lParam);

        private List<Control> controls = new List<Control>();
        Random r = new Random();

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            while(true)
            {
                // if at 10 controls, remove 2-3
                if(controls.Count >= 10)
                {
                    int toRemove = r.Next(3);
                    for (int i = 0; i < toRemove; i++)
                    {
                        var c = controls.First();
                        this.controls.Remove(c);
                        this.holder.Children.Remove(c);
                    }
                }
                else
                {
                    // add 1-5 new controls
                    var toAdd = r.Next(5);
                    for (int i = 0; i < toAdd; i++)
                    {
                    var c = new PeerCountControl();
                    this.controls.Add(c);
                    this.holder.Children.Add(c);
                    }
                }

                await Task.Delay(r.Next(1000, 5000));
            }
        }
    }
}
