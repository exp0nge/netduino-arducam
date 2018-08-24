﻿using System;
namespace ArduCamSingleShot
{

    public struct SensorReg
    {
        public int Reg;
        public int Val;

        public SensorReg(int r, int v)
        {
            Reg = r;
            Val = v;
        }
    }

    public class InitSettings
    {

        public static SensorReg[] JPEG_INIT = {
            new SensorReg(0xFF, 0x00),
            new SensorReg(0x2c, 0xff),
            new SensorReg(0x2e, 0xdf),
            new SensorReg(0xff, 0x01),
            new SensorReg(0x3c, 0x32),
            new SensorReg(0x11, 0x00),
            new SensorReg(0x09, 0x02),
            new SensorReg(0x04, 0x28),
            new SensorReg(0x13, 0xe5 ),
            new SensorReg(0x14, 0x48),
            new SensorReg(0x2c, 0x0c),
            new SensorReg(0x33, 0x78),
            new SensorReg(0x3a, 0x33),
            new SensorReg(0x3b, 0xfB),
            new SensorReg(0x3e, 0x00),
            new SensorReg(0x43, 0x11),
            new SensorReg(0x16, 0x10),
            new SensorReg(0x39, 0x92),
              new SensorReg(0x35, 0xda),
              new SensorReg(0x22, 0x1a),
              new SensorReg(0x37, 0xc3),
              new SensorReg(0x23, 0x00),
              new SensorReg(0x34, 0xc0),
              new SensorReg(0x36, 0x1a),
              new SensorReg(0x06, 0x88),
              new SensorReg(0x07, 0xc0),
              new SensorReg(0x0d, 0x87),
              new SensorReg(0x0e, 0x41),
              new SensorReg(0x4c, 0x00),
              new SensorReg(0x48, 0x00),
              new SensorReg(0x5B, 0x00),
              new SensorReg(0x42, 0x03),
              new SensorReg(0x4a, 0x81),
              new SensorReg(0x21, 0x99),
              new SensorReg(0x24, 0x40),
              new SensorReg(0x25, 0x38),
              new SensorReg(0x26, 0x82),
              new SensorReg(0x5c, 0x00),
              new SensorReg(0x63, 0x00),
              new SensorReg(0x61, 0x70),
              new SensorReg(0x62, 0x80),
              new SensorReg(0x7c, 0x05),
              new SensorReg(0x20, 0x80),
              new SensorReg(0x28, 0x30),
              new SensorReg(0x6c, 0x00),
              new SensorReg(0x6d, 0x80),
              new SensorReg(0x6e, 0x00),
              new SensorReg(0x70, 0x02),
              new SensorReg(0x71, 0x94),
              new SensorReg(0x73, 0xc1),
              new SensorReg(0x12, 0x40),
              new SensorReg(0x17, 0x11),
              new SensorReg(0x18, 0x43),
              new SensorReg(0x19, 0x00),
              new SensorReg(0x1a, 0x4b),
              new SensorReg(0x32, 0x09),
              new SensorReg(0x37, 0xc0),
              new SensorReg(0x4f, 0x60),
              new SensorReg(0x50, 0xa8),
              new SensorReg(0x6d, 0x00),
              new SensorReg(0x3d, 0x38),
              new SensorReg(0x46, 0x3f),
              new SensorReg(0x4f, 0x60),
              new SensorReg(0x0c, 0x3c),
              new SensorReg(0xff, 0x00),
              new SensorReg(0xe5, 0x7f),
              new SensorReg(0xf9, 0xc0),
              new SensorReg(0x41, 0x24),
              new SensorReg(0xe0, 0x14),
              new SensorReg(0x76, 0xff),
              new SensorReg(0x33, 0xa0),
              new SensorReg(0x42, 0x20),
              new SensorReg(0x43, 0x18),
              new SensorReg(0x4c, 0x00),
              new SensorReg(0x87, 0xd5),
              new SensorReg(0x88, 0x3f),
              new SensorReg(0xd7, 0x03),
              new SensorReg(0xd9, 0x10),
              new SensorReg(0xd3, 0x82),
              new SensorReg(0xc8, 0x08),
              new SensorReg(0xc9, 0x80),
              new SensorReg(0x7c, 0x00),
              new SensorReg(0x7d, 0x00),
              new SensorReg(0x7c, 0x03),
              new SensorReg(0x7d, 0x48),
              new SensorReg(0x7d, 0x48),
              new SensorReg(0x7c, 0x08),
              new SensorReg(0x7d, 0x20),
              new SensorReg(0x7d, 0x10),
              new SensorReg(0x7d, 0x0e),
              new SensorReg(0x90, 0x00),
              new SensorReg(0x91, 0x0e),
              new SensorReg(0x91, 0x1a),
              new SensorReg(0x91, 0x31),
              new SensorReg(0x91, 0x5a),
              new SensorReg(0x91, 0x69),
              new SensorReg(0x91, 0x75),
              new SensorReg(0x91, 0x7e),
              new SensorReg(0x91, 0x88),
              new SensorReg(0x91, 0x8f),
              new SensorReg(0x91, 0x96),
              new SensorReg(0x91, 0xa3),
              new SensorReg(0x91, 0xaf),
              new SensorReg(0x91, 0xc4),
              new SensorReg(0x91, 0xd7),
              new SensorReg(0x91, 0xe8),
              new SensorReg(0x91, 0x20),
              new SensorReg(0x92, 0x00),
              new SensorReg(0x93, 0x06),
              new SensorReg(0x93, 0xe3),
              new SensorReg(0x93, 0x05),
              new SensorReg(0x93, 0x05),
              new SensorReg(0x93, 0x00),
              new SensorReg(0x93, 0x04),
              new SensorReg(0x93, 0x00),
              new SensorReg(0x93, 0x00),
              new SensorReg(0x93, 0x00),
              new SensorReg(0x93, 0x00),
              new SensorReg(0x93, 0x00),
              new SensorReg(0x93, 0x00),
              new SensorReg(0x93, 0x00),
              new SensorReg(0x96, 0x00),
              new SensorReg(0x97, 0x08),
              new SensorReg(0x97, 0x19),
              new SensorReg(0x97, 0x02),
              new SensorReg(0x97, 0x0c),
              new SensorReg(0x97, 0x24),
              new SensorReg(0x97, 0x30),
              new SensorReg(0x97, 0x28),
              new SensorReg(0x97, 0x26),
              new SensorReg(0x97, 0x02),
              new SensorReg(0x97, 0x98),
              new SensorReg(0x97, 0x80),
              new SensorReg(0x97, 0x00),
              new SensorReg(0x97, 0x00),
              new SensorReg(0xc3, 0xed),
              new SensorReg(0xa4, 0x00),
              new SensorReg(0xa8, 0x00),
              new SensorReg(0xc5, 0x11),
              new SensorReg(0xc6, 0x51),
              new SensorReg(0xbf, 0x80),
              new SensorReg(0xc7, 0x10),
              new SensorReg(0xb6, 0x66),
              new SensorReg(0xb8, 0xA5),
              new SensorReg(0xb7, 0x64),
              new SensorReg(0xb9, 0x7C),
              new SensorReg(0xb3, 0xaf),
              new SensorReg(0xb4, 0x97),
              new SensorReg(0xb5, 0xFF),
              new SensorReg(0xb0, 0xC5),
              new SensorReg(0xb1, 0x94),
              new SensorReg(0xb2, 0x0f),
              new SensorReg(0xc4, 0x5c),
              new SensorReg(0xc0, 0x64),
              new SensorReg(0xc1, 0x4B),
              new SensorReg(0x8c, 0x00),
              new SensorReg(0x86, 0x3D),
              new SensorReg(0x50, 0x00),
              new SensorReg(0x51, 0xC8),
              new SensorReg(0x52, 0x96),
              new SensorReg(0x53, 0x00),
              new SensorReg(0x54, 0x00),
              new SensorReg(0x55, 0x00),
              new SensorReg(0x5a, 0xC8),
              new SensorReg(0x5b, 0x96),
              new SensorReg(0x5c, 0x00),
              new SensorReg(0xd3, 0x00),   //new Item(0xd3, 0x7f),
              new SensorReg(0xc3, 0xed),
              new SensorReg(0x7f, 0x00),
              new SensorReg(0xda, 0x00),
              new SensorReg(0xe5, 0x1f),
              new SensorReg(0xe1, 0x67),
              new SensorReg(0xe0, 0x00),
              new SensorReg(0xdd, 0x7f),
              new SensorReg(0x05, 0x00),

              new SensorReg(0x12, 0x40),
              new SensorReg(0xd3, 0x04),   //new Item(0xd3, 0x7f),
              new SensorReg(0xc0, 0x16),
              new SensorReg(0xC1, 0x12),
              new SensorReg(0x8c, 0x00),
              new SensorReg(0x86, 0x3d),
              new SensorReg(0x50, 0x00),
              new SensorReg(0x51, 0x2C),
              new SensorReg(0x52, 0x24),
              new SensorReg(0x53, 0x00),
              new SensorReg(0x54, 0x00),
              new SensorReg(0x55, 0x00),
              new SensorReg(0x5A, 0x2c),
              new SensorReg(0x5b, 0x24),
              new SensorReg(0x5c, 0x00),
            new SensorReg(0xff, 0xff),
        };

        public static SensorReg[] YUV422 = {
              new SensorReg(0xFF, 0x00),
              new SensorReg(0x05, 0x00),
              new SensorReg(0xDA, 0x10),
              new SensorReg(0xD7, 0x03),
              new SensorReg(0xDF, 0x00),
              new SensorReg(0x33, 0x80),
              new SensorReg(0x3C, 0x40),
              new SensorReg(0xe1, 0x77),
              new SensorReg(0x00, 0x00),
              new SensorReg(0xff, 0xff),
        };

        public static SensorReg[] JPEG = {
              new SensorReg(0xe0, 0x14),
              new SensorReg(0xe1, 0x77),
              new SensorReg(0xe5, 0x1f),
              new SensorReg(0xd7, 0x03),
              new SensorReg(0xda, 0x10),
              new SensorReg(0xe0, 0x00),
              new SensorReg(0xFF, 0x01),
              new SensorReg(0x04, 0x08),
              new SensorReg(0xff, 0xff),
        };

        public static SensorReg[] SIZE_320x420 = {
          new SensorReg(0x12, 0x40),
          new SensorReg(0x17, 0x11),
          new SensorReg(0x18, 0x43),
          new SensorReg(0x19, 0x00),
          new SensorReg(0x1a, 0x4b),
          new SensorReg(0x32, 0x09),
          new SensorReg(0x4f, 0xca),
          new SensorReg(0x50, 0xa8),
          new SensorReg(0x5a, 0x23),
          new SensorReg(0x6d, 0x00),
          new SensorReg(0x39, 0x12),
          new SensorReg(0x35, 0xda),
          new SensorReg(0x22, 0x1a),
          new SensorReg(0x37, 0xc3),
          new SensorReg(0x23, 0x00),
          new SensorReg(0x34, 0xc0),
          new SensorReg(0x36, 0x1a),
          new SensorReg(0x06, 0x88),
          new SensorReg(0x07, 0xc0),
          new SensorReg(0x0d, 0x87),
          new SensorReg(0x0e, 0x41),
          new SensorReg(0x4c, 0x00),
          new SensorReg(0xff, 0x00),
          new SensorReg(0xe0, 0x04),
          new SensorReg(0xc0, 0x64),
          new SensorReg(0xc1, 0x4b),
          new SensorReg(0x86, 0x35),
          new SensorReg(0x50, 0x89),
          new SensorReg(0x51, 0xc8),
          new SensorReg(0x52, 0x96),
          new SensorReg(0x53, 0x00),
          new SensorReg(0x54, 0x00),
          new SensorReg(0x55, 0x00),
          new SensorReg(0x57, 0x00),
          new SensorReg(0x5a, 0x50),
          new SensorReg(0x5b, 0x3c),
          new SensorReg(0x5c, 0x00),
          new SensorReg(0xe0, 0x00),
          new SensorReg(0xff, 0xff),
        };
    }

}