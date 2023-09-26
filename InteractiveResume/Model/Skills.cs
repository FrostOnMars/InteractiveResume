using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InteractiveResume.Model
{
    internal class Skills : INotifyPropertyChanged
    {
        private List<string> _hardSkills;
        private List<string> _softSkills;

         
        public List<string> HardSkills
        {
            get => _hardSkills;
            set
            {
                if (_hardSkills != value)
                {
                    _hardSkills = value;
                    OnPropertyChanged(nameof(_hardSkills)); // Notify that the "Name"
                                                            // property has changed
                }
            }
        }

        public List<string> SoftSkills
        {
            get => _softSkills;
            set
            {
                if (_softSkills == value) return;
                _softSkills = value;
                OnPropertyChanged(nameof(_softSkills)); // Notify that the "Name"
                                                        // property has changed
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        
        // Create a method to raise the PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
