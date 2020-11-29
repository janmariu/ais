# Bitbucket.AIS
A .NET library for parsing AIS data.

An example client using live data from the Norwegian coastal administration is included.

Currently supports message types: 1,2,3 - Position Report Class A,4 - Base Station Report, 5 - Static and Voyage Related Data. Pull requests for other types are welcome.

## Usage
AIS is a two-layer protocol. The outer layer is a NMEA message with an armoured string payload. 
To use this parser first you need to parse an NMEA message from a string and then decode the armoured payload to get the AIS message.

Include Bitbucket.AIS.dll to your project and use the AisDecoder class.

### Single sentence
```C#
using Bitbucket.AIS;
using Bitbucket.AIS.Messages;

NmeaMessage nmeaMsg = AisDecoder.ParseNmea("!BSVDM,1,1,,A,19NSCsP029PWIIrPoPv2<AjH06;4,0*79");
AisMessage aisMsg = AisDecoder.DecodeAis(nmeaMsg);
```

### Multi sentence 
AIS messages can be split over multiple NMEA lines. You are responsible for sentence ordering and passing the sentences to the library in the correct order.

```C#
var nmeaMsg1 = AisDecoder.ParseNmea("xxx"); //nmeaMsg1.SentenceNumber == 1 && nmeaMsg1.NumberOfSentences == 2.
var nmeaMsg2 = AisDecoder.ParseNmea("xxx"); //nmeaMsg2.SentenceNumber == 2 && nmeaMsg2.NumberOfSentences == 2.

AisMessage aisMsg = AisDecoder.DecodeAis(new List<NmeaMessage> { nmeaMsg, nmeaMsg2 });
```

# AIS information sources
https://gpsd.gitlab.io/gpsd/AIVDM.html
