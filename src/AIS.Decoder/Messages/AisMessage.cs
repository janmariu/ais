namespace Bitbucket.AIS.Messages
{
    public class AisMessage
    {
        public int MMSI;
        public PositionReport PositionReport { get; set; }
        public VoyageReport VoygageReport { get; set; }
        public int MessageType { get; set; }
        public int RepeatIndicator { get; set; }

        public override string ToString()
        {
            string output = $"MMSI: {MMSI} Type: {MessageType} ";

            switch(MessageType)
            {
                case 1:
                case 2:
                case 3:
                    output += PositionReport?.ToString();
                    break;
                case 5:
                    output += VoygageReport?.ToString();
                    break;
                default:
                    output += "unsupported messagetype";
                    break;
            }

            return output;
        }
    }
}
