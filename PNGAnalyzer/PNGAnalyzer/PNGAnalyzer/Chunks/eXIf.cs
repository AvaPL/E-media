using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PNGAnalyzer
{
    public class eXIf : Chunk
    {
        public eXIf(byte[] data, uint crc) : base("eXIf", data, crc)
        {
            ParseData(data);
        }

        public eXIf(Chunk chunk) : base(chunk)
        {
            if (chunk.Type != "eXIf")
                throw new ArgumentException("Invalid chunk type passed to eXIf");
            ParseData(chunk.Data);
        }

        public string EndianFlag { get; private set; }
        public int Offset { get; private set; }
        public IFD Ifd { get; private set; }

        private void ParseData(byte[] data)
        {
            EndianFlag = Encoding.GetEncoding("ISO-8859-1").GetString(data.Take(2).ToArray());
            int index = 2;
            if (Converter.ToInt16(data, index) != 42)
                throw new FormatException("eXIf data is not in TIFF format");
            index += 2;
            Offset = Converter.ToInt32(data, index);
            Ifd = new IFD(data, Offset);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {Ifd}";
        }

        public class IFD
        {
            public IFD(byte[] data, int index)
            {
                short length = Converter.ToInt16(data, index);
                index += 2;
                Tags = new List<Tag>(length);
                for (int i = 0; i < length; i++)
                {
                    Tags.Add(new Tag(data, index));
                    index += 12;
                }

                Data = data.ToArray();
            }

            public List<Tag> Tags { get; }
            public byte[] Data { get; }

            private static string ValueToString(byte[] data, short tagType, int count, byte[] offset)
            {
                int offsetValue = Converter.ToInt32(offset);
                return tagType switch
                {
                    1 => offset[0].ToString(),
                    2 => count <= 4
                        ? Encoding.ASCII.GetString(Converter.GetBytes(offsetValue), 0, count - 1)
                        : Encoding.ASCII.GetString(data, offsetValue, count - 1),
                    3 => Converter.ToUInt16(offset).ToString(),
                    4 => Converter.ToUInt32(offset).ToString(),
                    5 => Converter.ToURational(data, offsetValue).ToString(CultureInfo.InvariantCulture),
                    6 => ((sbyte) offset[0]).ToString(),
                    8 => Converter.ToInt16(offset).ToString(),
                    9 => Converter.ToInt32(offset).ToString(),
                    10 => Converter.ToRational(data, offsetValue).ToString(CultureInfo.InvariantCulture),
                    11 => Converter.ToFloat(offset).ToString(CultureInfo.InvariantCulture),
                    12 => Converter.ToDouble(data, offsetValue).ToString(CultureInfo.InvariantCulture),
                    _ => ""
                };
            }

            public override string ToString()
            {
                return string.Join(", ", Tags.Select(tag => tag.ToString()).Where(s => !string.IsNullOrWhiteSpace(s)));
            }

            public class Tag
            {
                public static readonly Dictionary<ushort, string> TagNames = new Dictionary<ushort, string>()
                {
                    {0x000b, "ProcessingSoftware"},
                    {0x00fe, "SubfileType"},
                    {0x00ff, "OldSubfileType"},
                    {0x0100, "ImageWidth"},
                    {0x0101, "ImageHeight"},
                    {0x0103, "Compression"},
                    {0x0106, "PhotometricInterpretation"},
                    {0x0107, "Thresholding"},
                    {0x0108, "CellWidth"},
                    {0x0109, "CellLength"},
                    {0x010a, "FillOrder"},
                    {0x010d, "DocumentName"},
                    {0x010e, "ImageDescription"},
                    {0x010f, "Make"},
                    {0x0110, "Model"},
                    {0x0111, "PreviewImageStart"},
                    {0x0112, "Orientation"},
                    {0x0115, "SamplesPerPixel"},
                    {0x0116, "RowsPerStrip"},
                    {0x0117, "PreviewImageLength"},
                    {0x0118, "MinSampleValue"},
                    {0x0119, "MaxSampleValue"},
                    {0x011a, "XResolution"},
                    {0x011b, "YResolution"},
                    {0x011c, "PlanarConfiguration"},
                    {0x011d, "PageName"},
                    {0x011e, "XPosition"},
                    {0x011f, "YPosition"},
                    {0x0122, "GrayResponseUnit"},
                    {0x0128, "ResolutionUnit"},
                    {0x0131, "Software"},
                    {0x0132, "ModifyDate"},
                    {0x013b, "Artist"},
                    {0x013c, "HostComputer"},
                    {0x013d, "Predictor"},
                    {0x0142, "TileWidth"},
                    {0x0143, "TileLength"},
                    {0x014c, "InkSet"},
                    {0x0151, "TargetPrinter"},
                    {0x0201, "ThumbnailOffset"},
                    {0x0202, "ThumbnailLength"},
                    {0x0213, "YCbCrPositioning"},
                    {0x02bc, "ApplicationNotes"},
                    {0x4746, "Rating"},
                    {0x4749, "RatingPercent"},
                    {0x8298, "Copyright"},
                    {0x829a, "ExposureTime"},
                    {0x829d, "FNumber"},
                    {0x83bb, "IPTC-NAA"},
                    {0x8546, "SEMInfo"},
                    {0x87b1, "GeoTiffAsciiParams"},
                    {0x8822, "ExposureProgram"},
                    {0x8824, "SpectralSensitivity"},
                    {0x882b, "SelfTimerMode"},
                    {0x8839, "SensitivityType"},
                    {0x8831, "StandardOutputSensitivity"},
                    {0x8832, "RecommendedExposureIndex"},
                    {0x8833, "ISOSpeed"},
                    {0x8834, "ISOSpeedLatitudeyyy"},
                    {0x8835, "ISOSpeedLatitudezzz"},
                    {0x9003, "DateTimeOriginal"},
                    {0x9004, "CreateDate"},
                    {0x9010, "OffsetTime"},
                    {0x9011, "OffsetTimeOriginal"},
                    {0x9012, "OffsetTimeDigitized"},
                    {0x9102, "CompressedBitsPerPixel"},
                    {0x9201, "ShutterSpeedValue"},
                    {0x9202, "ApertureValue"},
                    {0x9203, "BrightnessValue"},
                    {0x9204, "ExposureCompensation"},
                    {0x9205, "MaxApertureValue"},
                    {0x9206, "SubjectDistance"},
                    {0x9207, "MeteringMode"},
                    {0x9208, "LightSource"},
                    {0x9209, "Flash"},
                    {0x920a, "FocalLength"},
                    {0x9211, "ImageNumber"},
                    {0x9212, "SecurityClassification"},
                    {0x9213, "ImageHistory"},
                    {0x9290, "SubSecTime"},
                    {0xfe57, "Smoothness"},
                    {0xfe55, "Saturation"},
                    {0xfe53, "Brightness"},
                    {0xfe51, "Exposure"},
                    {0xfe4d, "Converter"},
                    {0xfde9, "SerialNumber"},
                    {0xea1d, "OffsetSchema"},
                    {0xc7ee, "EnhanceParams"},
                    {0xc7ec, "DepthUnits"},
                    {0xc7ea, "DepthNear"},
                    {0xc7a5, "BaselineExposureOffset"},
                    {0xc7a3, "ProfileHueSatMapEncoding"},
                    {0xc71a, "PreviewColorSpace"},
                    {0xc718, "PreviewSettingsName"},
                    {0xc716, "PreviewApplicationName"},
                    {0xc6fd, "ProfileEmbedPolicy"},
                    {0xc6f3, "CameraCalibrationSig"},
                    {0xc6bf, "ColorimetricReference"},
                    {0xc68b, "OriginalRawFileName"},
                    {0xc65b, "CalibrationIlluminant2"},
                    {0xc634, "DNGPrivateData"},
                    {0xc62e, "LinearResponseLimit"},
                    {0xc62c, "BaselineSharpness"},
                    {0xc62a, "BaselineExposure"},
                    {0xc614, "UniqueCameraModel"},
                    {0xa500, "Gamma"},
                    {0xa480, "GDALMetadata"},
                    {0xa435, "LensSerialNumber"},
                    {0xa433, "LensMake"},
                    {0xa431, "SerialNumber"},
                    {0xa420, "ImageUniqueID"},
                    {0xa409, "Saturation"},
                    {0xa407, "GainControl"},
                    {0xa405, "FocalLengthIn35mmFormat"},
                    {0xa403, "WhiteBalance"},
                    {0xa401, "CustomRendered"},
                    {0xa217, "SensingMethod"},
                    {0xa215, "ExposureIndex"},
                    {0xa20f, "FocalPlaneYResolution"},
                    {0xa20b, "FlashEnergy"},
                    {0xa004, "RelatedSoundFile"},
                    {0xa002, "ExifImageWidth"},
                    {0x9c9e, "EPKeywords"},
                    {0x9c9c, "XPComment"},
                    {0x9405, "CameraElevationAngle"},
                    {0x9403, "WaterDepth"},
                    {0x9401, "Humidity"},
                    {0x9292, "SubSecTimeDigitized"},
                    {0xfe58, "MoireFilter"},
                    {0xfe56, "Sharpness"},
                    {0xfe54, "Contrast"},
                    {0xfe52, "Shadows"},
                    {0xfe4e, "WhiteBalance"},
                    {0xfe4c, "RawFile"},
                    {0xfdea, "Lens"},
                    {0xfde8, "OwnerName"},
                    {0xc7ed, "DepthMeasureType"},
                    {0xc7eb, "DepthFar"},
                    {0xc7e9, "DepthFormat"},
                    {0xc7a8, "RawToPreviewGain"},
                    {0xc7a6, "DefaultBlackRender"},
                    {0xc7a4, "ProfileLookTableEncoding"},
                    {0xc7a1, "CameraLabel"},
                    {0xc789, "ReelName"},
                    {0xc764, "FrameRate"},
                    {0xc71b, "PreviewDateTime"},
                    {0xc719, "PreviewSettingsDigest"},
                    {0xc717, "PreviewApplicationVersion"},
                    {0xc6fe, "ProfileCopyright"},
                    {0xc6f8, "ProfileName"},
                    {0xc6f6, "AsShotProfileName"},
                    {0xc6f4, "ProfileCalibrationSig"},
                    {0xc65a, "CalibrationIlluminant1"},
                    {0xc635, "MakerNoteSafety"},
                    {0xc633, "ShadowScale"},
                    {0xc62f, "CameraSerialNumber"},
                    {0xc62b, "BaselineNoise"},
                    {0xc615, "LocalizedCameraModel"},
                    {0xa481, "GDALNoData"},
                    {0xa460, "CompositeImage"},
                    {0xa434, "LensModel"},
                    {0xa430, "OwnerName"},
                    {0xa40c, "SubjectDistanceRange"},
                    {0xa40a, "Sharpness"},
                    {0xa408, "Contrast"},
                    {0xa406, "SceneCaptureType"},
                    {0xa404, "DigitalZoomRatio"},
                    {0xa402, "ExposureMode"},
                    {0xa210, "FocalPlaneResolutionUnit"},
                    {0xa20e, "FocalPlaneResolutionX"},
                    {0xa003, "ExifImageHeight"},
                    {0xa001, "ColorSpace"},
                    {0x9c9f, "XPSubject"},
                    {0x9c9d, "XPAuthor"},
                    {0x9c9b, "XPTitle"},
                    {0x9404, "Acceleration"},
                    {0x9402, "Pressure"},
                    {0x9400, "AmbientTemperature"},
                    {0x9291, "SubSecTimeOriginal"}
                };

                public Tag(byte[] data, int index)
                {
                    TagID = Converter.ToUInt16(data, index);
                    index += 2;
                    TagType = Converter.ToInt16(data, index);
                    index += 2;
                    Count = Converter.ToInt32(data, index);
                    index += 4;
                    Offset = data.Skip(index).Take(4).ToArray();
                    Value = ValueToString(data, TagType, Count, Offset);
                }

                public ushort TagID { get; }
                public short TagType { get; }
                public int Count { get; }
                public byte[] Offset { get; }
                public string Value { get; }

                public override string ToString()
                {
                    return TagNames.ContainsKey(TagID) ? $"{TagNames[TagID]}: {Value}" : "";
                }
            }
        }
    }
}