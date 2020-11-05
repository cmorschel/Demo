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

using HoloMetrix.Net.Remote;
using HoloMetrix.Net.Remote.SoundIntensity;


namespace Demo_020_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Test(object sender, RoutedEventArgs e)
        {
            CuboidObject dut = new CuboidObject(400, 300, 350);
            CuboidSurface surf = new CuboidSurface(100, 100, 100, 5, 5, 5,
                new SurfaceState[]
                {
                    SurfaceState.Measure,
                    SurfaceState.Measure,
                    SurfaceState.Measure,
                    SurfaceState.Measure,
                    SurfaceState.Solid,
                    SurfaceState.Ignore
                },
                new MeasurementMethod[]
                {
                    MeasurementMethod.Discrete,
                    MeasurementMethod.Discrete,
                    MeasurementMethod.Discrete,
                    MeasurementMethod.Discrete,
                    MeasurementMethod.Discrete,
                    MeasurementMethod.Discrete
                });


            var hololens = await BluetoothConnectionUtilities.TryConnectToHoloMetrixHub();
            if(hololens == null)
            {
                return;
            }
            var siApp = await hololens.TryLaunchApp<SoundIntensityApp>();
            if (siApp == null)
            {
                return;
            }

            var measurement = await siApp.Setup(dut, surf);

            await measurement.SelectSegment(0, 0);
        }
    }
}
