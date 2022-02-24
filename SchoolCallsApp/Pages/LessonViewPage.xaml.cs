using Microsoft.Win32;
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

namespace SchoolCallsApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class LessonViewPage : Page
    {
        Lesson current;
        public LessonViewPage(Lesson lesson)
        {
            InitializeComponent();
            current = lesson;
            DataContext = current;
        }

        private void ResetMscPathS(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = ".wav|*.wav|.mp3|*.mp3";
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                current.MusicPathS = dialog.FileName;
            }
        }
        private void ResetMscPathE(object sender, RoutedEventArgs e)

        {
            OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = ".wav|*.wav|.mp3|*.mp3";
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                current.MusicPathE = dialog.FileName;
            }
}
    }
}
