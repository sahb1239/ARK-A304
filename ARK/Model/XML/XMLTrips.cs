﻿// <auto-generated />

using System;
using System.Xml.Serialization;

namespace ARK.Model.XML
{
    public class XMLTrips
    {

        /// <remarks/>
        [XmlType(AnonymousType = true)]
        [XmlRoot(Namespace = "", IsNullable = false)]
        public partial class dataroot
        {
            private DateTime generatedField;

            /// <remarks/>
            [XmlElement("Tur")]
            public datarootTur[] Tur { get; set; }

            /// <remarks/>
            [XmlAttribute()]
            public DateTime generated
            {
                get
                {
                    return generatedField;
                }
                set
                {
                    generatedField = value;
                }
            }
        }

        /// <remarks/>
        [XmlType(AnonymousType = true)]
        public partial class datarootTur
        {
            private DateTime datoField;

            /// <remarks/>
            public ushort ID { get; set; }

            /// <remarks/>
            public byte Kilometer { get; set; }

            /// <remarks/>
            public DateTime Dato
            {
                get
                {
                    return datoField;
                }
                set
                {
                    datoField = value;
                }
            }

            /// <remarks/>
            public byte Langtur { get; set; }

            /// <remarks/>
            public byte BådID { get; set; }

            /// <remarks/>
            public ushort Nr1 { get; set; }

            /// <remarks/>
            public ushort Nr2 { get; set; }

            /// <remarks/>
            [XmlIgnore()]
            public bool Nr2Specified { get; set; }

            /// <remarks/>
            public ushort Nr3 { get; set; }

            /// <remarks/>
            [XmlIgnore()]
            public bool Nr3Specified { get; set; }

            /// <remarks/>
            public ushort Nr4 { get; set; }

            /// <remarks/>
            [XmlIgnore()]
            public bool Nr4Specified { get; set; }

            /// <remarks/>
            public ushort Nr5 { get; set; }

            /// <remarks/>
            [XmlIgnore()]
            public bool Nr5Specified { get; set; }

            /// <remarks/>
            public ushort Nr6 { get; set; }

            /// <remarks/>
            [XmlIgnore()]
            public bool Nr6Specified { get; set; }

            /// <remarks/>
            public ushort Nr7 { get; set; }

            /// <remarks/>
            [XmlIgnore()]
            public bool Nr7Specified { get; set; }

            /// <remarks/>
            public ushort Nr8 { get; set; }

            /// <remarks/>
            [XmlIgnore()]
            public bool Nr8Specified { get; set; }

            /// <remarks/>
            public ushort Nr9 { get; set; }

            /// <remarks/>
            [XmlIgnore()]
            public bool Nr9Specified { get; set; }
        }


    }
}

