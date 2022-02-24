using NAudio.Wave;
using Newtonsoft.Json;
using SchoolCallsApp.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchoolCallsApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void RunCalls()
        {
            while (true)
            {
                var now = DateTime.Now.TimeOfDay;
                if (Presets.Count == 0)
                    continue;
                foreach (var lesson in Presets[currentPreset].Lessons)
                {
                    AudioFileReader reader;
                    if (Math.Round(lesson.LessonStart.TotalSeconds) == Math.Round(now.TotalSeconds))
                    {
                        if (string.IsNullOrWhiteSpace(lesson.MusicPathS))
                            continue;
                        reader = new AudioFileReader(lesson.MusicPathS);
                        MediaPlayer player = new MediaPlayer();
                        player.Open(new Uri(lesson.MusicPathS));
                        player.Play();
                        Thread.Sleep(reader.TotalTime + TimeSpan.FromSeconds(1));
                        continue;
                    }
                    if (Math.Round(lesson.LessonEnd.TotalSeconds) == Math.Round(now.TotalSeconds))
                    {
                        if (string.IsNullOrWhiteSpace(lesson.MusicPathE))
                            continue;
                        reader = new AudioFileReader(lesson.MusicPathE);
                        MediaPlayer player = new MediaPlayer();
                        player.Open(new Uri(lesson.MusicPathE));
                        player.Play();
                        Thread.Sleep(reader.TotalTime + TimeSpan.FromSeconds(1));
                        continue;
                    }
                }
                Thread.Sleep(200);
            }
        }
        public int currentPreset;
        public List<DayPreset> Presets { get; set; }
        public MainWindow()
        {
            try
            {


                InitializeComponent();
                PresetsLW.ItemsSource = Presets;
                Presets = new List<DayPreset>();
                var saves = JsonConvert.DeserializeObject<List<DayPreset>>(File.ReadAllText(Path.Combine(Utils.AppData, "lessons.json")));
                if (saves != null)
                    Presets = saves;
                PresetsLW.ItemsSource = Presets;

                if(Presets.Count > 0)
                    PresetsLW.SelectedIndex = currentPreset = Properties.Settings.Default.CurrentPreset;

                Task t = Task.Run(() => RunCalls());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateView(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                var selLes = LessonsLW.SelectedItem as Lesson;
                if (selLes != null)
                {
                    MainFr.Navigate(new LessonViewPage(selLes));
                }
                LessonsLW.ItemsSource = null;
                LessonsLW.ItemsSource = Presets[currentPreset].Lessons;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddLesson(object sender, RoutedEventArgs e)
        {
            Presets[currentPreset].Lessons.Add(new Lesson() { Id = Presets[currentPreset].Lessons.Count + 1 });
            LessonsLW.ItemsSource = null;
            LessonsLW.ItemsSource = Presets[currentPreset].Lessons;
            MainFr.Navigate(new LessonViewPage(Presets[currentPreset].Lessons.Last()));
            File.WriteAllText(Path.Combine(Utils.AppData, "lessons.json"), JsonConvert.SerializeObject(Presets, Formatting.Indented));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(Utils.AppData, "lessons.json"), JsonConvert.SerializeObject(Presets, Formatting.Indented));

        }

        private void UpdatePreset(object sender, SelectionChangedEventArgs e)
        {
            if (PresetsLW.SelectedIndex != -1)
            {
                currentPreset = PresetsLW.SelectedIndex;
                Properties.Settings.Default.CurrentPreset = currentPreset;
                Properties.Settings.Default.Save();
            }
            LessonsLW.ItemsSource = Presets[currentPreset].Lessons;
            PresetsLW.ItemsSource = null;
            PresetsLW.ItemsSource = Presets;
            MainFr.Navigate(new PresetViewPage(Presets[currentPreset]));
        }

        private void AddPreset(object sender, RoutedEventArgs e)
        {
            Presets.Add(new DayPreset() { Name = "Пресет " + (Presets.Count + 1) });
            MainFr.Navigate(new PresetViewPage(Presets.Last()));
            PresetsLW.ItemsSource = null;
            PresetsLW.ItemsSource = Presets;

        }
    }
}
