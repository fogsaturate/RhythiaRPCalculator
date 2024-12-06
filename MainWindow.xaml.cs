using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RhythiaRPCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public float speed = 1.0f;
        private readonly Dictionary<RadioButton, float> Speeds;
        public MainWindow()
        {
            InitializeComponent();

            Speeds = new Dictionary<RadioButton, float>
            {
                {SpeedMinusMinusMinus, 1 / 1.35f},
                {SpeedMinusMinus, 1 / 1.25f},
                {SpeedMinus, 1 / 1.15f},
                {Nomod, 1f},
                {SpeedPlus, 1.15f},
                {SpeedPlusPlus, 1.25f},
                {SpeedPlusPlusPlus, 1.35f},
                {SpeedPlusPlusPlusPlus, 1.45f},
            };
        }

        static double EaseIn(float Accuracy, float StarRating)
        {
            float Exponent = 90 - 12 * StarRating;
            Exponent = Math.Max(Exponent, 5);

            return Accuracy == 0 ? 0 : Math.Pow(2, Exponent * Accuracy - Exponent);
        }

        static double RPCalculator (float Accuracy, float StarRating)
        {
            return Math.Round(Math.Pow((StarRating * EaseIn(Accuracy, StarRating) * 100) / 2, 2) / 1000, 2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var SpeedSelected in Speeds)
            {
                if (SpeedSelected.Key.IsChecked == true)
                {
                    speed = SpeedSelected.Value;
                    break;
                }
            }

            RPText.Content = RPCalculator(float.Parse(AccuracyText.Text) / 100, float.Parse(StarRatingText.Text) * speed) * 2;
             
        }
    }
}