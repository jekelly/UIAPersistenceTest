namespace UIAPersistenceTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for PeerCountControl.xaml
    /// </summary>
    public partial class PeerCountControl : UserControl, INotifyPropertyChanged
    {
        private static int peersCreated = 0;
        private static event EventHandler PeerCreated;
        public event PropertyChangedEventHandler PropertyChanged;

        public int PeersCreated => peersCreated;

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            OnPeerCreated();
            return base.OnCreateAutomationPeer();
        }

        private void OnPeerCreated()
        {
            peersCreated++;
            PeerCreated?.Invoke(this, EventArgs.Empty);
        }

        public PeerCountControl()
        {
            InitializeComponent();
            PeerCreated += (o,e) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PeersCreated)));
        }
    }
}
