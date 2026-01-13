using FrameIt.General;
using FrameIt.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrameIt.Frames.AddFrame
{
    public partial class AddFrameCode : Page
    {
        public AddFrameCode()
        {
            InitializeComponent();
        }

        private readonly PairingCodeManager _pairingCodeManager = new();

        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            string code = PairingCodeTextBox.Text?.Trim();

            if (string.IsNullOrEmpty(code))
            {
                PopUpManager.ShowError("Please enter pairing code");
                // MessageBox.Show("Please enter pairing code");
                return;
            }

            var result = _pairingCodeManager.Validate(code);

            switch (result)
            {
                case PairingCodeValidationResult.Valid:
                    _pairingCodeManager.MarkAsUsed(code);
                    NavigationManager.Navigate(
                        new AddFrameName(code),
                        CanMoveBack: true,
                        ShowNavigationPanel: false
                    );
                    break;

                case PairingCodeValidationResult.NotFound:
                    PopUpManager.ShowError("Invalid pairing code");
                    // MessageBox.Show("Invalid pairing code");
                    break;

                case PairingCodeValidationResult.AlreadyUsed:
                    PopUpManager.ShowError("This pairing code has already been used");
                    // MessageBox.Show("This pairing code has already been used");
                    break;
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            NavigationManager.GoBack();
        }
    }
}
