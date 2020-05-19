using MiniTC.ViewModel;
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

namespace MiniTC.View
{
    public partial class ViewPanelTC : UserControl
    {
        public ViewPanelTC()
        {
            InitializeComponent();
        }

        private static readonly DependencyProperty DriversProperty =
            DependencyProperty.Register("Drivers", typeof(string[]), typeof(ViewPanelTC),
                new PropertyMetadata(null));

        private static readonly DependencyProperty CurrentDriveProperty =
            DependencyProperty.Register("CurrentDrive", typeof(string), typeof(ViewPanelTC),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty CurrentPathProperty =
            DependencyProperty.Register("CurrentPath", typeof(string), typeof(ViewPanelTC),
                new PropertyMetadata(null));

        private static readonly DependencyProperty CurrentFolderPorperty =
            DependencyProperty.Register("CurrentFolder", typeof(string), typeof(ViewPanelTC),
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty CurrentFolderContentProperty =
            DependencyProperty.Register("CurrentFolderContent", typeof(List<string>), typeof(ViewPanelTC),
                new PropertyMetadata(null));

        private static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.Register("DoubleClickCommand", typeof(ICommand), typeof(ViewPanelTC),
                new FrameworkPropertyMetadata(null));


        public string[] Drivers
        {
            get => (string[])GetValue(DriversProperty);
            set => SetValue(DriversProperty, value);
        }
        
        public string CurrentDrive
        {
            get => (string)GetValue(CurrentDriveProperty);
            set => SetValue(CurrentDriveProperty, value);
        }

        public string CurrentPath
        {
            get => (string)GetValue(CurrentPathProperty);
            set => SetValue(CurrentPathProperty, value);
        }

        public string CurrentFolder
        {
            get => (string)GetValue(CurrentFolderPorperty);
            set => SetValue(CurrentFolderPorperty, value);
        }

        public List<string> CurrentFolderContent
        {
            get => (List<string>)GetValue(CurrentFolderContentProperty);
            set => SetValue(CurrentFolderContentProperty, value);
        }

        public ICommand DoubleClickCommand
        {
            get => (ICommand)GetValue(DoubleClickCommandProperty);
            set => SetValue(DoubleClickCommandProperty, value);
        }

    }
}
