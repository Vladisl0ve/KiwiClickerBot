using KiwiClickerBot.Helpers;
using KiwiClickerBot.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KiwiClickerBot.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand StartCommand { get; set; }        

        public MainWindowViewModel() 
        {
            StartCommand = new RelayCommand(action => StartButtonClick());
        }

        private async void StartButtonClick()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string originalPath = Path.Combine(projectDirectory, "Resources", "original.jpg");
            string samplePath = Path.Combine(projectDirectory, "Resources", "original.jpg");
            var aforge = new AforgeService(originalPath, samplePath);
            var result = await aforge.IsContains(originalPath, samplePath);
            MessageBox.Show($"Found {aforge.GetPlaces().Count} results!");
        }
    }
}
