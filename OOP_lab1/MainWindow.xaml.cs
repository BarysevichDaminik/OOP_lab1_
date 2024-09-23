using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace OOP_lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int CustomersCount = 0;
        public static int TasksCount = 0;
        ObservableCollection<Row> orderPlace;
        ObservableCollection<Row> tasks;
        ObservableCollection<Row> waitingArea;

        public SetupEnvironment setupEnvironment;

        public MainWindow()
        {
            InitializeComponent();
            setupEnvironment = new(this);
        }
        public void Enqueue(int columnIndex, string content)
        {
            switch (columnIndex)
            {
                case 0:
                    orderPlaceListBox.Dispatcher.Invoke(() => orderPlaceListBox.Items.Add(content));
                    break;
                case 1:
                    tasksListBox.Dispatcher.Invoke(() => tasksListBox.Items.Add(content));
                    break;
                case 2:
                    waitingAreaListBox.Dispatcher.Invoke(() => waitingAreaListBox.Items.Add(content));
                    break;
            }
        }
        public void Dequeue(int columnIndex)
        {
            switch (columnIndex)
            {
                case 0:
                    if (orderPlaceListBox.Items.Count > 0)
                    {
                        orderPlaceListBox.Dispatcher.Invoke(() => orderPlaceListBox.Items.RemoveAt(0));
                    }
                    break;
                case 1:
                    if (tasksListBox.Items.Count > 0)
                    {
                        tasksListBox.Dispatcher.Invoke(() => tasksListBox.Items.RemoveAt(0));
                    }
                    break;
                case 2:
                    if (waitingAreaListBox.Items.Count > 0)
                    {
                        waitingAreaListBox.Dispatcher.Invoke(() => waitingAreaListBox.Items.RemoveAt(0));
                    }
                    break;
            }
        }
        public void Dequeue(int columnIndex, string name)
        {
            switch (columnIndex)
            {
                case 0:
                    RemoveItemByName(orderPlaceListBox, name);
                    break;
                case 1:
                    RemoveItemByName(tasksListBox, name);
                    break;
                case 2:
                    RemoveItemByName(waitingAreaListBox, name);
                    break;
            }
        }
        void RemoveItemByName(ListBox listBox, string name)
        {
            listBox.Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    if (listBox.Items[i].ToString() == name)
                    {
                        listBox.Items.RemoveAt(i);
                        break;
                    }
                }
            });
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            setupEnvironment.Run();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            setupEnvironment.Stop();
        }
    }
}