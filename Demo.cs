using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HoloMetrix.Net.Utilities;
using HoloMetrix.Net.Remote;
using HoloMetrix.Net.Remote.SoundIntensity;

namespace Demo_020_2
{
    class Demo
    {
        private Measurement measurement;

        public async void Test()
        {
            var device = BluetoothConnectionUtilities.SelectDeviceFromPicker();
            if(device == null)
            {
                return;
            }

            var session = BluetoothConnectionUtilities.TryStartRemoteSession(device, BluetoothConnectionUtilities.HoloMetrixHUBService);
            if(session == null)
            {
                return;
            }

            var siApp = await session.TryLaunchApp<SoundIntensityApp>();
            if(siApp == null)
            {
                return;
            }

            CuboidObject dut = new CuboidObject(500, 650, 500);
            CuboidSurface surface = new CuboidSurface(500, 500, 500, 5, 5, 5,
                new SurfaceState[]
                {
                    SurfaceState.Measure, // front
				    SurfaceState.Measure, // top
				    SurfaceState.Measure, // left
				    SurfaceState.Measure, // right
				    SurfaceState.Solid,   // bottom
				    SurfaceState.Ignore   // back
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

            measurement = siApp.Setup(dut, surface);
            if(measurement == null)
            {
                return;
            }

            measurement.SelectSegmentRequested += Measurement_SelectSegmentRequested;
            measurement.StartMeasurementRequested += Measurement_StartMeasurementRequested;

        }

        private void Measurement_StartMeasurementRequested(object sender, MeasurementEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Measurement_SelectSegmentRequested(object sender, SelectSegmentEventArgs e)
        {
            //...

            measurement.SelectSegment(e.SegmentGroupIndex, e.SegmentIndex);
        }
    }
}
