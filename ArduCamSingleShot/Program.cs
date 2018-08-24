using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;
using SecretLabs.NETMF.Hardware.Netduino;
using System;

namespace ArduCamSingleShot
{
    public class ArduCAM_Mini
    {
        public const int I2CReadRegRead = 0x61;
        public const int I2CReadRegWrite = 0x60;
        public static int CS = 7;
        private static int P_CS;
        private static int B_CS = CS;
        public const int ARDUCHIP_TEST1 = 0x00;
        public const int OV2640_CHIPID_HIGH = 0x0A;
        public const int OV2640_CHIPID_LOW = 0x0B;
        public const int JPEG_FMT = 1;
        public const int REG_SIZE = 8;

        public const int ARDUCHIP_FIFO = 0x04;
        public const int FIFO_CLEAR_MASK = 0x01;
        public const int FIFO_START_MASK = 0x02;
        public const int FIFO_RDPTR_RST_MASK = 0x10;
        public const int FIFO_WRPTR_RST_MASK = 0x20;
        public const int FIFO_SIZE1 = 0x42;
        public const int FIFO_SIZE2 = 0x43;
        public const int FIFO_SIZE3 = 0x44;
        public const int ARDUCHIP_FRAMES = 0x01;
        public const int ARDUCHIP_TRIG = 0x41;
        public const int CAP_DONE_MASK = 0x08;
        public const int GPIO_PWDN_MASK = 0x02;
        public const int ARDUCHIP_GPIO = 0x06;
        public const int SINGLE_FIFO_READ = 0x3D;

        private int SensorAddr = 0x30;


        public const int I2C_READ_SPEED = 8000; // in khz

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
        public I2CDevice i2cDevice = new I2CDevice(null);
        private I2CDevice.Configuration i2cReadConfig = new I2CDevice.Configuration(0x60 >> 1, 100);
        private I2CDevice.Configuration i2cWriteConfig = new I2CDevice.Configuration(0x61 >> 1, 100);


        public ArduCAM_Mini()
        {
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


        private void cbi(ref int reg, int bitmask)
        {
            reg &= ~bitmask;
        }

        private void sbi(ref int reg, int bitmask)
        {
            reg |= bitmask;
        }

        private int busWrite(int addr, int val)
        {
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

        private int busRead(int addr)
        {
            byte[] dataOut = new byte[] { (byte)addr, (byte)0x55 };
            byte[] dataIn = new byte[REG_SIZE];
            spiDevice.WriteRead(dataOut, dataIn);

            string message = "Read SPI " + addr.ToString() + ":";

            for (int index = 0; index < dataIn.Length; index++)
            {
                message += " " + dataIn[index].ToString();
            }
            Debug.Print(message);

            return 1;
        }

        public byte writeRegister(int addr, int data)
        {
            byte Addr = (byte)addr;
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

        public byte readRegister(int addr)
        {
            byte Addr = (byte)addr;
            Debug.Print("Attempting read on Addr " + addr);

            byte[] writeBuffer = new byte[2];
            byte[] readBuffer = new byte[2];
            writeBuffer[0] = Addr;
            writeBuffer[1] = 0;
            spiDevice.WriteRead(writeBuffer, readBuffer);
            Thread.Sleep(10);

            Debug.Print(readBuffer[0].ToString("X2") + " " + readBuffer[1].ToString("X2"));
            return readBuffer[1];
        }

        public byte[] readI2CRegister(int addr)
        {
            Debug.Print("Attempting to read I2C register " + addr.ToString("X2"));
            i2cDevice.Config = i2cReadConfig;

            byte Addr = (byte)(addr & 0x00FF);

            byte[] buffer = new byte[1];
            I2CDevice.I2CTransaction[] reading = new I2CDevice.I2CTransaction[2];
            reading[0] = I2CDevice.CreateWriteTransaction(new byte[] { Addr });
            reading[1] = I2CDevice.CreateReadTransaction(buffer);

            Debug.Print("Executing read transaction...");
            int bytesRead = i2cDevice.Execute(reading, 100);

            if (bytesRead == 0)
            {
                throw new NullReferenceException("0 bytes read for i2c register " + addr.ToString("X2"));
            }

            string message = "Read I2c " + addr.ToString("X2") + ":";

            for (int index = 0; index < buffer.Length; index++)
            {
                message += " " + buffer[index].ToString("X2");
            }

            Debug.Print("Transaction complete, reading " + buffer.Length + " bytes");
            Debug.Print(message);

            return buffer;
        }

        public void writeI2CRegister(int addr, int regData)
        {
            byte Addr = (byte)(addr & 0x00FF);
            byte RegData = (byte)(regData & 0x00FF);
            Debug.Print("Attempting to write I2C register " + Addr.ToString("X2") + " with regData " + RegData.ToString());
            i2cDevice.Config = i2cWriteConfig;


            byte[] buffer = { Addr, RegData };
            //I2CDevice.I2CTransaction[] reading = new I2CDevice.I2CTransaction[1];
            //reading[0] = I2CDevice.CreateReadTransaction(buffer);

            Debug.Print("Executing write transaction...");
            I2CDevice.I2CTransaction[] writeTransaction = new I2CDevice.I2CTransaction[1];

            writeTransaction[0] = I2CDevice.CreateWriteTransaction(buffer);


            int bytesRead = i2cDevice.Execute(writeTransaction, 1);

            if (bytesRead == 0)
            {
                throw new NullReferenceException("0 bytes read for i2c register " + addr.ToString("X2"));
            }
            else
            {
                Debug.Print("Bytes read: " + bytesRead.ToString());
            }
        }

        public void WriteI2CRegisters(SensorReg[] regs) {
            for (int i = 0; i < regs.Length; i++) {
                if ((regs[i].Reg != 0xFF) | (regs[i].Val != 0xFF))
                {
                    writeI2CRegister(regs[i].Reg, regs[i].Val);
                    Thread.Sleep(10);
                }
            }
        }

        public void InitCam() {
            writeI2CRegister(0xff, 0x01);
            Thread.Sleep(10);
            writeRegister(0x12, 0x80);
            Thread.Sleep(100);

            Debug.Print("OV2640_JPEG_INIT...");
            WriteI2CRegisters(InitSettings.JPEG_INIT);

            Debug.Print("OV2640_YUV422...");
            WriteI2CRegisters(InitSettings.YUV422);

            Debug.Print("OV2640_JPEG...");
            WriteI2CRegisters(InitSettings.JPEG);

            writeI2CRegister(0xff, 0x01);
            Thread.Sleep(10);
            writeI2CRegister(0x15, 0x00);
            Thread.Sleep(10);

            Debug.Print("OV2640_320x240_JPEG");
            WriteI2CRegisters(InitSettings.SIZE_320x420);
        }

        public void ClearFIFO_Flag() {
            writeRegister(ARDUCHIP_FIFO, FIFO_CLEAR_MASK);
        }

        public void FlushFIFO() {
            writeRegister(ARDUCHIP_FIFO, FIFO_CLEAR_MASK);
        }

        public void StartCapture() {
            writeRegister(ARDUCHIP_FIFO, FIFO_START_MASK);
        }

        public int ReadFIFO_Length() {
            int len1, len2, len3, length = 0;
            len1 = readRegister(FIFO_SIZE1);
            len2 = readRegister(FIFO_SIZE2);
            len3 = readRegister(FIFO_SIZE3) & 0x7f;
            length = ((len3 << 16) | (len2 << 8) | len1) & 0x07fffff;
            return length;
        }

        public int ReadFIFO() {
            int data;
            data = busRead(SINGLE_FIFO_READ);
            return data;
        }

        public int GetBit(int addr, int bit) {
            int temp;
            temp = readRegister(addr);
            temp = temp & bit;
            return temp;
        }

        public void SetBit(int addr, int bit) {
            int temp;
            temp = readRegister(addr);
            writeRegister(addr, temp | bit);
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

            if ((int)testByte == 0x55)
            {
                Debug.Print("Successfully wrote and read back " + testByte.ToString("X2") + " to test register");
            }
            else
            {
                throw new NotSupportedException("Test register didn't return 0x55!!");
            }

            arduCAM.writeI2CRegister(0xFF, 0x01);
            byte[] writeReadback = arduCAM.readI2CRegister(0xFF);
            if ((int)writeReadback[0] == 0x01)
            {
                Debug.Print("Successfully confirmed i2c WRITE OV2640_MINI_2MP");
            }
            else
            {
                throw new NotSupportedException("Unable to write to register!");
            }

            Debug.Print("--\n");
            byte[] chipVersionHigh = arduCAM.readI2CRegister(OV2640_CHIPID_HIGH);
            Thread.Sleep(10);
            Debug.Print("--\n");
            byte[] chipVersionLow = arduCAM.readI2CRegister(OV2640_CHIPID_LOW);
            Debug.Print("--\n");

            if ((int)chipVersionHigh[0] == 0x26 && ((int)chipVersionLow[0] == 0x41 || (int)chipVersionLow[0] == 0x42))
            {
                Debug.Print("Succesfully confirmed i2c READ OV2640_MINI_2MP");
            }
            else
            {
                throw new NotSupportedException("Unknown sensor!");
            }

            Debug.Print("I2C is operational!");

            arduCAM.InitCam();

            Debug.Print("Clearing FIFO flag");
            arduCAM.ClearFIFO_Flag();

            Thread.Sleep(10);
            arduCAM.writeRegister(ARDUCHIP_FRAMES, 0x00);

            Debug.Print("Attempting single capture");
            arduCAM.FlushFIFO();
            arduCAM.ClearFIFO_Flag();
            arduCAM.StartCapture();

            Thread.Sleep(50);
            int bit = arduCAM.GetBit(ARDUCHIP_TRIG, CAP_DONE_MASK);
            if (bit > 0) {
                Debug.Print("Capture complete!");
                arduCAM.SetBit(ARDUCHIP_GPIO, GPIO_PWDN_MASK);
                Thread.Sleep(50);
                int temp = 0, temp_last = 0;
                while ((temp != 0xD9) | (temp_last != 0xFF))
                {
                    temp_last = temp;
                    temp = arduCAM.ReadFIFO();
                    //Debug.Print(temp);
                    Thread.Sleep(10);
                }
                //Clear the capture done flag 
                arduCAM.ClearFIFO_Flag();
            } else {
                Debug.Print("Capture not complete,  got bit as " + bit.ToString());
            }


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