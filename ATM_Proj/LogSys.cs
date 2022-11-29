using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{

    public enum LogType
    { Req, Ack }
    class eLogger
    {
        public static eLog GenerateLog(eUserAction action, string data, string dst, string src, LogType type) { return new eLog(action, data, dst, src, type); }
        public static eLog GenerateLog(eHeader header, string data) { return new eLog(header.action, data, header.dst, header.src, header.type); }
    }

    public struct eHeader
    {
        public eUserAction action;
        public string dst;
        public string src;
        public LogType type;
    }

    class eLog
    {
        public eLog(eUserAction action, string data, string dst, string src, LogType type)
        {
            _header.action = action;
            _header.dst = dst;
            _header.src = src;
            _header.type = type;
            _data = data;
        }

        public eHeader Header => _header;
        public string Data => _data;
        private eHeader _header;
        private string _data;//from node is src, to node is dst answer
    }
}
