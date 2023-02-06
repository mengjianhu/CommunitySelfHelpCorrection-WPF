using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 社区自助矫正系统WPF.Domain
{
    public class PickersViewModel : ViewModelBase
    {
        private DateTime _date;
        private DateTime _date1;
        private DateTime _time;
        private DateTime _time1;
        private string _validatingTime;
        private DateTime? _futureValidatingDate;

        public PickersViewModel()
        {
            Date = DateTime.Now;
            Time = DateTime.Now;
            Date1 = DateTime.Now;
            Time1 = DateTime.Now;
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        public DateTime Date1
        {
            get => _date1;
            set => SetProperty(ref _date1, value);
        }

        public DateTime Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }
        public DateTime Time1
        {
            get => _time1;
            set => SetProperty(ref _time1, value);
        }

        public string ValidatingTime
        {
            get => _validatingTime;
            set => SetProperty(ref _validatingTime, value);
        }

        public DateTime? FutureValidatingDate
        {
            get => _futureValidatingDate;
            set => SetProperty(ref _futureValidatingDate, value);
        }
    }
}
