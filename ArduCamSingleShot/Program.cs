using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;
using SecretLabs.NETMF.Hardware.Netduino;
using System;

namespace NetduinoBlink
{
    public class ArduCAM_Mini
    {
        public const int I2CReadRegRead = 0x61;
        public const int I2CReadRegWrite = 0x60;
        public static int CS = 7;
        private static int P_CS;
        private static int B_CS = CS;
        public const int ARDUCHIP_TEST1 = 0x00;
        public const int REG_SIZE = 8;
        private int sensorAddr = 0x30;
        I2CDevice i2CDevice;
        private static SPI.Configuration spiConfig = new SPI.Configuration(
            ChipSelect_Port: Pins.GPIO_PIN_D7,      // Chip select is digital IO 4.
            ChipSelect_ActiveState: false,          // Chip select is active low.
            ChipSelect_SetupTime: 0,                // Amount of time between selection and the clock starting
            ChipSelect_HoldTime: 0,                 // Amount of time the device must be active after the data has been read.
            Clock_Edge: true,                      // Sample on the falling edge.
            Clock_IdleState: false,                  // Clock is idle when high.
            Clock_RateKHz: 8000,                    // 2MHz clock speed.
            SPI_mod: SPI_Devices.SPI1               // Use SPI1
        );
        public SPI spiDevice;

        public ArduCAM_Mini() {
            i2CDevice = new I2CDevice(new I2CDevice.Configuration(I2CReadRegRead, 50));
            spiDevice = new SPI(spiConfig);
        }

        //void wrSensorReg8_8(int regId, int regData) {
            
        //}

        //void initCam() {
        //    wrSensorReg8_8(0xff, 0x01);
        //    wrSensorReg8_8(0x12, 0x80);
        //    Thread.Sleep(100)
        //    if (m_fmt == JPEG)
        //    {
        //        wrSensorRegs8_8(OV2640_JPEG_INIT);
        //        wrSensorRegs8_8(OV2640_YUV422);
        //        wrSensorRegs8_8(OV2640_JPEG);
        //        wrSensorReg8_8(0xff, 0x01);
        //        wrSensorReg8_8(0x15, 0x00);
        //        wrSensorRegs8_8(OV2640_320x240_JPEG);
        //        //wrSensorReg8_8(0xff, 0x00);
        //        //wrSensorReg8_8(0x44, 0x32);
        //    }
        //    else
        //    {
        //        wrSensorRegs8_8(OV2640_QVGA);
        //    }
        //}


        private void cbi(ref int reg, int bitmask) {
            reg &= ~bitmask;
        }

        private void sbi(ref int reg, int bitmask)
        {
            reg |= bitmask;
        }

        private int busWrite(int addr, int val) {
            //cbi(P_CS, B_CS);
            byte[] writeBuf = new byte[] {
                (byte) addr,
                (byte) val
            };

            Debug.Print("Writing " + writeBuf[0].ToString() + " " + writeBuf[1].ToString());

            spiDevice.Write(writeBuf);
            //sbi(P_CS, B_CS);
            return 1;
        }

        private int busRead(int addr) {
            byte[] dataOut = new byte[] { (byte)addr, (byte) 0x55 };
            byte[] dataIn = new byte[REG_SIZE];
            spiDevice.WriteRead(dataOut, dataIn);

            string message = "Read SPI " + addr.ToString()  + ":";

            for (int index = 0; index < dataIn.Length; index++)
            {
                message += " " + dataIn[index].ToString();
            }
            Debug.Print(message);

            return 1;
        }

        public byte writeRegister(int addr, int data) {
            byte Addr = (byte) addr;
            byte Val = (byte)data;
            byte[] writeBuffer = new byte[2];
            byte[] readBuffer = new byte[2];
            Debug.Print("Attempting Write and readback of Addr " + Addr.ToString());
            writeBuffer[0] = (byte)(Addr + 0x80);
            writeBuffer[1] = Val;
            spiDevice.Write(writeBuffer);
            Thread.Sleep(10);

            writeBuffer[0] = Addr;
            writeBuffer[1] = 0;
            spiDevice.WriteRead(writeBuffer, readBuffer);
            Thread.Sleep(10);

            Debug.Print(readBuffer[0].ToString("X2") + " " + readBuffer[1].ToString("X2"));
            return readBuffer[1];
        }

        public byte readRegister(int addr) {
            byte Addr = (byte)addr;
            Debug.Print("Attempting read on addr " + addr);

            byte[] writeBuffer = new byte[2];
            byte[] readBuffer = new byte[2];
            writeBuffer[0] = Addr;
            writeBuffer[1] = 0;
            spiDevice.WriteRead(writeBuffer, readBuffer);
            Thread.Sleep(10);

            Debug.Print(readBuffer[0].ToString("X2") + " " + readBuffer[1].ToString("X2"));
            return readBuffer[1];
        }


        public static void Main()
        {
            // configure an output port for us to "write" to the LED
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
            //OutputPort CS_Port = new OutputPort(Pins.GPIO_PIN_D7, true);

            Debug.Print("Attempting to instantiate ArduCAM_Mini");
            ArduCAM_Mini arduCAM = new ArduCAM_Mini();

            arduCAM.writeRegister(0x07, 0x80);
            Thread.Sleep(100);
            arduCAM.writeRegister(0x07, 0x80);
            Thread.Sleep(100);

            Thread.Sleep(10);
            arduCAM.writeRegister(ARDUCHIP_TEST1, 0x55);

            byte testByte = arduCAM.readRegister(ARDUCHIP_TEST1);

            if ((int)testByte == 0x55) {
                Debug.Print("Successfully wrote and read back " + testByte.ToString("X2") + " to test register");
            } else {
                throw new NotSupportedException("Test register didn't return 0x55!!");
            }

            //byte chipVersion = arduCAM.readRegister(0x40);

            //if ((int)chipVersion == 0x40) {
            //    Debug.Print("Succesfully confirmed OV2640_MINI_2MP");
            //} else {
            //    throw new NotSupportedException("Unknown sensor: " + chipVersion.ToString("X2"));
            //}



            while (true)
            {
                led.Write(true); // turn on the LED 
                Thread.Sleep(250); // sleep for 250ms 
                led.Write(false); // turn off the LED 
                Thread.Sleep(250); // sleep for 250ms 
            }

        }
    }
}