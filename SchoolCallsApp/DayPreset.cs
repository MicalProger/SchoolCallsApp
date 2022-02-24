using System.Collections.Generic;

namespace SchoolCallsApp
{
    public class DayPreset
    {
        public List<Lesson> Lessons;
        public string Name { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {

            return Name;

        }

        public DayPreset()
        {
            Lessons = new List<Lesson>();
        }
    }
}
