using System;

namespace SchoolCallsApp
{
    public class Lesson
    {
        public int Id { get; set; }
        public TimeSpan LessonStart;
        public TimeSpan LessonEnd;

        public string MusicPathS { get; set; }
        public string MusicPathE { get; set; }

        public string LessionStartStr
        {
            get => LessonStart.ToString("hh':'mm");
            set
            {
                TimeSpan.TryParse(value, out LessonStart);
            }
        }
        public string LessionEndStr
        {
            get => LessonEnd.ToString("hh':'mm");
            set
            {
                TimeSpan.TryParse(value, out LessonEnd);
            }
        }

        public string Name => "Урок №" + Id;

        public string Limits => $"{LessonStart.ToString("hh':'mm")}-{LessonEnd.ToString("hh':'mm")}";

        public Lesson()
        {

        }

    }
}
