using FrameIt.General;
using FrameIt.Models;
using FrameIt.Services;
using System.Windows;
using System.Windows.Controls;

namespace FrameIt.Frames.AddFrame
{
    public partial class AddFrameName : Page
    {
        private readonly string _pairingCode;

        public AddFrameName(string code)
        {
            InitializeComponent();
            _pairingCode = code;
        }

        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            string name = FrameNameTextBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                PopUpManager.ShowError("Please enter a valid frame name.");
                return;
            }

            var newFrame = new FrameItem
            {
                Id = FramesManager.GenerateNextId(),
                PairingCode = _pairingCode,
                ImagePath = "Images/PlaceholderLogo.jpg",
                Config = new FrameConfig
                {
                    Name = name,
                    IsFrameOn = true
                }
            };

            FramesManager.SaveFrame(newFrame);
            PopUpManager.ShowSuccess("Frame added successfully!");
            NavigationManager.GoToHome();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            NavigationManager.GoBack();
            NavigationManager.GoBack();
        }
    }
}
